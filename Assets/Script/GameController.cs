using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public PingBallObjectController pBObjController;
    public Player player;
    public SoundController soundController;

    public int score;
    public int tmpScore = 0;

    public int killCount;

    public int ballStayCount;
    public int ballShotCount;

    public GameObject ballPrefabs;
    public Transform launchPos;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI ballCountText;
    public TextMeshProUGUI hPText;

    public GameObject pausePanel;
    public GameObject pauseButton;
    public bool isGameInProgress = true;
    public bool isGamePause = false;

    public GameObject gameOverPanel;
    public bool isGameOver = false;

    private void Start()
    {
        Time.timeScale = 1;
        BallInstantiate();
        TextHandle();
        soundController.PlayGameBGM(true);
    }

    public void TextHandle()
    {
        hPText.text = "HP " + player.hP.ToString();
        scoreText.text = tmpScore.ToString();
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
            yield return new WaitForSeconds(0.05f);
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

    public void BallInstantiate()
    {
        GameObject ball = Instantiate(ballPrefabs, launchPos.position, Quaternion.identity);
        pBObjController.ball = ball;

        ballShotCount++;
        ballStayCount--;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        isGameOver = true;
        soundController.PlayGameOverBGM();
    }
}
