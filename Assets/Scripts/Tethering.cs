using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tethering : MonoBehaviour
{
    public TerrainHit tether;
    public float tetherForce = 250000f;
    public Transform centreOfMass;
    private float ropeLength;
    private bool deployed;
    private Vector3 ropeCentre;    
    private Vector3 playerPosition;
    private float playerSpeed;
    private Vector3 displacement;
    private Vector3 inwardForce;

    void Update()
    {
        deployed = tether.deployed;
        if (deployed == false) {
            return;
        }
        ropeLength = tether.ropeLength;
        ropeCentre = tether.targetPosition;
        playerPosition = GetComponent<Rigidbody>().transform.position;
        playerSpeed = GetComponent<Rigidbody>().velocity.magnitude;
        displacement = ropeCentre - playerPosition;

        float parallelSpeed = Vector3.Dot(GetComponent<Rigidbody>().velocity, displacement.normalized);
        Vector3 parallelVelocity = displacement.normalized * parallelSpeed;
        if (Vector3.Distance(ropeCentre,playerPosition) >= ropeLength 
            && Vector3.Dot(displacement.normalized, GetComponent<Rigidbody>().velocity.normalized)<0) // check player is outside tether radius and moving away
        {
            // rotate player velocity to be perpendicular to tether
            GetComponent<Rigidbody>().velocity = (GetComponent<Rigidbody>().velocity - parallelVelocity).normalized * playerSpeed;
            inwardForce = tetherForce * displacement.normalized;
            GetComponent<Rigidbody>().AddForceAtPosition(inwardForce, centreOfMass.position);
        }
        
    }
}
