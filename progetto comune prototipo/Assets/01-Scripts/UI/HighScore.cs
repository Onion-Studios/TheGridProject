using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text ScoreRecord;
    public int LastScore;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Inkstone.FinalScore > PersistantManagerScript.HighScore)
        {
            PersistantManagerScript.HighScore = Inkstone.FinalScore;
            PlayerPrefs.SetInt("HighScore", PersistantManagerScript.HighScore);
            PlayerPrefs.Save();
        }
    }
    void Start()
    {
        //ScoreRecord.text = PersistantManagerScript.HighScore.ToString();
        //ScoreRecord.text = PersistantManagerScript.Instance.ScoreRecord.ToString();
        //ScoreRecord.text = PlayerPrefs.GetInt("HighScore", 000000000).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
