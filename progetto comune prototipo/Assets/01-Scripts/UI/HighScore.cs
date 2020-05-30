using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text ScoreRecord;
    public int LastScore;
    UIManager UIManager;
    // Start is called before the first frame update
    private void Awake()
    {
        LastScore = PersistantManagerScript.Instance.ScoreRecord;
    }
    void Start()
    {
        int conversion = int.Parse(ScoreRecord.text);
        ScoreRecord.text = conversion.ToString();
        if(LastScore>conversion)
        {
            ScoreRecord.text=LastScore.ToString();
        }
        //ScoreRecord.text = PersistantManagerScript.Instance.ScoreRecord.ToString();
        //ScoreRecord.text = PlayerPrefs.GetInt("HighScore", 000000000).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
