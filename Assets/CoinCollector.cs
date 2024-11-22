using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public AudioClip coinSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        UpdateScoreText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score += 1;

            Debug.Log("Score: " + score);

            UpdateScoreText();

            if (coinSound != null)
            {
                audioSource.PlayOneShot(coinSound);
            }

            GameManager.instance.CollectCoin();
            Destroy(other.gameObject);
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
