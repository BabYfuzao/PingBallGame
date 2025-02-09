using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBallObjectController : MonoBehaviour
{
    public int flipPower;

    public HingeJoint2D leftJoint;
    public HingeJoint2D rightJoint;

    private bool isLeftFlipping = false;
    private bool isRightFlipping = false;

    public SpriteRenderer plungerSR;
    public float launchForcePercentage = 0f;
    public float maxLaunchForce;

    [HideInInspector] public bool isPlayChargeSFX = false;

    public GameObject ball;
    public GameObject launchDoor;

    public bool isShoot = true;
    public bool canLoaded = true;

    public GameController gameController;
    public SoundController soundController;

    void Update()
    {
        if (gameController.isGameInProgress && !gameController.isGamePause && !gameController.isGameOver)
        {
            SpringControl();
            FlipperControl();
            BallInstantiate();
        }
    }

    private void SpringControl()
    {
        if (Input.GetKey(KeyCode.Return) && !isShoot)
        {
            if (!isPlayChargeSFX)
            {
                soundController.PlayChargeSFX();
                isPlayChargeSFX = true;
            }

            launchForcePercentage = Mathf.Clamp(launchForcePercentage + 3f * Time.deltaTime, 0, 1);
            plungerSR.size = new Vector3(5.12f, 1.3f + (5.12f - 1.3f) * (1 - launchForcePercentage));
        }
        else if (Input.GetKeyUp(KeyCode.Return) && !isShoot)
        {
            soundController.PlayShotSFX();
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, maxLaunchForce * launchForcePercentage));
        }
        else
        {
            launchForcePercentage = 0;
            plungerSR.size = new Vector3(5.12f, 5.12f);
        }
    }

    private void FlipperControl()
    {
        JointMotor2D leftMotor = leftJoint.motor;
        JointMotor2D rightMotor = rightJoint.motor;

        if (Input.GetKey(KeyCode.A))
        {
            if (!isLeftFlipping)
            {
                soundController.PlayFlipperOpenSFX();
                isLeftFlipping = true;
            }
            leftMotor.motorSpeed = flipPower;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            if (isLeftFlipping)
            {
                soundController.PlayFlipperCloseSFX();
                isLeftFlipping = false;
            }
            leftMotor.motorSpeed = -flipPower;
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (!isRightFlipping)
            {
                soundController.PlayFlipperOpenSFX();
                isRightFlipping = true;
            }
            rightMotor.motorSpeed = -flipPower;
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            if (isRightFlipping)
            {
                soundController.PlayFlipperCloseSFX();
                isRightFlipping = false;
            }
            rightMotor.motorSpeed = flipPower;
        }

        leftJoint.motor = leftMotor;
        rightJoint.motor = rightMotor;
    }

    private void BallInstantiate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameController.ballStayCount >= 1 && canLoaded)
        {
            gameController.BallInstantiate();
            canLoaded = false;
        }
    }

}
