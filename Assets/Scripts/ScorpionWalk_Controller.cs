using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionWalk_Controller : MonoBehaviour
{
    [SerializeField] Transform[] futureLegs = new Transform[6];

    [SerializeField] private float distanceRayCast = 20.0f;
    [SerializeField] private float offsetRaycastHeight = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        foreach(Transform legFuturePos in futureLegs)
        {
            //if (Physics.Raycast(legFuturePos.position + new Vector3(0, offsetRaycastHeight, 0), Vector3.up, out hit, distanceRayCast))
            //{
            //    Debug.Log("Up: " + hit.point);
            //    Debug.Log(hit.transform.gameObject.name);
            //    legFuturePos.position = hit.point;
            //}
            if (Physics.Raycast(legFuturePos.position + new Vector3(0, offsetRaycastHeight, 0), Vector3.down, out hit, distanceRayCast/*Mathf.Infinity*/))
            {
                Debug.Log("Down: " + hit.point);
                Debug.Log(hit.transform.gameObject.name);
                legFuturePos.position = hit.point;

            }
        }
        

    }


    private void OnDrawGizmos()
    {
        foreach (Transform legFuturePos in futureLegs)
        {
            //Gizmos.color = Color.blue;
            //Gizmos.DrawLine(legFuturePos.position + new Vector3(0, offsetRaycastHeight, 0), legFuturePos.position + Vector3.up * distanceRayCast);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(legFuturePos.position + new Vector3(0, offsetRaycastHeight, 0), legFuturePos.position + Vector3.down * distanceRayCast);
        }

    }

}
