using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int World { get; private set; }
    public int Stage { get; private set; }
    public int Lives { get; private set; }

    public int Coins { get; private set; }
    public AudioSource groundMusic;
    public AudioSource gameOverSound;
    public AudioSource lostLifeSound;
    public AudioSource coinSound;

    private readonly int stagesPerWorld = 4;
    private readonly int maxLives = 3;

    void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        Lives = maxLives;
        LoadLevel(1, 1);
        Coins = 0;
    }

    public void NextLevel()
    {
        int nextStage = Stage + 1 % stagesPerWorld;
        int nextWorld = nextStage == 0 ? World + 1 : World;
        LoadLevel(nextWorld, nextStage);
    }

    public void ResetLevel(float delaySeconds)
    {
        groundMusic.Stop();

        if (Lives - 1 > 0)
        {
            lostLifeSound.Play();
        }
        else
        {
            gameOverSound.Play();
        }

        Invoke(nameof(ResetLevel), delaySeconds);
    }

    public void ResetLevel()
    {
        Lives--;

        if (Lives > 0)
        {
            LoadLevel(World, Stage);
        }
        else
        {
            GameOver();
        }
    }

    public void LoadLevel(int world, int stage)
    {
        World = world;
        Stage = stage;
        groundMusic.Play();
        SceneManager.LoadScene($"{world}-{stage}");
    }

    private void GameOver()
    {
        StartNewGame();
    }

    public void AddCoin()
    {
        Coins++;
        coinSound.Play();

        if (Coins == 100)
        {
            AddLife();
            Coins = 0;
        }
    }

    public void AddLife()
    {
        Lives++;
    }


    #region Singleton
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    #endregion Singleton
}
