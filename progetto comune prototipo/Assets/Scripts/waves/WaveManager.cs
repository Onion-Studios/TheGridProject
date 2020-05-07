using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    #region Database Wave
    int[] SignGroup = new int[6];
    //data waves 1
    public int[] enemyIDWave1 = new int[10];
    public int[] LanesWave1 = new int[10];
    public float[] delayswave1 = new float[10];
    public int[] SignGroupwave1 = new int[10];
    #endregion

    #region Wavesvariable
    wave[] waveintensity1 = new wave[6];
    wave[] waveintensity2 = new wave[6];
    wave[] waveintensity3 = new wave[6];
    Dictionary<int, wave[]> dictionarywaves;
    //waves created
    wave Wave1;
    #endregion

    Enemyspawnmanager enemyspawnmanager;
    public bool TRY_WaveActive = false;
    public int TRY_waveintesity = 0;
    public int TRY_wavenumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    void ShuffleAlgorithm(int[] array)
    {
        for(int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);
            int tempvariable = array[i];
            array[i] = array[j];
            array[j] = tempvariable;
        }
    }

    void Initializewaves()
    {
        Wave1 = new wave(enemyIDWave1, SignGroupwave1, LanesWave1, delayswave1);
    }

    IEnumerator SpawnWaveCoroutine()
    {
        for(int i = 0; i < enemyIDWave1.Length; i++)
        {
            foreach (var enemy in enemyspawnmanager.poolnemici[enemyIDWave1[i]])
            {
                if(enemy.gameObject.activeInHierarchy == false)
                {
                    enemy.transform.position = new Vector3(enemyspawnmanager.positionpossible[LanesWave1[i]], 1.3f, LanesWave1[i]);
                    switch (enemyIDWave1[i])
                    {
                        case 0:
                            NormalEnemy NormalEnemy = enemy.GetComponent<NormalEnemy>();
                            NormalEnemy.segninormalenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
                            if (LanesWave1[i] == 0)
                            {
                                NormalEnemy.speed = 1.35f;
                            }
                            else if (LanesWave1[i] == 1)
                            {
                                NormalEnemy.speed = 1.27f;
                            }
                            else if (LanesWave1[i] == 2)
                            {
                                NormalEnemy.speed = 1.17f;
                            }
                            else if (LanesWave1[i] == 3)
                            {
                                NormalEnemy.speed = 1.07f;
                            }
                            else if (LanesWave1[i] == 4)
                            {
                                NormalEnemy.speed = 1f;
                            }
                            break;
                        case 1:
                            KamikazeEnemy kamikazeenemy = enemy.GetComponent<KamikazeEnemy>();
                            kamikazeenemy.segnikamikazenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
                            if (LanesWave1[i] == 0)
                            {
                                kamikazeenemy.speed = 1.35f;
                            }
                            else if (LanesWave1[i] == 1)
                            {
                                kamikazeenemy.speed = 1.27f;
                            }
                            else if (LanesWave1[i] == 2)
                            {
                                kamikazeenemy.speed = 1.17f;
                            }
                            else if (LanesWave1[i] == 3)
                            {
                                kamikazeenemy.speed = 1.07f;
                            }
                            else if (LanesWave1[i] == 4)
                            {
                                kamikazeenemy.speed = 1f;
                            }
                            break;
                        case 2:
                            GoldenEnemy goldenEnemy = enemy.GetComponent<GoldenEnemy>();
                            goldenEnemy.segnigoldenenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
                            if (LanesWave1[i] == 0)
                            {
                                goldenEnemy.speed = 1.35f;
                            }
                            else if (LanesWave1[i] == 1)
                            {
                                goldenEnemy.speed = 1.27f;
                            }
                            else if (LanesWave1[i] == 2)
                            {
                                goldenEnemy.speed = 1.17f;
                            }
                            else if (LanesWave1[i] == 3)
                            {
                                goldenEnemy.speed = 1.07f;
                            }
                            else if (LanesWave1[i] == 4)
                            {
                                goldenEnemy.speed = 1f;
                            }
                            break;
                        case 3:
                            ArmoredEnemy armoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                            armoredEnemy.segniarmoredenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
                            if (LanesWave1[i] == 0)
                            {
                                armoredEnemy.speed = 1.35f;
                            }
                            else if (LanesWave1[i] == 1)
                            {
                                armoredEnemy.speed = 1.27f;
                            }
                            else if (LanesWave1[i] == 2)
                            {
                                armoredEnemy.speed = 1.17f;
                            }
                            else if (LanesWave1[i] == 3)
                            {
                                armoredEnemy.speed = 1.07f;
                            }
                            else if (LanesWave1[i] == 4)
                            {
                                armoredEnemy.speed = 1f;
                            }
                            break;
                        case 4:
                            UndyingEnemy undyingEnemy = enemy.GetComponent<UndyingEnemy>();
                            undyingEnemy.segniundyingenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
                            //undyingEnemy.startingPosition = enemyspawnposition;
                            if (LanesWave1[i] == 0)
                            {
                                undyingEnemy.speed = 1.35f;
                            }
                            else if (LanesWave1[i] == 1)
                            {
                                undyingEnemy.speed = 1.27f;
                            }
                            else if (LanesWave1[i] == 2)
                            {
                                undyingEnemy.speed = 1.17f;
                            }
                            else if (LanesWave1[i] == 3)
                            {
                                undyingEnemy.speed = 1.07f;
                            }
                            else if (LanesWave1[i] == 4)
                            {
                                undyingEnemy.speed = 1f;
                            }
                            break;
                        default:
                            break;
                    }
                    enemy.SetActive(true);
                    break;
                }                
            }
            yield return new WaitForSeconds(delayswave1[i]);
        }
    }


}
