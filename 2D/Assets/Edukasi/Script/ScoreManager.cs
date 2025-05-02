using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    public int currentScore = 0;
    public int maxScore = 5;

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    [Header("Events")]
    public UnityEvent onMaxScoreReached;

    private bool maxScoreReached = false;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        currentScore = Mathf.Clamp(currentScore, 0, maxScore);
        UpdateScoreUI();

        if (currentScore >= maxScore && !maxScoreReached)
        {
            maxScoreReached = true;
            onMaxScoreReached?.Invoke();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Collected: {currentScore} / {maxScore}";
        }
    }
}
