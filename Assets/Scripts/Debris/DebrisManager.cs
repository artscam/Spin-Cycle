using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisManager : MonoBehaviour
{

    const int threadGroupSize = 1024;
    public Transform tornado;
    public DebrisSettings settings;
    public ComputeShader compute;
    Debris[] debris;

    void Start()
    {
        debris = FindObjectsOfType<Debris>();
        foreach (Debris d in debris)
        {
            d.Initialize(settings, tornado);
        }

    }

    void Update()
    {
        if (debris != null)
        {

            int numDebris = debris.Length;
            var debrisData = new DebrisData[numDebris];

            for (int i = 0; i < debris.Length; i++)
            {
                debrisData[i].displacementXZ = debris[i].displacementXZ;
                debrisData[i].rotation = debris[i].transform.rotation;
                debrisData[i].currentAngle = debris[i].currentAngle;
                debrisData[i].semiMajorAxis = debris[i].semiMajorAxis;
                debrisData[i].semiMinorAxis = debris[i].semiMinorAxis;
                debrisData[i].phase = debris[i].phase;
            }
            var debrisBuffer = new ComputeBuffer(numDebris, DebrisData.Size);
            debrisBuffer.SetData(debrisData);

            compute.SetBuffer(0, "debris", debrisBuffer);
            compute.SetFloat("_ElapsedTime", Time.deltaTime);
            compute.SetFloat("_MaxOrbitSpeed", settings.maxOrbitSpeed);
            compute.SetFloat("_MaxTumbleSpeed", settings.maxTumbleSpeed);


            int threadGroups = Mathf.CeilToInt(numDebris / (float)threadGroupSize);
            compute.Dispatch(0, threadGroups, 1, 1);
        
            debrisBuffer.GetData(debrisData);

            for (int i = 0; i < debris.Length; i++)
            {
            

                debris[i].displacementXZ = debrisData[i].displacementXZ;
                debris[i].transform.rotation = debrisData[i].rotation;
                debris[i].currentAngle = debrisData[i].currentAngle;
                debris[i].UpdateDebris();
                if (i == 3)
                {
                      //  Debug.Log("buffer rot 3 = " + debrisData[i].rotation);
                };
            }

            debrisBuffer.Release();
        }
    }

    public struct DebrisData
    {
        public Vector2 displacementXZ;
        public Quaternion rotation;
        public float semiMajorAxis;
        public float semiMinorAxis;
        public float phase;
        public float currentAngle;
        public static int Size
        {
            get
            {
                return sizeof(float) * (2 + 4 + 4);
            }
        }
    }
}