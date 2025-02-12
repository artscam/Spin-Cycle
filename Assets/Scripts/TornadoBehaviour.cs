using UnityEngine;
using UnityEngine.AI;

public class TornadoBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer;
    public float timescale = 1F;
    public float speed = 30F;
    public Vector3 target;
    public GameObject bias;
    public float trackingStrength = 0.5F;
    public int rotation = -1;
    private NavMeshAgent agent;

    void Start()
    {
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit closestHit, 500, 1))
        {
            GetComponent<NavMeshAgent>().Warp(closestHit.position);
        }
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        if (agent != null)
        {
            agent.enabled = true;
            agent.speed = speed;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timescale)
        {
            NewTarget();
            timer = 0;
        }

    }

    void NewTarget()
    {
        float biasX = bias.transform.position.x - transform.position.x;
        float biasY = bias.transform.position.y - transform.position.y;
        float biasZ = bias.transform.position.z - transform.position.z;
        Vector3 biasVector = trackingStrength * new Vector3(biasX, biasY, biasZ);
        float distance = Random.Range(0, 100);

        target = RandomNavSphere(transform.position, biasVector, distance, -1);
        agent.SetDestination(target);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, Vector3 bias, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin+bias;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
