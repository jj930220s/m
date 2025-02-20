using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;


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

    protected override UIState GetUIState()
    {
        return UIState.GAMEOVER;
    }





}
