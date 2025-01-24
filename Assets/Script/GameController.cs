using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public PlayerController player;

    public int score;
    public int tmpScore = 0;
    public int hP = 3;

    public GameObject ballPrefabs;
    public Transform launchPos;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hPText;

    private void Start()
    {
        GameStart();
        //TextHandle();
    }

    /*public void TextHandle()
    {
        hPText.text = "HP " + hP.ToString();
        scoreText.text = tmpScore.ToString();
    }*/

    public IEnumerator UpdateScore()
    {
        while (true)
        {
            if (tmpScore < score)
            {
                tmpScore++;
            }

            //TextHandle();
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void BallDestroy()
    {
        hP--;
        //TextHandle();
        /*if (hP > 0)
        {*/
            GameStart();
        /*}
        else
        {
            GameOver();
        }*/
        player.isShoot = false;
    }

    public void GameStart()
    {
        GameObject ball = Instantiate(ballPrefabs, launchPos.position, Quaternion.identity);
        player.ball = ball;
        player.launchDoor.SetActive(false);
        player.isLaunching = true;
    }

    public void GameOver()
    {

    }
}
