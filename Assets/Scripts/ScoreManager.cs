using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro UI component
    private static ScoreManager instance;
    private int currentScore = 0;

    // Singleton instance
    public static ScoreManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        // Singleton pattern setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // If scoreText is not assigned, try to find it
            if (scoreText == null)
            {
                scoreText = FindObjectOfType<TextMeshProUGUI>();
                if (scoreText == null)
                {
                    Debug.LogError("No TextMeshProUGUI found in the scene! Please add a UI Text element.");
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreDisplay();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreDisplay();
        Debug.Log($"Score changed to: {currentScore}");
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
        else
        {
            Debug.LogError("Score Text component is missing!");

            // Try to find it again
            scoreText = FindObjectOfType<TextMeshProUGUI>();
            if (scoreText != null)
            {
                scoreText.text = $"Score: {currentScore}";
            }
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Try to find TextMeshProUGUI in the new scene if we don't have one
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<TextMeshProUGUI>();
            if (scoreText != null)
            {
                UpdateScoreDisplay();
            }
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
