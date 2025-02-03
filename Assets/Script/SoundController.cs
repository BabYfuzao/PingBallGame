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

    // SFX
    public AudioClip loadSFX;
    public AudioClip chargeSFX;
    public AudioClip shotSFX;
    public AudioClip ballHitSFX;
    public AudioClip gunSFX;
    public AudioClip flipperOpenSFX;
    public AudioClip flipperCloseSFX;

    //BGM
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

    //SFX
    public void PlayLoadSFX()
    {
        sFXAudioSource.PlayOneShot(loadSFX);
    }

    public void PlayChargeSFX()
    {
        sFXAudioSource.PlayOneShot(chargeSFX);
    }

    public void PlayShotSFX()
    {
        sFXAudioSource.PlayOneShot(shotSFX);
    }

    public void PlayBallHitSFX()
    {
        sFXAudioSource.PlayOneShot(ballHitSFX);
    }

    public void PlayGunSFX()
    {
        sFXAudioSource.PlayOneShot(gunSFX);
    }

    public void PlayFlipperOpenSFX()
    {
        sFXAudioSource.PlayOneShot(flipperOpenSFX);
    }

    public void PlayFlipperCloseSFX()
    {
        sFXAudioSource.PlayOneShot(flipperCloseSFX);
    }
}