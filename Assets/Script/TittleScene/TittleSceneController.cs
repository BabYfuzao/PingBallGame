using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TittleSceneController : MonoBehaviour
{
    public GameObject Button;

    public TextMeshProUGUI firstScore;

    void Start()
    {
        TextHandle();
    }

    public void DeleteScore()
    {
        PlayerPrefs.DeleteAll();
        TextHandle();
    }

    public void TextHandle()
    {
        int score = PlayerPrefs.GetInt("LAST SCORE", 0);
        firstScore.text = "1. " + score.ToString("000000");
    }

    public void UIHide()
    {
        Button.SetActive(false);
    }
}
