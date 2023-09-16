using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TornadoBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public float newtarget = 5F;
    public float speed = 10F;
    public NavMeshAgent nav;
    public Vector3 target;
    public GameObject bias;
    public float trackingStrength = 0.5F;

    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        nav.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= newtarget)
        {
            newTarget();
            timer = 0;
        }

    }

    void newTarget()
    {
        float myX = gameObject.transform.position.x;
        float myZ = gameObject.transform.position.z;

        float biasX = bias.transform.position.x-myX;
        float biasZ = bias.transform.position.z-myZ;

        float xPos = myX + Random.Range(myX - 100, myX + 100) + biasX/trackingStrength;
        float zPos = myZ + Random.Range(myZ - 100, myZ + 100) + biasZ/trackingStrength;

        target = new Vector3(xPos, gameObject.transform.position.y, zPos);

        nav.SetDestination(target);
    }
}
