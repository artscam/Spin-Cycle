using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float speedRails = 60;

    public float maxBrakeTorque = 50;

    public GameObject massFlag;
    public Vector3 moveCOM;
    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = moveCOM;
    }


    // finds the corresponding visual wheel element
    // correctly applies the transform

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float currentSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        float currentMotor = maxMotorTorque * Mathf.Clamp(Input.GetAxis("Vertical"), -1.0f, 1.0f);
        float currentSteering = maxSteeringAngle * Mathf.Clamp(Input.GetAxis("Horizontal"), -1.0f, 1.0f) 
            * (1+Mathf.Exp(-currentSpeed/speedRails))/2;
       //  Debug.Log("velocity = " + GetComponent<Rigidbody>().velocity + ", steering = " + currentSteering + " torque = " + axleInfos[0].leftWheel.motorTorque);

        foreach (AxleInfo thisAxle in axleInfos)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                thisAxle.isBraking = true;
            } 
            else
            {
                thisAxle.isBraking = false;
            }

            if (thisAxle.isSteering)
            {
                thisAxle.leftWheel.steerAngle = currentSteering;
                thisAxle.rightWheel.steerAngle = currentSteering;
            }
            if (thisAxle.isMotor && !thisAxle.isBraking)
            {
                thisAxle.leftWheel.motorTorque = currentMotor;
                thisAxle.rightWheel.motorTorque = currentMotor;
            }
            if (thisAxle.isBraking)
            {
                thisAxle.leftWheel.brakeTorque = maxBrakeTorque;
                thisAxle.rightWheel.brakeTorque = maxBrakeTorque;
            }
            else
            {
                thisAxle.leftWheel.brakeTorque = 0;
                thisAxle.rightWheel.brakeTorque = 0;
            }
            ApplyLocalPositionToVisuals(thisAxle.leftWheel);
            ApplyLocalPositionToVisuals(thisAxle.rightWheel);
        }
        // centre of mass flag update
        Vector3 thePosition = transform.TransformPoint(GetComponent<Rigidbody>().centerOfMass);
        massFlag.transform.position = thePosition;
    }

}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool isMotor;
    public bool isSteering;
    public bool isBraking = false;
}

