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
    public float currentAngle = 0.0f;
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

    private void Start()
    {
        displacementXZ = new Vector2(centre.position.x - transform.position.x, centre.position.z - transform.position.z);
        radius = displacementXZ.magnitude;
        semiMajorAxis = radius;
        semiMinorAxis = radius * Mathf.Pow(1 - Mathf.Pow(eccentricity, 2), 0.5f);
    }

    public void Initialize(DebrisSettings settings, Transform centre)
    {
        this.centre = centre;
        eccentricity = Random.Range(0.6f, 0.90f);
        phase = Random.Range(0f, 2 * Mathf.PI);
        maxOrbitSpeed = settings.maxOrbitSpeed;
        maxTumbleSpeed = settings.maxTumbleSpeed;
        displacementXZ = new Vector2(centre.position.x - transform.position.x, centre.position.z - transform.position.z);
        semiMajorAxis = displacementXZ.magnitude;
        semiMinorAxis = semiMajorAxis * Mathf.Pow(1 - Mathf.Pow(eccentricity, 2), 0.5f);
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
       // transform.position = new Vector3(displacementXZ.x+centrePos.x, this.position.y, displacementXZ.y+centrePos.z);
    }
    public void Update()
    {
        if (centre != null)
        {
            displacementXZ = new Vector2(centrePos.x - transform.position.x, centrePos.z - transform.position.z);
            radius = displacementXZ.magnitude;

            currentAngle += maxOrbitSpeed / (radius + 1) * Time.deltaTime;

            // Calculate the position of the orbitingObject in the elliptical orbit
            float x = centre.position.x + semiMajorAxis * Mathf.Cos(currentAngle + (Mathf.Deg2Rad * phase));
            float z = centre.position.z + semiMinorAxis * Mathf.Sin(currentAngle + (Mathf.Deg2Rad * phase));

            transform.position = new Vector3(x, transform.position.y, z);

            float tumbleRotation = (maxTumbleSpeed / (radius + 1)) * Time.deltaTime;
            transform.Rotate(Vector3.left, tumbleRotation, Space.Self);
        }

    }

}
