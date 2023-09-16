using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoForce : MonoBehaviour
{
    // compute force on attached object
    public GameObject tornado;
    public float maxStrength = 10000;
    public float radius = 40;
    public float height = 20;

    Rigidbody car;
    public GameObject centreOfLift;
    Vector3 tornadoForce;
    float proximity;
    float localStrength;

    // Start is called before the first frame update
    void Start()
    {
        car = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 displacement = new Vector2(tornado.transform.position.x - car.position.x, tornado.transform.position.z - car.position.z);
        proximity = displacement.magnitude;

        if ((proximity < 2*radius) && (car.position.y < tornado.transform.position.y + height))
        {
            //localStrength = maxStrength * Mathf.Exp(-Mathf.Pow(proximity, 2) / (2 * Mathf.Pow(radius,2)));
            localStrength = maxStrength / (1+Mathf.Pow(proximity/radius, 2));
            tornadoForce.y = 1.5f*localStrength;
            // Create a radial inward force + slight perpendicular anticlockwise acceleration 
            tornadoForce.x = localStrength * (displacement.x + displacement.y)/ proximity;
            tornadoForce.z = localStrength * (displacement.y - displacement.x) / proximity;
            car.AddForce(tornadoForce);
            car.AddForceAtPosition(tornadoForce,centreOfLift.transform.position);
        }
    }
}
