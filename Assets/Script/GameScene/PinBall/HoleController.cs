using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoleController : MonoBehaviour
{
    public bool isBlackHoleFormation = false;
    public bool canFakeBallInstantiate = false;

    public float blackHoleDuration;

    public GameObject pBlackHole;
    public GameObject pWhiteHole;

    public GameObject fakeBallPrefab;

    public Transform pWhiteHoleTransForm;

    public GameObject bBlackHole;

    public GameObject heart;
    public Transform[] hearts;

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

    public void FakeBallInstantiate()
    {
        if (!canFakeBallInstantiate)
        {
            GameObject fakeBall = Instantiate(fakeBallPrefab, pWhiteHoleTransForm.position, Quaternion.identity);
            canFakeBallInstantiate = true;
        }
    }

    public void BallInBlackHole()
    {
        soundController.PlayHoleSFX();
        StartCoroutine(StartHoleAction());
    }

    public IEnumerator StartHoleAction()
    {
        BattleBlackHoleFormation(true);
        heart.SetActive(true);
        SpriteRenderer heartSR = heart.GetComponent<SpriteRenderer>();

        foreach (Transform heart in hearts)
        {
            heart.DOShakeScale(blackHoleDuration, new Vector3(1f, 1f, 0), 10, 30, true);
        }

        for (int i = 0; i < blackHoleDuration; i++)
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
        heart.SetActive(false);
        isBlackHoleFormation = false;
        soundController.StopHoleSFX();
    }
}
