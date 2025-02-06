using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public PingBallObjectController pBObjController;
    public Player player;
    public SoundController soundController;
    public EnemySpawner enemySpawner;

    public int score;
    public int tmpScore = 0;

    public int killCount;

    public int ballStayCount;
    public int ballShotCount;

    public GameObject ballPrefabs;
    public Transform launchPos;

    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI ballCountText;
    public TextMeshProUGUI hPText;

    public TMP_InputField playerNameInput;
    public GameObject playerNameInputObject;
    public TextMeshProUGUI playerNameText;
    public string playerName;

    public GameObject readyPanel;
    public GameObject readyPanelButton;
    public GameObject readyButton;

    public GameObject nameRuleTextObject;
    public Transform nameRuleTextTransform;
    public GameObject okButton;

    public GameObject pausePanel;
    public GameObject pauseButton;
    public bool isGameInProgress;
    public bool isGamePause = false;

    public GameObject gameOverPanel;
    public bool isGameOver = false;


    private void Start()
    {
        isGameInProgress = false;
        readyPanel.SetActive(true);
        countDownText.gameObject.SetActive(false);
    }

    public void TextHandle()
    {
        playerNameText.text = playerName;
        hPText.text = player.currentHP.ToString();
        scoreText.text = tmpScore.ToString("00000000");
        killCountText.text = killCount.ToString("00000000");
        ballCountText.text = "Ball* " + ballStayCount.ToString();
    }

    public void SubmitPlayerName()
    {
        string input = playerNameInput.text;

        if (IsValidPlayerName(input))
        {
            playerName = input;
            playerNameInputObject.SetActive(false);
            okButton.SetActive(false);
            nameRuleTextObject.SetActive(false);
            readyButton.SetActive(true);
        }
        else
        {
            nameRuleTextTransform.DOShakeScale(1f, new Vector3(0.1f, 0.1f, 0), 10, 90, false);
        }
    }

    private bool IsValidPlayerName(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        if (input.Length > 5)
            return false;

        if (input.Contains(" "))
            return false;

        foreach (char c in input)
        {
            if (!char.IsLetterOrDigit(c))
                return false;
        }

        return true;
    }

    public IEnumerator ReadyCountDown()
    {
        readyPanelButton.SetActive(false);
        countDownText.gameObject.SetActive(true);

        for (int countDown = 3; countDown > 0; countDown--)
        {
            countDownText.text = countDown.ToString();
            yield return new WaitForSeconds(1f);
        }

        countDownText.text = "GO";
        yield return new WaitForSeconds(1f);

        isGameInProgress = true;
        readyPanel.SetActive(false);
        TextHandle();
        StartCoroutine(enemySpawner.EnemySpawn());
        soundController.PlayGameBGM(true);
    }

    public IEnumerator UpdateScore()
    {
        while (true)
        {
            if (tmpScore < score)
            {
                tmpScore++;
            }
            TextHandle();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void PauseControl()
    {
        isGamePause = !isGamePause;
        isGameInProgress = !isGameInProgress;
        Time.timeScale = isGamePause ? 0 : 1;

        pausePanel.SetActive(isGamePause);
        pauseButton.SetActive(isGameInProgress);

        soundController.PlayGameBGM(isGameInProgress);
    }

    public void BallInstantiate()
    {
        soundController.PlayLoadSFX();

        GameObject ball = Instantiate(ballPrefabs, launchPos.position, Quaternion.identity);
        pBObjController.ball = ball;

        ballShotCount++;
        ballStayCount--;
        TextHandle();
    }

    public void CheckGameOverStatus()
    {
        StartCoroutine(GameOverJudge());
    }

    public IEnumerator GameOverJudge()
    {
        yield return new WaitForSeconds(0.1f);

        if (ballStayCount <= 0 && ballShotCount <= 0)
        {
            GameOver();
        }
    }

    public void PlayerGameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    private IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(0.1f);
        GameOver();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        isGameOver = true;
        soundController.PlayGameOverBGM();

        SaveResult();
    }

    private void SaveResult()
    {
        List<(string name, int score)> leaderboard = new List<(string, int)>();

        for (int i = 0; i < 3; i++)
        {
            string existingName = PlayerPrefs.GetString("NAME_" + i, "No Name");
            int existingScore = PlayerPrefs.GetInt("SCORE_" + i, 0);
            leaderboard.Add((existingName, existingScore));
        }

        bool nameExists = false;
        for (int i = 0; i < leaderboard.Count; i++)
        {
            if (leaderboard[i].name == playerName)
            {
                leaderboard[i] = (playerName, Mathf.Max(leaderboard[i].score, score));
                nameExists = true;
                break;
            }
        }

        if (!nameExists)
        {
            leaderboard.Add((playerName, score));
        }

        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));

        for (int i = 0; i < 3; i++)
        {
            if (i < leaderboard.Count)
            {
                PlayerPrefs.SetString("NAME_" + i, leaderboard[i].name);
                PlayerPrefs.SetInt("SCORE_" + i, leaderboard[i].score);
            }
            else
            {
                PlayerPrefs.SetString("NAME_" + i, "No Name");
                PlayerPrefs.SetInt("SCORE_" + i, 0);
            }
        }

        PlayerPrefs.Save();
    }
}
