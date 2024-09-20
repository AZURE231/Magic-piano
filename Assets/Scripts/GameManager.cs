using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const string MUSIC_NAME = "LikeMyFather";

    [SerializeField] RectTransform tilemap;

    private int score = 0;

    // Config the scroll
    [Header("Spawner")]
    [SerializeField] private TileSpawner tileSpawner;

    [Header("Score")]
    [SerializeField] private int perfectScore = 2;
    [SerializeField] private int greatScore = 1;
    [SerializeField] private int combo = 0;

    [Header("Tile speed")]
    [SerializeField] private const float minSpeed = 5f;
    [SerializeField] private const float maxSpeed = 10f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float currentSpeed;

    private float elapsedTime;

    private bool isStart = false;

    // Event Handler
    public event EventHandler OnGameOver;
    public event EventHandler OnGameWin;
    public event EventHandler OnRestartGame;

    public event EventHandler<OnScoreChangedEventArgs> OnScoreChanged;
    public class OnScoreChangedEventArgs : EventArgs
    {
        public int score;
        public bool isPerfect;
        public int combo;
    }

    private bool canRestart = true;
    private float restartCooldown = 1f;

    public static GameManager Instance;

    private void Awake()
    {

        //! This is a singleton
        if (GameManager.Instance == null)
        {
            Instance = this;
        }
        else if (GameManager.Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void GameManager_OnTileOutOfScreen(object sender, EventArgs e)
    {
        GameOver();
    }

    public void Update()
    {
        if (!isStart) return;
        if (AudioManager.Instance.IsPlayingSong(MUSIC_NAME) == false && isStart)
        {
            OnGameWin?.Invoke(this, EventArgs.Empty);
            currentSpeed = 0;
            isStart = false;
            tileSpawner.StopSpawnTile();
        }

        // Increase speed
        elapsedTime += Time.deltaTime;
        currentSpeed = Mathf.Min(minSpeed + (acceleration * elapsedTime), maxSpeed);
    }

    public void IncreasePoint(bool isPerfect)
    {
        if (isPerfect)
        {
            combo++;
            score += perfectScore + combo;
        }
        else
        {
            combo = 0;
            score += greatScore;
        }
        OnScoreChanged?.Invoke(this, new OnScoreChangedEventArgs
        {
            combo = combo,
            score = score,
            isPerfect = isPerfect
        });
    }

    public void StartGame()
    {
        currentSpeed = minSpeed;
        elapsedTime = 0;
        combo = 0;
        OnScoreChanged?.Invoke(this, new OnScoreChangedEventArgs
        {
            score = 0,
            combo = 0,
        });
        score = 0;
        AudioManager.Instance.Play(MUSIC_NAME);
        isStart = true;
        tileSpawner.StartSpawnTile();
    }

    public void RestartGame()
    {
        if (canRestart)
        {
            canRestart = false;
            StartCoroutine(RestartWithDelay());
        }
    }

    private IEnumerator RestartWithDelay()
    {
        yield return new WaitForSeconds(restartCooldown); // Add a short delay
        OnRestartGame?.Invoke(this, EventArgs.Empty);
        StartGame();
        canRestart = true;
    }

    public void GameOver()
    {
        currentSpeed = 0;
        isStart = false;
        tileSpawner.StopSpawnTile();
        AudioManager.Instance.StopPlaying(MUSIC_NAME);
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }

    public float GetSpeed()
    {
        return this.currentSpeed;
    }
}
