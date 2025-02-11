using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource bGMAudioSource;
    public AudioSource sFXAudioSource;

    // BGM
    public AudioClip gameBGM;
    public AudioClip gameOverBGM;
    public AudioClip gameOverBGM2;

    // SFX
    public AudioClip popSFX;
    // UI
    public AudioClip buttonSFX;
    public AudioClip countDownSFX;
    public AudioClip goSFX;

    // PinBall
    public AudioClip loadSFX;
    public AudioClip chargeSFX;
    public AudioClip shotSFX;
    public AudioClip ballHitSFX;
    public AudioClip scoreSFX;
    public AudioClip flipperOpenSFX;
    public AudioClip flipperCloseSFX;
    public AudioClip dangerSFX;
    public AudioClip meteoriteSFX;
    public AudioClip meteoriteExplosionSFX;
    public AudioClip holeSFX;
    public AudioClip bounceSFX;
    public AudioClip flipperSFX;

    // Battle
    public AudioClip gunSFX;
    public AudioClip enemyHitSFX;
    public AudioClip dropItemSFX;

    // BGM
    public void PlayGameBGM(bool isPlayGameBGM)
    {
        if (bGMAudioSource.clip != gameBGM)
        {
            bGMAudioSource.clip = gameBGM;
            bGMAudioSource.loop = true;
            bGMAudioSource.Play();
        }

        if (isPlayGameBGM)
        {
            if (!bGMAudioSource.isPlaying)
            {
                bGMAudioSource.UnPause();
            }
        }
        else
        {
            if (bGMAudioSource.isPlaying)
            {
                bGMAudioSource.Pause();
            }
        }
    }

    public void PlayGameOverBGM()
    {
        bGMAudioSource.clip = gameOverBGM;
        bGMAudioSource.loop = false;
        bGMAudioSource.Play();
    }

    public void PlayGameOverBGM2()
    {
        bGMAudioSource.clip = gameOverBGM2;
        bGMAudioSource.loop = false;
        bGMAudioSource.Play();
    }

    // SFX
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sFXAudioSource.PlayOneShot(clip);
        }
    }

    public void PlayPopSFX() => PlaySFX(popSFX);

    // UI
    public void PlayButtonSFX() => PlaySFX(buttonSFX);
    public void PlayCountDownSFX() => PlaySFX(countDownSFX);
    public void PlayGoSFX() => PlaySFX(goSFX);

    // PinBall
    public void PlayLoadSFX() => PlaySFX(loadSFX);
    public void PlayChargeSFX() => PlaySFX(chargeSFX);
    public void PlayShotSFX() => PlaySFX(shotSFX);
    public void PlayBallHitSFX() => PlaySFX(ballHitSFX);
    public void PlayScoreSFX() => PlaySFX(scoreSFX);
    public void PlayFlipperOpenSFX() => PlaySFX(flipperOpenSFX);
    public void PlayFlipperCloseSFX() => PlaySFX(flipperCloseSFX);
    public void PlayDangerSFX() => PlaySFX(dangerSFX);
    public void PlayMeteoriteSFX() => PlaySFX(meteoriteSFX);
    public void PlayMeteoriteExplosionSFX() => PlaySFX(meteoriteExplosionSFX);
    public void PlayBounceSFX() => PlaySFX(bounceSFX);
    public void PlayFlipperSFX() => PlaySFX(flipperSFX);

    public void PlayHoleSFX()
    {
        sFXAudioSource.clip = holeSFX;
        sFXAudioSource.loop = true;
        sFXAudioSource.Play();
    }

    public void StopHoleSFX()
    {
        sFXAudioSource.Stop();
    }

    // Battle
    public void PlayGunSFX() => PlaySFX(gunSFX);
    public void PlayEnemyHitSFX() => PlaySFX(enemyHitSFX);
    public void PlayDropItemSFX() => PlaySFX(dropItemSFX);
}