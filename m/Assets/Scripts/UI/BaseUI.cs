using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseUI : MonoBehaviour
{

    protected UIManager uiManager;

    public virtual void Init(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    protected virtual void ExitButton()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("GameScene");
    }

    protected abstract UIState GetUIState();

    public void SetActive(UIState state)
    {
        this.gameObject.SetActive(GetUIState()==state);
    }
}
