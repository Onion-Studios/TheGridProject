using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text ScoreRecord;
    public int LastScore;
    // Start is called before the first frame update
    private void Awake()
    {
        if (StartEndSequence.hoursPlayed == 0)
        {
            ScoreRecord.text = "TIME PLAYED:\n" + StartEndSequence.minutesPlayed + "M " + StartEndSequence.secondsPlayed + "S\n";
        }
        else
        {
            ScoreRecord.text = "TIME PLAYED:\n" + StartEndSequence.hoursPlayed + "H " + StartEndSequence.minutesPlayed + "M " + StartEndSequence.secondsPlayed + "S\n";
        }
        if (Inkstone.FinalScore > PersistantManagerScript.HighScore)
        {
            PersistantManagerScript.HighScore = Inkstone.FinalScore;
            PlayerPrefs.SetInt("HighScore", PersistantManagerScript.HighScore);
            PlayerPrefs.Save();
            ScoreRecord.text += "NEW RECORD!\n" + PersistantManagerScript.HighScore;
        }
        else
        {
            ScoreRecord.text += "FINAL SCORE:\n" + Inkstone.FinalScore + "\nPREVIOUS RECORD:\n" + PersistantManagerScript.HighScore;
        }
    }
}
