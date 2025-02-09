using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoleController : MonoBehaviour
{
    public bool isBlackHoleFormation = false;

    public GameObject pBlackHole;
    public GameObject pWhiteHole;

    public Transform pWhiteHoleTransForm;

    public GameObject bBlackHole;

    public GameObject heart;
    public Transform heart1, heart2, heart3;

    public PinBallObjectController pbobjController;
    public GameController gameController;
    public SoundController soundController;

    public void PinBallBlackHoleFormation(bool isFormation)
    {
        pBlackHole.SetActive(isFormation);
    }

    public void BattleBlackHoleFormation(bool isFormation)
    {
        if (!isBlackHoleFormation)
        {
            isBlackHoleFormation = true;
            soundController.PlayHoleSFX();
        }
        bBlackHole.SetActive(isFormation);
    }

    public void PinBallWhiteHoleFormation(bool isFormation)
    {
        pWhiteHole.SetActive(isFormation);
    }

    public void BallInBlackHole()
    {
        soundController.PlayHoleSFX();
        StartCoroutine(StartHoleAction());
    }

    public IEnumerator StartHoleAction()
    {
        heart1.DOShakeScale(1f, new Vector3(0.1f, 0.1f, 0), 10, 90, false);
        BattleBlackHoleFormation(true);
        for (int i = 0; i < 10; i++)
        {
            soundController.PlayScoreSFX();
            gameController.ScoreUpdate(5);
            yield return new WaitForSeconds(1f);
        }

        PinBallWhiteHoleFormation(true);
        pbobjController.ball.transform.position = pWhiteHoleTransForm.position;

        PinBallBlackHoleFormation(false);
        BattleBlackHoleFormation(false);
        yield return new WaitForSeconds(1f);

        PinBallWhiteHoleFormation(false);
        isBlackHoleFormation = false;
        soundController.StopHoleSFX();
    }
}
