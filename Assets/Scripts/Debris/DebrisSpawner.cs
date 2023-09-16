using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{

    public enum GizmoType { Never, SelectedOnly, Always }
    public Debris prefab;
    public float spawnRadius = 10;
    public int spawnCount = 10;
    public Color colour;
    public GizmoType showSpawnRegion;

    void Awake()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // Generate points within a stretched upside down hemisphere
            Vector3 noise = Random.insideUnitSphere;
            noise.y = 3 * (1- Mathf.Abs(noise.y));
            Vector3 pos = transform.position + noise * spawnRadius;
            Debris debris = Instantiate(prefab);     
            debris.transform.position = pos;
            debris.SetColour(colour);
        }
    }

    private void OnDrawGizmos()
    {
        if (showSpawnRegion == GizmoType.Always)
        {
            DrawGizmos();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showSpawnRegion == GizmoType.SelectedOnly)
        {
            DrawGizmos();
        }
    }

    void DrawGizmos()
    {

        Gizmos.color = new Color(colour.r, colour.g, colour.b, 0.3f);
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }

}