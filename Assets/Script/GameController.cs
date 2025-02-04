using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public GameObject readyPanel;
    public GameObject readyPanelButton;

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
        hPText.text = $"HP {player.currentHP.ToString()}/{player.maxHP.ToString()}";
        scoreText.text = tmpScore.ToString();
        killCountText.text = "Kill " + killCount.ToString();
        ballCountText.text = "Ball* " + ballStayCount.ToString();
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
    }
}
