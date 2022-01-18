using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBall : MonoBehaviour
{
    [SerializeField]
    IK_tentacles _myOctopus;

    [SerializeField] private Controller mySceneController;

    [SerializeField] private Transform blueTarget;
    [SerializeField] private Transform redBallPosition;

    //movement speed in units per second
    [Range(-1.0f, 1.0f)]
    [SerializeField]
    private float _movementSpeed = 5f;

    [HideInInspector] public bool updateArrows = false;
    private bool arrowVisible = true;

    Vector3 _dir;

    //Arrows
    [Header("Arrows")]
    [SerializeField] private GameObject arrowInitialVelocity;
    [SerializeField] private GameObject arrowVelocity;
    [SerializeField] private GameObject arrowforce;

    [SerializeField] private GameObject[] arrows;

    [SerializeField] private LineRenderer greyLine;

    public Vector3 forceArrowDirection = Vector3.forward;

    public Vector3 initialVelocityArrowDirection = Vector3.forward;

    public Vector3 velocityArrowDirection = Vector3.forward;

    private Vector3 ballInitialPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        ballInitialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;

        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        //get the Input from Vertical axis
        float verticalInput = Input.GetAxis("Vertical");

        //update the position
        //transform.position = transform.position + new Vector3(-horizontalInput * _movementSpeed * Time.deltaTime, verticalInput * _movementSpeed * Time.deltaTime, 0);
        Vector3 direction = transform.position - blueTarget.position;
        direction = direction.normalized;

        redBallPosition.localPosition = direction * 0.0015f;

        CalculateGreyLine();
        SetArrowsDirections();

        if(updateArrows && arrowVisible)
        {
            TurnOnArrows();
        }
        else
        {
            TurnOffArrows();
        }

    }

    public void TurnOnArrows()
    {
        if(arrows != null)
        {
            foreach(GameObject arrow in arrows)
            {
                arrow.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
    public void TurnOffArrows()
    {
        if (arrows != null)
        {
            foreach (GameObject arrow in arrows)
            {
                arrow.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void CalculateGreyLine()
    {
        greyLine.SetPosition(0, ballInitialPos);
        greyLine.SetPosition(1, blueTarget.position);
    }

    public void ResetArrows()
    {
        updateArrows = false;
        if (arrows != null)
        {
            foreach (GameObject arrow in arrows)
            {
                arrow.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private void SetArrowsDirections()
    {
        arrowInitialVelocity.transform.rotation = Quaternion.LookRotation(initialVelocityArrowDirection);
        arrowVelocity.transform.rotation = Quaternion.LookRotation(velocityArrowDirection);
        arrowforce.transform.rotation = Quaternion.LookRotation(forceArrowDirection);
    }

    public void SetStateLine()
    {
        greyLine.enabled = !greyLine.enabled;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.root.gameObject.tag == "Player")
        {
            mySceneController.SetStartForce();
            _myOctopus.NotifyShoot();
            updateArrows = true;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.root.gameObject.tag == "Player")
        {
            mySceneController.ResetForce();
        }
    }

    public void SetArrowVisible(bool _value)
    {
        arrowVisible = _value;
    }

}
