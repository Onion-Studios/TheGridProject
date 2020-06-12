using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //database dei dati contenuti in ciascuna wave
    #region Database Wave
    public int[] SixSignGroup = new int[6];
    public int[] FourSignGroup = new int[4];
    public int[] ThreeSignGroup = new int[3];
    //LEGENDA il primo numero rappresenta l'intensita e il secondo la wave relativa a quell'intensita
    #region databasewave1
    [Header("Intesità 1")]
    //data wave 1-1
    public int[] enemyIDWave1_1 = new int[10];
    public int[] LanesWave1_1 = new int[10];
    public float[] delayswave1_1 = new float[10];
    public int[] SignGroupwave1_1 = new int[10];
    //data wave 1-2
    public int[] enemyIDWave1_2 = new int[11];
    public int[] LanesWave1_2 = new int[11];
    public float[] delayswave1_2 = new float[11];
    public int[] SignGroupwave1_2 = new int[11];
    //data wave 1-3
    public int[] enemyIDWave1_3 = new int[11];
    public int[] LanesWave1_3 = new int[11];
    public float[] delayswave1_3 = new float[11];
    public int[] SignGroupwave1_3 = new int[11];
    //data wave 1-4
    public int[] enemyIDWave1_4 = new int[10];
    public int[] LanesWave1_4 = new int[10];
    public float[] delayswave1_4 = new float[10];
    public int[] SignGroupwave1_4 = new int[10];
    //data wave 1-5
    public int[] enemyIDWave1_5 = new int[9];
    public int[] LanesWave1_5 = new int[9];
    public float[] delayswave1_5 = new float[9];
    public int[] SignGroupwave1_5 = new int[9];
    //data wave 1-6
    public int[] enemyIDWave1_6 = new int[12];
    public int[] LanesWave1_6 = new int[12];
    public float[] delayswave1_6 = new float[12];
    public int[] SignGroupwave1_6 = new int[12];
    #endregion
    #region databasewave2
    [Header("Intesità 2")]
    //data wave 2-1
    public int[] enemyIDWave2_1 = new int[12];
    public int[] LanesWave2_1 = new int[12];
    public float[] delayswave2_1 = new float[12];
    public int[] SignGroupwave2_1 = new int[12];
    //data wave 2-2
    public int[] enemyIDWave2_2 = new int[14];
    public int[] LanesWave2_2 = new int[14];
    public float[] delayswave2_2 = new float[14];
    public int[] SignGroupwave2_2 = new int[14];
    //data wave 2-3
    public int[] enemyIDWave2_3 = new int[13];
    public int[] LanesWave2_3 = new int[13];
    public float[] delayswave2_3 = new float[13];
    public int[] SignGroupwave2_3 = new int[13];
    //data wave 2-4
    public int[] enemyIDWave2_4 = new int[12];
    public int[] LanesWave2_4 = new int[12];
    public float[] delayswave2_4 = new float[12];
    public int[] SignGroupwave2_4 = new int[12];
    //data wave 2-5
    public int[] enemyIDWave2_5 = new int[13];
    public int[] LanesWave2_5 = new int[13];
    public float[] delayswave2_5 = new float[13];
    public int[] SignGroupwave2_5 = new int[13];
    //data wave 2-6
    public int[] enemyIDWave2_6 = new int[13];
    public int[] LanesWave2_6 = new int[13];
    public float[] delayswave2_6 = new float[13];
    public int[] SignGroupwave2_6 = new int[13];
    #endregion
    #region databasewave3
    [Header("Intesità 3")]
    //data wave 3-1
    public int[] enemyIDWave3_1 = new int[13];
    public int[] LanesWave3_1 = new int[13];
    public float[] delayswave3_1 = new float[13];
    public int[] SignGroupwave3_1 = new int[13];
    //data wave 3-2
    public int[] enemyIDWave3_2 = new int[12];
    public int[] LanesWave3_2 = new int[12];
    public float[] delayswave3_2 = new float[12];
    public int[] SignGroupwave3_2 = new int[12];
    //data wave 3-3
    public int[] enemyIDWave3_3 = new int[14];
    public int[] LanesWave3_3 = new int[14];
    public float[] delayswave3_3 = new float[14];
    public int[] SignGroupwave3_3 = new int[14];
    //data wave 3-4
    public int[] enemyIDWave3_4 = new int[11];
    public int[] LanesWave3_4 = new int[11];
    public float[] delayswave3_4 = new float[11];
    public int[] SignGroupwave3_4 = new int[11];
    //data wave 3-5
    public int[] enemyIDWave3_5 = new int[16];
    public int[] LanesWave3_5 = new int[16];
    public float[] delayswave3_5 = new float[16];
    public int[] SignGroupwave3_5 = new int[16];
    //data wave 3-6
    public int[] enemyIDWave3_6 = new int[12];
    public int[] LanesWave3_6 = new int[12];
    public float[] delayswave3_6 = new float[12];
    public int[] SignGroupwave3_6 = new int[12];
    #endregion
    #endregion

    //database delle varie wave
    #region Wavesvariable
    wave[] waveintensity1 = new wave[6];
    wave[] waveintensity2 = new wave[6];
    wave[] waveintensity3 = new wave[6];
    Dictionary<int, wave[]> dictionarywaves = new Dictionary<int, wave[]>();
    //waves created intensita 1
    wave Wave1_1;
    wave Wave1_2;
    wave Wave1_3;
    wave Wave1_4;
    wave Wave1_5;
    wave Wave1_6;
    //waves created intensita 2
    wave Wave2_1;
    wave Wave2_2;
    wave Wave2_3;
    wave Wave2_4;
    wave Wave2_5;
    wave Wave2_6;
    //wave created intesnita 3
    wave Wave3_1;
    wave Wave3_2;
    wave Wave3_3;
    wave Wave3_4;
    wave Wave3_5;
    wave Wave3_6;
    #endregion

    Enemyspawnmanager enemyspawnmanager;
    GameManager gamemanager;
    StartEndSequence startEndSequence;
    public int Activewave_intensity;
    public int Activewave_number;
    public bool TEST_WaveActive = false;
    public int TEST_waveintesity = 0;
    public int TEST_wavenumber = 0;
    wave wavetospawn;
    int casualwave;
    int count;
    IEnumerator normalWaves, testWaves;

    // Start is called before the first frame update
    void Start()
    {
        //referenza spawnmanager + null check
        enemyspawnmanager = FindObjectOfType<Enemyspawnmanager>();
        if(enemyspawnmanager == null)
        {
            Debug.Log("enemyspawnmanager is null");
        }
        //referenza gamemanager + null check
        gamemanager = FindObjectOfType<GameManager>();
        if (gamemanager == null)
        {
            Debug.Log("gamemanager is null");
        }
        startEndSequence = FindObjectOfType<StartEndSequence>();
        if (startEndSequence == null)
        {
            Debug.LogError("StartEndSequence is NULL!");
        }

        Initializewaves();
        Initializeintensitywave();
        Initializedictionarywaves();
        //check sulla partenza della wave di test oppure no

        normalWaves = SpawnWaveCoroutine();
        testWaves = Testspawncoroutine(TEST_waveintesity, TEST_wavenumber);



    }

    // Update is called once per frame
    void Update()
    {

        if (startEndSequence.starting == false && count == 0)
        {
            
            if (TEST_WaveActive == false)
            {
                StartCoroutine(normalWaves);
            }
            else
            {
                StartCoroutine(testWaves);
            }
            count++;
        }
        if(startEndSequence.ending == true && count == 1)
        {
            if (TEST_WaveActive == false)
            {
                StopCoroutine(normalWaves);
            }
            else
            {
                StopCoroutine(testWaves);
            }
            count++;
        }

    }

    //algoritmo di shuffle di fisher-yates per un array finito
    void ShuffleAlgorithm(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i);
            int tempvariable = array[i];
            array[i] = array[j];
            array[j] = tempvariable;
        }
    }

    //inizializza le wave mettendo i dati delle wave al loro interno
    void Initializewaves()
    {
        //wave intensity 1
        Wave1_1 = new wave(enemyIDWave1_1, SignGroupwave1_1, LanesWave1_1, delayswave1_1);
        Wave1_2 = new wave(enemyIDWave1_2, SignGroupwave1_2, LanesWave1_2, delayswave1_2);
        Wave1_3 = new wave(enemyIDWave1_3, SignGroupwave1_3, LanesWave1_3, delayswave1_3);
        Wave1_4 = new wave(enemyIDWave1_4, SignGroupwave1_4, LanesWave1_4, delayswave1_4);
        Wave1_5 = new wave(enemyIDWave1_5, SignGroupwave1_5, LanesWave1_5, delayswave1_5);
        Wave1_6 = new wave(enemyIDWave1_6, SignGroupwave1_6, LanesWave1_6, delayswave1_6);
        //wave intensity 2
        Wave2_1 = new wave(enemyIDWave2_1, SignGroupwave2_1, LanesWave2_1, delayswave2_1);
        Wave2_2 = new wave(enemyIDWave2_2, SignGroupwave2_2, LanesWave2_2, delayswave2_2);
        Wave2_3 = new wave(enemyIDWave2_3, SignGroupwave2_3, LanesWave2_3, delayswave2_3);
        Wave2_4 = new wave(enemyIDWave2_4, SignGroupwave2_4, LanesWave2_4, delayswave2_4);
        Wave2_5 = new wave(enemyIDWave2_5, SignGroupwave2_5, LanesWave2_5, delayswave2_5);
        Wave2_6 = new wave(enemyIDWave2_6, SignGroupwave2_6, LanesWave2_6, delayswave2_6);
        //wave intensity 3
        Wave3_1 = new wave(enemyIDWave3_1, SignGroupwave3_1, LanesWave3_1, delayswave3_1);
        Wave3_2 = new wave(enemyIDWave3_2, SignGroupwave3_2, LanesWave3_2, delayswave3_2);
        Wave3_3 = new wave(enemyIDWave3_3, SignGroupwave3_3, LanesWave3_3, delayswave3_3);
        Wave3_4 = new wave(enemyIDWave3_4, SignGroupwave3_4, LanesWave3_4, delayswave3_4);
        Wave3_5 = new wave(enemyIDWave3_5, SignGroupwave3_5, LanesWave3_5, delayswave3_5);
        Wave3_6 = new wave(enemyIDWave3_6, SignGroupwave3_6, LanesWave3_6, delayswave3_6);
    }

    //inizializza le wave nelle corrette intensita
    void Initializeintensitywave()
    {
        //intensity1
        waveintensity1[0] = Wave1_1;
        waveintensity1[1] = Wave1_2;
        waveintensity1[2] = Wave1_3;
        waveintensity1[3] = Wave1_4;
        waveintensity1[4] = Wave1_5;
        waveintensity1[5] = Wave1_6;
        //intensity2
        waveintensity2[0] = Wave2_1;
        waveintensity2[1] = Wave2_2;
        waveintensity2[2] = Wave2_3;
        waveintensity2[3] = Wave2_4;
        waveintensity2[4] = Wave2_5;
        waveintensity2[5] = Wave2_6;
        //intensity3
        waveintensity3[0] = Wave3_1;
        waveintensity3[1] = Wave3_2;
        waveintensity3[2] = Wave3_3;
        waveintensity3[3] = Wave3_4;
        waveintensity3[4] = Wave3_5;
        waveintensity3[5] = Wave3_6;
    }

    //inizializza le wave di determinata intensita con la loro key di relativa intensita
    void Initializedictionarywaves()
    {

        dictionarywaves.Add(1, waveintensity1);
        dictionarywaves.Add(2, waveintensity2);
        dictionarywaves.Add(3, waveintensity3);
    }

    /// <summary>
    /// coroutine di spawn delle wave 
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnWaveCoroutine()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            ShuffleAlgorithm(SixSignGroup);
            ShuffleAlgorithm(FourSignGroup);
            ShuffleAlgorithm(ThreeSignGroup);

            switch (gamemanager.GameIntensity)
            {
                case 1:
                    casualwave = Random.Range(0, 6);
                    wavetospawn = dictionarywaves[gamemanager.GameIntensity][casualwave];
                    Activewave_intensity = gamemanager.GameIntensity;
                    Activewave_number = casualwave;
                    break;
                case 2:
                    casualwave = Random.Range(0, 6);
                    wavetospawn = dictionarywaves[gamemanager.GameIntensity][casualwave];
                    Activewave_intensity = gamemanager.GameIntensity;
                    Activewave_number = casualwave;
                    break;
                case 3:
                    casualwave = Random.Range(0, 6);
                    wavetospawn = dictionarywaves[gamemanager.GameIntensity][casualwave];
                    Activewave_intensity = gamemanager.GameIntensity;
                    Activewave_number = casualwave;
                    break;
            }

            for (int i = 0; i < wavetospawn.enemyID.Length; i++)
            {
                foreach (var enemy in enemyspawnmanager.poolenemy[wavetospawn.enemyID[i]])
                {
                    if (enemy.gameObject.activeInHierarchy == false)
                    {
                        enemy.transform.position = new Vector3(enemyspawnmanager.positionpossible[wavetospawn.lanes[i]], 1.3f, wavetospawn.lanes[i]);
                        switch (wavetospawn.enemyID[i])
                        {
                            case 0:
                                NormalEnemy NormalEnemy = enemy.GetComponent<NormalEnemy>();
                                switch(gamemanager.GameIntensity)
                                {
                                    case 1:
                                        NormalEnemy.SignNormalYokai[SixSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                    case 2:
                                        NormalEnemy.SignIntensity1Normal[SixSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                    case 3:
                                        NormalEnemy.SignIntensity1PlusNormal[SixSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                }
                                NormalEnemy.speed = NormalEnemy.baseSpeed;
                                break;
                            case 1:
                                KamikazeEnemy kamikazeenemy = enemy.GetComponent<KamikazeEnemy>();
                                switch (gamemanager.GameIntensity)
                                {
                                    case 1:
                                        kamikazeenemy.SignIntensity1Kamikaze[SixSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                    case 2:
                                        kamikazeenemy.SignIntensity1PlusKamikaze[SixSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                    case 3:
                                        kamikazeenemy.SignIntensity2Kamikaze[FourSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                }
                                kamikazeenemy.speed = kamikazeenemy.baseSpeed;
                                break;
                            case 2:
                                ArmoredEnemy armoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                                switch (gamemanager.GameIntensity)
                                {
                                    case 1:
                                        armoredEnemy.SignIntensity1Armored[SixSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                    case 2:
                                        armoredEnemy.SignIntensity1PlusArmored[SixSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                    case 3:
                                        armoredEnemy.SignIntensity2Armored[FourSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                }
                                armoredEnemy.speed = armoredEnemy.baseSpeed;
                                break;
                            case 3:
                                UndyingEnemy undyingEnemy = enemy.GetComponent<UndyingEnemy>();
                                switch (gamemanager.GameIntensity)
                                {
                                    case 3:
                                        undyingEnemy.SignIntensity3Undying[ThreeSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                }
                                //undyingEnemy.startingPosition = enemyspawnposition;
                                undyingEnemy.speed = undyingEnemy.baseSpeed;
                                break;
                            case 4:
                                MalevolentEnemy malevolentEnemy = enemy.GetComponent<MalevolentEnemy>();
                                /*malevolentEnemy.signmalevolentenemy[SignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                */
                                break;
                            case 5:
                                FrighteningEnemy frighteningEnemy = enemy.GetComponent<FrighteningEnemy>();
                                switch (gamemanager.GameIntensity)
                                {
                                    case 2:
                                        frighteningEnemy.SignIntensity2Frightening[FourSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                    case 3:
                                        frighteningEnemy.SignIntensity2PlusFrightening[FourSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                }
                                frighteningEnemy.speed = frighteningEnemy.baseSpeed;
                                break;
                            case 6:
                                BufferEnemy bufferEnemy = enemy.GetComponent<BufferEnemy>();
                                switch (gamemanager.GameIntensity)
                                {
                                    case 1:
                                        bufferEnemy.SignIntensity1Buffer[SixSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                    case 2:
                                        bufferEnemy.SignIntensity1PlusBuffer[SixSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                    case 3:
                                        bufferEnemy.SignIntensity2Buffer[FourSignGroup[wavetospawn.Signgroup[i] - 1]].gameObject.SetActive(true);
                                        break;
                                }
                                bufferEnemy.speed = bufferEnemy.baseSpeed;
                                break;
                            default:
                                break;
                        }
                        enemy.SetActive(true);
                        break;
                    }
                }
                yield return new WaitForSeconds(wavetospawn.delays[i]);
                                NormalEnemy.speed = NormalEnemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                                kamikazeenemy.speed = kamikazeenemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                                armoredEnemy.speed = armoredEnemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                                undyingEnemy.speed = undyingEnemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                                frighteningEnemy.speed = frighteningEnemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                                bufferEnemy.speed = bufferEnemy.baseSpeed + gamemanager.intensitySpeedIncrease;
            }
         
        }

    }

    /// <summary>
    /// coroutine di spawn della wave di test
    /// </summary>
    /// <param name="waveintesnity"></param>
    /// <param name="wavenumber"></param>
    /// <returns></returns>
    IEnumerator Testspawncoroutine(int waveintesnity, int wavenumber)
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < dictionarywaves[waveintesnity][wavenumber].enemyID.Length; i++)
        {
            foreach (var enemy in enemyspawnmanager.poolenemy[dictionarywaves[waveintesnity][wavenumber].enemyID[i]])
            {
                if (enemy.gameObject.activeInHierarchy == false)
                {
                    enemy.transform.position = new Vector3(enemyspawnmanager.positionpossible[dictionarywaves[waveintesnity][wavenumber].lanes[i]], 1.3f, dictionarywaves[waveintesnity][wavenumber].lanes[i]);
                    switch (dictionarywaves[waveintesnity][wavenumber].enemyID[i])
                    {
                        case 0:
                            NormalEnemy NormalEnemy = enemy.GetComponent<NormalEnemy>();
                            //NormalEnemy.signnormalenemy[SignGroup[dictionarywaves[waveintesnity][wavenumber].Signgroup[i] - 1]].gameObject.SetActive(true);
                            NormalEnemy.speed = NormalEnemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                            break;
                        case 1:
                            KamikazeEnemy kamikazeenemy = enemy.GetComponent<KamikazeEnemy>();
                            //kamikazeenemy.signkamikazenemy[SignGroup[dictionarywaves[waveintesnity][wavenumber].Signgroup[i] - 1]].gameObject.SetActive(true);
                            kamikazeenemy.speed = kamikazeenemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                            break;
                        case 2:
                            ArmoredEnemy armoredEnemy = enemy.GetComponent<ArmoredEnemy>();
                            //armoredEnemy.signarmoredenemy[SignGroup[dictionarywaves[waveintesnity][wavenumber].Signgroup[i] - 1]].gameObject.SetActive(true);
                            armoredEnemy.speed = armoredEnemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                            break;
                        case 3:
                            UndyingEnemy undyingEnemy = enemy.GetComponent<UndyingEnemy>();
                            //undyingEnemy.signundyingenemy[SignGroup[dictionarywaves[waveintesnity][wavenumber].Signgroup[i] - 1]].gameObject.SetActive(true);
                            //undyingEnemy.startingPosition = enemyspawnposition;
                            undyingEnemy.speed = undyingEnemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                            break;
                        case 4:
                            MalevolentEnemy malevolentEnemy = enemy.GetComponent<MalevolentEnemy>();
                            break;
                        case 5:
                            FrighteningEnemy frighteningEnemy = enemy.GetComponent<FrighteningEnemy>();
                            //frighteningEnemy.signfrighteningenemy[dictionarywaves[waveintesnity][wavenumber].Signgroup[i] - 1].gameObject.SetActive(true);
                            frighteningEnemy.speed = frighteningEnemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                            break;
                        case 6:
                            BufferEnemy bufferEnemy = enemy.GetComponent<BufferEnemy>();
                            //bufferEnemy.signbufferenemy[dictionarywaves[waveintesnity][wavenumber].Signgroup[i] - 1].gameObject.SetActive(true);
                            bufferEnemy.speed = bufferEnemy.baseSpeed + gamemanager.intensitySpeedIncrease;
                            break;
                        default:
                            break;
                    }
                    enemy.SetActive(true);
                    break;
                }
            }
            yield return new WaitForSeconds(dictionarywaves[waveintesnity][wavenumber].delays[i]);
        }
    }


}
