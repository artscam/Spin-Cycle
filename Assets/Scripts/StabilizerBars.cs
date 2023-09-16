using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilizerBars : MonoBehaviour
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public float antiRoll = 500F;
    void FixedUpdate ()
    {
  
        float travelL = 1F;
        float travelR = 1F;

        bool groundedL = leftWheel.GetGroundHit(out WheelHit hit);
        if (groundedL)
        {
            travelL = (-leftWheel.transform.InverseTransformPoint(hit.point).y - leftWheel.radius) / leftWheel.suspensionDistance;
        }

        bool groundedR = rightWheel.GetGroundHit(out hit);
        if (groundedR)
        {
            travelR = (-rightWheel.transform.InverseTransformPoint(hit.point).y - rightWheel.radius) / rightWheel.suspensionDistance;
        }

        float antiRollForce = (travelL - travelR) * antiRoll;

        if (groundedL)
        {
            GetComponent<Rigidbody>().AddForceAtPosition(leftWheel.transform.up * -antiRollForce, leftWheel.transform.position);
        }
        if (groundedR)
        {
            GetComponent<Rigidbody>().AddForceAtPosition(rightWheel.transform.up * -antiRollForce, rightWheel.transform.position);
        }
    }


}
