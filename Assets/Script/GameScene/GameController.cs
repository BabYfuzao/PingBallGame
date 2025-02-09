using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using DG.Tweening;
using System;
using UnityEditor.Experimental.GraphView;

public class GameController : MonoBehaviour
{
    public PinBallObjectController pBObjController;
    public Player player;
    public SoundController soundController;
    public EnemySpawner enemySpawner;

    public int score;
    public int tmpScore = 0;

    public int killCount;

    public int ballStayCount;
    public int ballShotCount;

    public GameObject ballPrefab;
    [HideInInspector] public GameObject ball;
    public Transform launchPos;

    public GameObject playerDetailPanel;

    public TextMeshProUGUI readyText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI ballCountText;
    public TextMeshProUGUI hPText;

    public GameObject[] ballIcons;

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
    public GameObject JokerDisplay;
    public bool isGameOver = false;


    private void Start()
    {
        isGameInProgress = false;
        playerDetailPanel.SetActive(false);
        readyPanel.SetActive(true);
        countDownText.gameObject.SetActive(false);
    }

    public void UIStatusUpdate()
    {
        hPText.text = player.currentHP.ToString();
        scoreText.text = tmpScore.ToString();
        killCountText.text = killCount.ToString();
        ballCountText.text = "max(5)\n*" + ballStayCount.ToString();

        for (int i = 0; i < ballIcons.Length; i++)
        {
            if (i < ballStayCount)
            {
                ballIcons[i].SetActive(true);
            }
            else
            {
                ballIcons[i].SetActive(false);
            }
        }
    }

    public void SubmitPlayerName()
    {
        string input = playerNameInput.text;

        if (IsValidPlayerName(input))
        {
            playerName = input;
            playerNameText.text = playerName;

            playerNameInputObject.SetActive(false);
            okButton.SetActive(false);
            nameRuleTextObject.SetActive(false);
            readyButton.SetActive(true);
            readyText.gameObject.SetActive(true);

            readyText.text = $"{playerName}, ARE YOU READY?";
        }
        else
        {
            nameRuleTextTransform.DOShakeScale(1f, new Vector3(0.1f, 0.1f, 0), 10, 90, false);

            TMP_Text nameRuleText = nameRuleTextTransform.GetComponent<TMP_Text>();
            Color originalColor = nameRuleText.color;
            Color errorColor = Color.red;

            nameRuleText.color = errorColor;
            nameRuleText.DOColor(originalColor, 1f).SetEase(Ease.Linear);
        }
    }

    private bool IsValidPlayerName(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        if (input.Length > 8)
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
        readyText.gameObject.SetActive(false);
        countDownText.gameObject.SetActive(true);

        for (int countDown = 3; countDown > 0; countDown--)
        {
            soundController.PlayCountDownSFX();
            countDownText.text = countDown.ToString();
            yield return new WaitForSeconds(1f);
        }

        soundController.PlayGoSFX();
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        isGameInProgress = true;
        readyPanel.SetActive(false);
        playerDetailPanel.SetActive(true);
        UIStatusUpdate();
        StartCoroutine(enemySpawner.EnemySpawn());
        soundController.PlayGameBGM(true);
    }

    public void ScoreUpdate(int scoreCount)
    {
        score += scoreCount;
        StartCoroutine(StartScoreUpdate());
    }

    public IEnumerator StartScoreUpdate()
    {
        while (true)
        {
            if (tmpScore < score)
            {
                tmpScore++;
            }
            UIStatusUpdate();
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

        ball = Instantiate(ballPrefab, launchPos.position, Quaternion.identity);
        pBObjController.ball = ball;

        ballShotCount++;
        ballStayCount--;
        UIStatusUpdate();
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

        if (score >= 1 && killCount >= 1)
        {
            SaveResult();
            gameOverText.text = $"Congratulations! {playerName}, You get {score.ToString()} score, and kill {killCount.ToString()} enemy.";
        }
        else
        {
            gameOverText.text = "You look like a Joker.";
        }
    }

    private void SaveResult()
    {
        List<(string name, int score)> scoreLeaderboard = LoadScoreLeaderboard();
        List<(string name, int kills)> killLeaderboard = LoadKillLeaderboard();

        UpdateLeaderboard(scoreLeaderboard, playerName, score);
        UpdateLeaderboard(killLeaderboard, playerName, killCount);

        SaveLeaderboard(scoreLeaderboard, "SCORE_");
        SaveLeaderboard(killLeaderboard, "KILLS_");
    }

    private List<(string name, int score)> LoadScoreLeaderboard()
    {
        List<(string name, int score)> leaderboard = new List<(string, int)>();

        for (int i = 0; i < 5; i++)
        {
            string existingName = PlayerPrefs.GetString("SCORE_NAME_" + i, "-");
            int existingScore = PlayerPrefs.GetInt("SCORE_" + i, 0);
            leaderboard.Add((existingName, existingScore));
        }

        return leaderboard;
    }

    private List<(string name, int kills)> LoadKillLeaderboard()
    {
        List<(string name, int kills)> leaderboard = new List<(string, int)>();

        for (int i = 0; i < 5; i++)
        {
            string existingName = PlayerPrefs.GetString("KILLS_NAME_" + i, "-");
            int existingKills = PlayerPrefs.GetInt("KILLS_" + i, 0);
            leaderboard.Add((existingName, existingKills));
        }

        return leaderboard;
    }

    private void UpdateLeaderboard<T>(List<(string name, T score)> leaderboard, string playerName, T newScore) where T : IComparable
    {
        bool nameExists = false;

        for (int i = 0; i < leaderboard.Count; i++)
        {
            if (leaderboard[i].name == playerName)
            {
                leaderboard[i] = (playerName, Max(leaderboard[i].score, newScore));
                nameExists = true;
                break;
            }
        }

        if (!nameExists)
        {
            leaderboard.Add((playerName, newScore));
        }

        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));
    }

    private T Max<T>(T a, T b) where T : IComparable
    {
        return a.CompareTo(b) > 0 ? a : b;
    }

    private void SaveLeaderboard<T>(List<(string name, T score)> leaderboard, string prefix)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < leaderboard.Count)
            {
                PlayerPrefs.SetString(prefix + "NAME_" + i, leaderboard[i].name);
                PlayerPrefs.SetInt(prefix + i, Convert.ToInt32(leaderboard[i].score));
            }
            else
            {
                PlayerPrefs.SetString(prefix + "NAME_" + i, "-");
                PlayerPrefs.SetInt(prefix + i, 0);
            }
        }

        PlayerPrefs.Save();
    }
}
