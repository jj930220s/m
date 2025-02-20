using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum UIState
{
    HOME,
    GAME,
    GAMEOVER
}

public class UIManager : MonoBehaviour
{
    HomeUI homeUI;
    GameUI gameUI;
    GameOverUI gameOverUI;

    private UIState currentState;

    private void Awake()
    {
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);

        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);

        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);

        ChangeState(UIState.HOME);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.GAME);
    }
    public void SetGameOver()
    {
        ChangeState(UIState.GAMEOVER);
    }    

    public void ChangeWave(int waveIndex)
    {
        gameUI.UpdateWaveText(waveIndex);

    }

    public void ChangePlayerHP(float currentHP,float maxHp)
    {
        gameUI.UpdateHPSlider(currentHP / maxHp);
    }




    public void ChangeState(UIState state)
    {
        currentState = state;

        homeUI.SetActive(state);
        gameUI.SetActive(state);
        gameOverUI.SetActive(state);
    }

}
