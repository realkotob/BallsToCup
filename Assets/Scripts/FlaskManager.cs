using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskManager : GenericSingleton<FlaskManager>
{
    public GameObject colliderPrefab;
    public Transform colliderParent;
    public int numberOfColliders = 500;
    float colliderDistanceFromCenter = 1.2f;
    float requestedRotation = 0;
    Rigidbody flaskRigidbody;
    public Transform containerCenter;
    public float maxDistanceFromCenter = 0.9f;
    void Start()
    {
        flaskRigidbody = GetComponent<Rigidbody>();
        // spawnCollidersOnSphereSurface();
    }

    void spawnCollidersOnSphereSurface()
    {
        var points = fibonacci_sphere(numberOfColliders);
        for (int i = 0; i < numberOfColliders; i++)
        {
            var rotationToMatchPosition = Quaternion.FromToRotation(Vector3.forward, points[i]);
            var collider = Instantiate(colliderPrefab, points[i] * colliderDistanceFromCenter + colliderParent.position,
                                       rotationToMatchPosition, colliderParent);
        }
    }

    Vector3[] fibonacci_sphere(int samples)
    {
        var points = new Vector3[samples];
        var phi = Mathf.PI * (3f - Mathf.Sqrt(5f));

        for (var i = 0; i < samples; i++)
        {
            var y = 1f - (i / (samples - 1f)) * 2f;
            var radius = Mathf.Sqrt(1f - y * y);

            var theta = phi * i;

            var x = Mathf.Cos(theta) * radius;
            var z = Mathf.Sin(theta) * radius;

            points[i] = new Vector3(x, y, z);
        }

        return points;
    }

    public void addRotation(float rotation)
    {
        requestedRotation += rotation;
    }

    public float useRotation()
    {
        float rotation = requestedRotation;
        requestedRotation = 0;
        return rotation;
    }

    public void FixedUpdate()
    {
        float v = useRotation();
        if (v != 0)
        {
            // print("rotating " + v);
            transform.Rotate(0, 0, v);
            // flaskRigidbody.AddTorque(transform.forward * v);
        }
    }
}
