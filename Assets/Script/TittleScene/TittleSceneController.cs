using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TittleSceneController : MonoBehaviour
{
    public GameObject Button;

    public TextMeshProUGUI leaderboardText;

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
        DisplayLeaderboard();
    }

    public void DisplayLeaderboard()
    {
        leaderboardText.text = "Leaderboard:\n";

        for (int i = 0; i < 3; i++)
        {
            string name = PlayerPrefs.GetString("NAME_" + i, "No Name");
            int score = PlayerPrefs.GetInt("SCORE_" + i, 0);

            leaderboardText.text += $"No. {i + 1}: {name} - {score.ToString("000000")}\n";
        }
    }

    public void UIHide()
    {
        Button.SetActive(false);
    }
}
