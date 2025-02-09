using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleSceneController : MonoBehaviour
{
    public GameObject ui;

    public GameObject confirmPanel;

    public TextMeshProUGUI[] scoreRankTexts;
    public TextMeshProUGUI[] scoreNameTexts;
    public TextMeshProUGUI[] scoreTexts;

    public TextMeshProUGUI[] killRankTexts;
    public TextMeshProUGUI[] killNameTexts;
    public TextMeshProUGUI[] killTexts;

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
        ScoreLeaderboard();
        KillLeaderboard();
    }

    public void ScoreLeaderboard()
    {
        for (int i = 0; i < 5; i++)
        {
            string name = PlayerPrefs.GetString("SCORE_NAME_" + i, "-");
            int score = PlayerPrefs.GetInt("SCORE_" + i, 0);

            if (i < scoreRankTexts.Length)
            {
                scoreRankTexts[i].text = GetOrdinal(i + 1);
                scoreNameTexts[i].text = name;
                scoreTexts[i].text = score.ToString();
            }
        }
    }

    public void KillLeaderboard()
    {
        for (int i = 0; i < 5; i++)
        {
            string name = PlayerPrefs.GetString("KILLS_NAME_" + i, "-");
            int kills = PlayerPrefs.GetInt("KILLS_" + i, 0);

            if (i < killRankTexts.Length)
            {
                killRankTexts[i].text = GetOrdinal(i + 1);
                killNameTexts[i].text = name;
                killTexts[i].text = kills.ToString();
            }
        }
    }

    private string GetOrdinal(int number)
    {
        if (number % 100 / 10 == 1)
        {
            return number + "th";
        }
        else
        {
            switch (number % 10)
            {
                case 1: return number + "st";
                case 2: return number + "nd";
                case 3: return number + "rd";
                default: return number + "th";
            }
        }
    }

    public void UIHide()
    {
        ui.SetActive(false);
    }
}