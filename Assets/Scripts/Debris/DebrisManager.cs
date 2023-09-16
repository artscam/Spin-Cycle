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
        //Debug.Log("number of debris = " + debris.Length);
        foreach (Debris d in debris)
        {
            d.Initialize(settings, tornado);
           // Debug.Log("axis = " + d.semiMajorAxis);
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
                  // Debug.Log("displacement = " + debris[i].displacementXZ);
                 //  Debug.Log("SMaM = " + debris[i].semiMajorAxis);
                debrisData[i].rotation = debris[i].transform.rotation;
            }
         //   Debug.Log("DebrisData size: " + DebrisData.Size);
            var debrisBuffer = new ComputeBuffer(numDebris, DebrisData.Size);
            debrisBuffer.SetData(debrisData);

            compute.SetBuffer(0, "debris", debrisBuffer);
            compute.SetFloat("_ElapsedTime", Time.deltaTime);
            compute.SetFloat("_MaxOrbitSpeed", settings.maxOrbitSpeed);
            compute.SetFloat("_MaxTumbleSpeed", settings.maxTumbleSpeed);


            int threadGroups = Mathf.CeilToInt(numDebris / (float)threadGroupSize);
         //   Debug.Log("number of thread groups: "+threadGroups);
            compute.Dispatch(0, threadGroups, 1, 1);

            debrisBuffer.GetData(debrisData);

            for (int i = 0; i < debris.Length; i++)
            {
                if (i == 3)
                {
                  //  Debug.Log("position 3 = " + debris[i].displacementXZ + ", buffer 3 = " + debrisData[i].displacementXZ + ", currentAngle 3 = " + debris[i].currentAngle);
                };

                debris[i].displacementXZ = debrisData[i].displacementXZ;
                debris[i].transform.rotation = debrisData[i].rotation;
                debris[i].UpdateDebris();
                
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