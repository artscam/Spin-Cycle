using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebrisBehaviour : MonoBehaviour
{
    public Transform centre;
    public float maxOrbitSpeed  = 600.0f;
    public float maxTumbleSpeed = 700.0f;
    public float eccentricity = 0.0f;
    public float phase = 0.0f; //vary betwwen 0 and 1
    private float currentAngle = 0.0f;
    private float semiMajorAxis;
    private float semiMinorAxis;
    private float phaseRadians;
    private void Start()
    {
        Vector2 displacementXZ = new Vector2(centre.position.x - transform.position.x, centre.position.z - transform.position.z);
        float distance = displacementXZ.magnitude;
        semiMajorAxis = distance;
        semiMinorAxis = distance * Mathf.Pow(1 - Mathf.Pow(eccentricity, 2), 0.5f);
        float phaseRadians = Mathf.Clamp(phase,0,1) * 2 * Mathf.PI;
    }
    void Update()
    {
        if (centre != null)
        {
            Vector2 displacementXZ = new Vector2(centre.position.x - transform.position.x, centre.position.z - transform.position.z);
            float distance = displacementXZ.magnitude;

            // Calculate the rotation angle based on the orbit speed
            float rotationAngle = maxOrbitSpeed / (distance + 1) * Time.deltaTime;
            currentAngle += rotationAngle;

            // Calculate the position of the orbitingObject in the elliptical orbit
            float x = centre.position.x + semiMajorAxis * Mathf.Cos(currentAngle * Mathf.Deg2Rad+phaseRadians);
            float z = centre.position.z + semiMinorAxis * Mathf.Sin(currentAngle * Mathf.Deg2Rad+phaseRadians);

            // Set the new position of the orbitingObject
            transform.position = new Vector3(x, transform.position.y, z);

            float tumbleRotation = (maxTumbleSpeed / (distance + 1)) * Time.deltaTime;
            transform.Rotate(Vector3.left, tumbleRotation, Space.Self);
        }
    }
}
