using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver = false;

    [Header("UI Elements")]
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI levelText;  
    public TextMeshProUGUI coinsText;  
    public GameObject restartButton;

    [Header("Audio")]
    public AudioClip gameOverSound;
    public AudioClip nextLevelSound; 
    private AudioSource audioSource;

    [Header("Level and Coins")]
    public int level = 1;  
    public int coinsPerLevel = 5; 
    private int remainingCoins;  
    public float spawnRange = 10.0f; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);
        if (levelText != null) UpdateLevelText();
        if (coinsText != null) UpdateCoinsText();
        SpawnCoins();
    }

    public void GameOver()
    {
        isGameOver = true;
        StopAllOtherAudio();
        PlayGameOverSound();
        ShowGameOverText();

        if (restartButton != null)
        {
            restartButton.SetActive(true);
            Debug.Log("Restart button displayed.");
        }
        else
        {
            Debug.LogError("Restart button is null!");
        }
    }

    void ShowGameOverText()
    {
        if (gameOverText != null)
        {
            gameOverText.text = "Game Over!";
            gameOverText.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("GameOverText is null!");
        }
    }

    void PlayGameOverSound()
    {
        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
        else
        {
            Debug.LogError("GameOverSound is not assigned!");
        }
    }

    void StopAllOtherAudio()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in allAudioSources)
        {
            if (source != audioSource)
            {
                source.Stop();
            }
        }
    }

    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + level;
        }
        else
        {
            Debug.LogError("LevelText is null!");
        }
    }

    void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = "Coins Left: " + remainingCoins;
        }
        else
        {
            Debug.LogError("CoinsText is null!");
        }
    }

    void SpawnCoins()
    {
        remainingCoins = coinsPerLevel * level;
        UpdateCoinsText();

        for (int i = 0; i < remainingCoins; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange), 
                1.3f,                                  
                Random.Range(-spawnRange, spawnRange)  
            );

            GameObject coinTemplate = GameObject.FindGameObjectWithTag("Coin");

            if (coinTemplate != null)
            {
                Instantiate(coinTemplate, randomPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogError("No GameObject with the 'Coin' tag found!");
                break;
            }
        }
    }


    public void CollectCoin()
    {
        remainingCoins--;
        UpdateCoinsText();
        Debug.Log("Coins remaining: " + remainingCoins);

        if (remainingCoins <= 0)
        {
            Debug.Log("Level Completed! Advancing to next level.");
            NextLevel();
        }
    }

    void NextLevel()
    {
        level++;
        UpdateLevelText();
        if (nextLevelSound != null)
        {
            audioSource.PlayOneShot(nextLevelSound);
        }
        else
        {
            Debug.LogError("NextLevelSound is not assigned!");
        }

        SpawnCoins();
    }

    public void RestartGame()
    {
        isGameOver = false;
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);

        level = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
