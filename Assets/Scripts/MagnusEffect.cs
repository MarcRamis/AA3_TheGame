using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnusEffect : MonoBehaviour
{
    [Range(0.0f, 30.0f)]
    [SerializeField] private float airDensity = 1.2f;
    [SerializeField] private float dragCoefficient = 0.5f;
    [SerializeField] private float cilinderRadius = 5e-05f;
    [SerializeField] private float density = 1.2f;
    [SerializeField] private Vector3 effectDirection = Vector3.right + Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }

    public Vector3 MagnusForce(Vector3 _angularVelocity)
    {
        float angularVelocity = new Vector3(_angularVelocity.x, 0, _angularVelocity.z).magnitude;
        float forceM = (dragCoefficient * density * cilinderRadius * Mathf.Pow(angularVelocity, 2f)) / 2;
        return effectDirection * forceM;
    }
}
