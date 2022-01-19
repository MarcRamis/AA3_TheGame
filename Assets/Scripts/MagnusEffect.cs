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
    [SerializeField] private Vector3 effectDirection = Vector3.down;

    [HideInInspector] public bool doOnce = false;

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

    public Vector3 MagnusForceWithVelocity(Vector3 _velocity, float _radius)
    {
        //Vector3 angularVelocity = _velocity / (2.0f * Mathf.PI * _radius);
        if(_velocity != Vector3.zero && !doOnce)
        {
            if(_velocity.x < 0.0f)
            {
                effectDirection = Vector3.down;
                doOnce = true;
            }
            else
            {
                effectDirection = Vector3.up;
                doOnce = true;
            }
        }
        Debug.Log(_velocity.x + _velocity.z);
        return Vector3.Cross(effectDirection, _velocity);
    }
}
