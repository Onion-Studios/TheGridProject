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
    GameManager gamemanager;
    public bool Test_WaveActive = false;
    public int Test_waveintesity = 0;
    public int Test_wavenumber = 0;
    wave wavetospawn;
    int casualwave;

    // Start is called before the first frame update
    void Start()
    {
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        gamemanager = FindObjectOfType<GameManager>();
        if(Test_WaveActive == false)
        {
            StartCoroutine(SpawnWaveCoroutine());
        }
        else
        {
            StartCoroutine(Testspawncoroutine(Test_waveintesity, Test_wavenumber));
        }
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
        switch (gamemanager.GameIntensity)
        {
            case 0:
                casualwave = Random.Range(0, 7);
                wavetospawn = dictionarywaves[gamemanager.GameIntensity][casualwave];
                break;
            case 1:
                casualwave = Random.Range(0, 7);
                wavetospawn = dictionarywaves[gamemanager.GameIntensity][casualwave];
                break;
            case 2:
                casualwave = Random.Range(0, 7);
                wavetospawn = dictionarywaves[gamemanager.GameIntensity][casualwave];
                break;
        }

        for(int i = 0; i < wavetospawn.enemyID.Length ; i++)
        {
            foreach (var enemy in enemyspawnmanager.poolenemy[enemyIDWave1[i]])
            {
                if(enemy.gameObject.activeInHierarchy == false)
                {
                    enemy.transform.position = new Vector3(enemyspawnmanager.positionpossible[LanesWave1[i]], 1.3f, LanesWave1[i]);
                    switch (enemyIDWave1[i])
                    {
                        case 0:
                            NormalEnemy NormalEnemy = enemy.GetComponent<NormalEnemy>();
                            NormalEnemy.signnormalenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
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
                            kamikazeenemy.signkamikazenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
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
                            ArmoredEnemy armoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                            armoredEnemy.signarmoredenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
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
                        case 3:
                            UndyingEnemy undyingEnemy = enemy.GetComponent<UndyingEnemy>();
                            undyingEnemy.signundyingenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
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

     IEnumerator Testspawncoroutine(int waveintesnity, int wavenumber)
    {
        for (int i = 0; i < dictionarywaves[waveintesnity][wavenumber].enemyID.Length; i++)
        {
            foreach (var enemy in enemyspawnmanager.poolenemy[enemyIDWave1[i]])
            {
                if (enemy.gameObject.activeInHierarchy == false)
                {
                    enemy.transform.position = new Vector3(enemyspawnmanager.positionpossible[LanesWave1[i]], 1.3f, LanesWave1[i]);
                    switch (enemyIDWave1[i])
                    {
                        case 0:
                            NormalEnemy NormalEnemy = enemy.GetComponent<NormalEnemy>();
                            NormalEnemy.signnormalenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
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
                            kamikazeenemy.signkamikazenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
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
                        case 3:
                            ArmoredEnemy armoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                            armoredEnemy.signarmoredenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
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
                            undyingEnemy.signundyingenemy[SignGroup[SignGroupwave1[i]]].gameObject.SetActive(true);
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
