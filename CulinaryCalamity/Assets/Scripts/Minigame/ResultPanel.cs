using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the result panel UI and displays performance data.
/// </summary>
public class ResultPanel : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI notesHitText;
    [SerializeField] private TextMeshProUGUI notesMissedText;
    [SerializeField] private TextMeshProUGUI noteStreakText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI goldEarnedText;
    [SerializeField] private TextMeshProUGUI percentHitText;

    /// <summary>
    /// Displays the result panel with provided performance data.
    /// </summary>
    /// <param name="notesHit"></param>
    /// <param name="notesMissed"></param>
    /// <param name="noteStreak"></param>
    /// <param name="percentHit"></param>
    /// <param name="currentScore"></param>
    /// <param name="goldEarned"></param>
    public void ShowResults(int notesHit, int notesMissed, int noteStreak, float percentHit, int currentScore, int goldEarned)
    {
        resultPanel.SetActive(true);
        
        notesHitText.text = "Notes Hit: " + notesHit;
        notesMissedText.text = "Notes Missed: " + notesMissed;
        noteStreakText.text = "Note Streak: " + noteStreak;
        percentHitText.text = "Percent hit: " + percentHit + "%";
        goldEarnedText.text = "Gold Earned: " + goldEarned;
        finalScoreText.text = "Final Score: " + currentScore;
    }

    /// <summary>
    /// Handles the exit button click event by loading the restaurant scene.
    /// </summary>
    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("Restaurant"); // Load the desired scene
    }
}
