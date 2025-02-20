using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;

    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;

    private UIManager uiManager;
    public static bool isFirstLoading = true;

    private float highScore;

    private void Awake()
    {
        Instance = this;
        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        uiManager = FindObjectOfType<UIManager>();


        _playerResourceController=player.GetComponent<ResourceController>();
        _playerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        _playerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);

    }

    private void Start()
    {
        if(!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }
    }

    public void StartGame()
    {
        uiManager.SetPlayGame();
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(2 + currentWaveIndex / 4);
        uiManager.ChangeWave(currentWaveIndex);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        uiManager.SetGameOver(currentWaveIndex);
        CheckTopScore(currentWaveIndex);
    }

    public EnemyManager GetEnemyManager()
    {
        return enemyManager;
    }

    private void CheckTopScore(int score)
    {
        highScore = PlayerPrefs.GetInt("HighScore");

        if(highScore<score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

}
