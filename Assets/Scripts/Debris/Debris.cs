using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    [HideInInspector]
    public Vector2 displacementXZ;
    public Transform centre;
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public float radius;
    [HideInInspector]
    public float maxOrbitSpeed;
    [HideInInspector]
    public float maxTumbleSpeed;
    [HideInInspector]
    public float eccentricity;
    [HideInInspector]
    public float phase;
    [HideInInspector]
    public Vector3 centrePos;
    [HideInInspector]
    public float currentAngle;
    [HideInInspector]
    public float semiMajorAxis;
    [HideInInspector]
    public float semiMinorAxis;

    // Cached
    Material material;

    void Awake()
    {
        centrePos = centre.position;
        material = transform.GetComponentInChildren<MeshRenderer>().material;
    }

    public void Initialize(DebrisSettings settings, Transform centre)
    {
        this.centre = centre;
        eccentricity = Random.Range(0.6f, 0.90f);
        phase = Random.Range(0f, 2 * Mathf.PI);
        maxOrbitSpeed = settings.maxOrbitSpeed;
        maxTumbleSpeed = settings.maxTumbleSpeed;
        displacementXZ = new Vector2(centre.position.x - transform.position.x, centre.position.z - transform.position.z);
        radius = displacementXZ.magnitude;
        semiMajorAxis = displacementXZ.magnitude;
        semiMinorAxis = semiMajorAxis * Mathf.Pow(1 - Mathf.Pow(eccentricity, 2), 0.5f);
        currentAngle = Random.Range(0f, 2 * Mathf.PI);
    }

    public void SetColour(Color col)
    {
        if (material != null)
        {
            material.color = col;
        }
    }
    public void UpdateDebris()
    {
        transform.position = new Vector3(centre.position.x + displacementXZ.x, transform.position.y, centre.position.z + displacementXZ.y);
    }


}
