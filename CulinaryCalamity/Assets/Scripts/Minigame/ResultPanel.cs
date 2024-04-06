using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the result panel UI and displays performance data.
/// </summary>
public class ResultPanel : MonoBehaviour
{
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TextMeshProUGUI _notesHitText;
    [SerializeField] private TextMeshProUGUI _notesMissedText;
    [SerializeField] private TextMeshProUGUI _noteStreakText;
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private TextMeshProUGUI _goldEarnedText;
    [SerializeField] private TextMeshProUGUI _percentHitText;

    private MiniGameManager _miniGameManager;

    /// <summary>
    /// Initializes the ResultPanel instance by assigning a reference to the MiniGameManager object in the scene.
    /// </summary>
    void Start()
    {
        _miniGameManager = FindObjectOfType<MiniGameManager>();
    }

    /// <summary>
    /// Displays the result panel with provided performance data.
    /// </summary>
    /// <param name="_notesHit"></param>
    /// <param name="_notesMissed"></param>
    /// <param name="_noteStreak"></param>
    /// <param name="_percentHit"></param>
    /// <param name="_currentScore"></param>
    /// <param name="_goldEarned"></param>
    public void ShowResults(int _notesHit, int _notesMissed, int _noteStreak, float _percentHit, int _currentScore, int _goldEarned)
    {
        _resultPanel.SetActive(true);
        
        _notesHitText.text = "Notes Hit: " + _notesHit;
        _notesMissedText.text = "Notes Missed: " + _notesMissed;
        _noteStreakText.text = "Note Streak: " + _noteStreak;
        _percentHitText.text = "Percent hit: " + _percentHit + "%";
        _goldEarnedText.text = "Gold Earned: " + _goldEarned;
        _finalScoreText.text = "Final Score: " + _currentScore;
    }

    /// <summary>
    /// Handles the exit button click event by loading the restaurant scene.
    /// </summary>
    public void OnExitButtonClick()
    {
        _miniGameManager.ReactivatePlayerObject();
        SceneManager.LoadScene("Restaurant");
    }
}
