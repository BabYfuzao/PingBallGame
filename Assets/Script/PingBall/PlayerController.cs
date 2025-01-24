using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int flipPower;

    public HingeJoint2D leftJoint;
    public HingeJoint2D rightJoint;

    public SpriteRenderer plungerSR;
    public float launchForcePercentage = 0f;
    public float maxLaunchForce;

    public GameObject ball;
    public GameObject launchDoor;

    public bool isLaunching;
    public bool isShoot = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (ball != null)
        {
            SpringControl();
        }
        FlipperControl();
    }

    private void SpringControl()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            launchForcePercentage = Mathf.Clamp(launchForcePercentage + 3f * Time.deltaTime, 0, 1);
            plungerSR.size = new Vector3(5.12f, 1.3f + (5.12f - 1.3f) * (1 - launchForcePercentage));
        }
        else if (Input.GetKeyUp(KeyCode.Return) && !isShoot)
        {
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, maxLaunchForce * launchForcePercentage));
        }
        else
        {
            launchForcePercentage = 0;
            plungerSR.size = new Vector3(5.12f, 5.12f);
        }

        if (ball.transform.position.y > 1)
        {
            launchDoor.SetActive(true);
            isShoot = true;
        }

    }

    private void FlipperControl()
    {
        JointMotor2D leftMotor = leftJoint.motor;
        JointMotor2D rightMotor = rightJoint.motor;

        leftMotor.motorSpeed = Input.GetKey(KeyCode.A) ? flipPower : -flipPower;
        rightMotor.motorSpeed = Input.GetKey(KeyCode.D) ? -flipPower : flipPower;

        leftJoint.motor = leftMotor;
        rightJoint.motor = rightMotor;
    }
}
