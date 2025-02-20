using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class ShowUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        text.text = "HighScore: " + PlayerPrefs.GetInt("HighScore");
    }


}
