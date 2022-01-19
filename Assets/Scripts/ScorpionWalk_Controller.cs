using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionWalk_Controller : MonoBehaviour
{
    [SerializeField] Transform[] futureLegs = new Transform[6];

    [SerializeField] Transform[] currentLegsPos = new Transform[6];
    [SerializeField] Transform[] jointLegs02 = new Transform[6];
    float[] legsStartPos = new float[6];
    float[] legDifference = new float[6];
    [Range(0.0f, 2.0f)]
    [SerializeField] private float distanceFromLegPosFutPos = 0.8f;

    [SerializeField] private float distanceRayCast = 20.0f;
    [SerializeField] private float offsetRaycastHeight = 10.0f;
    [Header("Scorpion Walk Movement")]
    [Range(1.0f, 30.0f)]
    [SerializeField] private float rotationMultiplayer = 10.0f;
    [Range(0.0f, 30.0f)]
    [SerializeField] private float percentToGoUpMultiplayer = 1.0f;

    private float startHeight;

    private bool[] isLegInFloor = new bool[6];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < currentLegsPos.Length; i++)
        {
            legsStartPos[i] = currentLegsPos[i].position.y;
        }
        startHeight = transform.GetChild(0).position.y;

        for (int i = 0; i < isLegInFloor.Length; i++)
        {
            isLegInFloor[i] = true;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        foreach (Transform legFuturePos in futureLegs)
        {
            if (Physics.Raycast(legFuturePos.position + new Vector3(0, offsetRaycastHeight, 0), Vector3.down, out hit, distanceRayCast))
            {
                legFuturePos.position = hit.point;

            }
        }
        //CalculateLegDistanceWithFutpos();
        //RaycastHit hitWithCurrentLegs;
        //for (int i = 0; i < currentLegsPos.Length; i++)
        //{
        //    if (Physics.Raycast(currentLegsPos[i].position + new Vector3(0, offsetRaycastHeight, 0), Vector3.down, out hitWithCurrentLegs, distanceRayCast) && isLegInFloor[i])
        //    {
        //        currentLegsPos[i].position = new Vector3(currentLegsPos[i].position.x, hitWithCurrentLegs.point.y, currentLegsPos[i].position.z);

        //    }
        //}

        RotateScorpionBody();
        MoveScorpionBody();
    }

    private void RotateScorpionBody()
    {
        for (int i = 0; i < currentLegsPos.Length; i++)
        {
            legDifference[i] = currentLegsPos[i].position.y - legsStartPos[i];
        }
        float angleRotFront;
        float angleRotBack;
        float angleResult;
        ////Rotate in axis X
        //Legs front
        angleRotFront = (legDifference[0] + legDifference[1]) * rotationMultiplayer;

        //Legs back
        angleRotBack = (legDifference[4] + legDifference[5]) * rotationMultiplayer;

        angleResult = angleRotFront - angleRotBack;
        this.gameObject.transform.eulerAngles = new Vector3(angleResult, this.gameObject.transform.localRotation.eulerAngles.y, this.gameObject.transform.localRotation.eulerAngles.z);

        ////Rotate in axis Z
        float angleRotLeft;
        float angleRotRight;
        //Legs Left
        angleRotLeft = (legDifference[1] + legDifference[3] + legDifference[5]) * (rotationMultiplayer / 2);

        //Legs Right
        angleRotRight = (legDifference[0] + legDifference[2] + legDifference[4]) * (rotationMultiplayer / 2);

        angleResult = angleRotLeft - angleRotRight;
        this.gameObject.transform.GetChild(0).eulerAngles = new Vector3(this.gameObject.transform.localRotation.eulerAngles.x, this.gameObject.transform.localRotation.eulerAngles.y, angleResult);
    }

    private void MoveScorpionBody()
    {
        for (int i = 0; i < currentLegsPos.Length; i++)
        {
            legDifference[i] = currentLegsPos[i].position.y - legsStartPos[i];
        }
        float allLegsDifference = 0.0f;
        foreach (float dif in legDifference)
        {
            allLegsDifference += dif;
        }

        this.gameObject.transform.GetChild(0).GetChild(0).position = new Vector3(this.gameObject.transform.GetChild(0).GetChild(0).position.x, (startHeight + (allLegsDifference / 6) * percentToGoUpMultiplayer), this.gameObject.transform.GetChild(0).GetChild(0).position.z);
        this.gameObject.transform.GetChild(0).GetChild(1).position = new Vector3(this.gameObject.transform.GetChild(0).GetChild(1).position.x, (startHeight + (allLegsDifference / 6) * percentToGoUpMultiplayer), this.gameObject.transform.GetChild(0).GetChild(1).position.z);
        this.gameObject.transform.GetChild(0).GetChild(3).position = new Vector3(this.gameObject.transform.GetChild(0).GetChild(3).position.x, (startHeight + (allLegsDifference / 6) * percentToGoUpMultiplayer), this.gameObject.transform.GetChild(0).GetChild(3).position.z);

        
    }

    private void CalculateLegDistanceWithFutpos()
    {
        for (int i = 0; i < currentLegsPos.Length; i++)
        {
            if (Vector3.Distance(currentLegsPos[i].position, futureLegs[i].position) > distanceFromLegPosFutPos)
            {
                isLegInFloor[i] = false;
            }
            else if (Vector3.Distance(currentLegsPos[i].position, futureLegs[i].position) < 0.1f)
            {
                isLegInFloor[i] = true;
            }
        }
    }

    public void ResetLegsStartPos()
    {
        for(int i = 0; i < currentLegsPos.Length; i++)
        {
            currentLegsPos[i].localPosition = Vector3.zero;
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
