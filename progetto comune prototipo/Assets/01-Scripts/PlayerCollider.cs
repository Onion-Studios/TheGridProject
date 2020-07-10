using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    Playerbehaviour PB;
    private void Start()
    {
        PB = FindObjectOfType<Playerbehaviour>();
    }
    public void OnTriggerStay(Collider other)
    {
        if (PB.invincibilityActive == false && PB.hitOnce == false)
        {
            if (other.gameObject.CompareTag("NormalEnemy"))
            {
                other.GetComponent<NormalEnemy>().Deathforgriglia();
            }
            if (other.gameObject.CompareTag("KamikazeEnemy"))
            {
                other.GetComponent<KamikazeEnemy>().Deathforgriglia();
            }
            if (other.gameObject.CompareTag("ArmoredEnemy"))
            {
                other.GetComponent<ArmoredEnemy>().Deathforgriglia();
            }
            if (other.gameObject.CompareTag("FrighteningEnemy"))
            {
                other.GetComponent<FrighteningEnemy>().Deathforgriglia();
            }
            PB.hitOnce = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        PB.hitOnce = false;
    }
}
