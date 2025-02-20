using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI gameOverMsg;


    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);

        restartButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);

    }

    protected override void ExitButton()
    {
        base.ExitButton();
    }
    public void OnClickStartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickExitButton()
    {
        ExitButton();
    }

    public void SetWaveText(int wave)
    {
        gameOverMsg.text="Now Wave : " + wave.ToString(); 
    }

    protected override UIState GetUIState()
    {
        return UIState.GAMEOVER;
    }





}
