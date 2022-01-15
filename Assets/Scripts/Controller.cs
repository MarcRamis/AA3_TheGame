using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Slider forceSlider;
    [Range(1.0f, 10.0f)]
    [SerializeField] private float velocityStrenghtBar = 5.0f;

    private bool isKeyPressed = false;
    private bool doOnceKeyPressed = false;

    private bool isStrenghtGoingUp = true;

    public GameObject ball = null;
    private Vector3 initialPos = new Vector3(0, 0, 0);
    private Vector3 ballVelocity = new Vector3(0, 0, 0);
    private Vector3 ballAcceleration = new Vector3(0, 0, 0);
    private Vector3 gravity = new Vector3(0.0f, 0.0f, 0.0f);

    [SerializeField] private GameObject ballTarget = null;
    private Vector3 initialBallTargetPos = new Vector3(0, 0, 0);

    [SerializeField] IK_Scorpion scorpionScript = null;

    [SerializeField] IK_tentacles octopusScript = null;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = ball.transform.position;
        initialBallTargetPos = ballTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeys();
        if(isKeyPressed && !doOnceKeyPressed)
        {
            ForceSliderLogic();
        }
    }

    private void FixedUpdate()
    {
        SolverEuler();
    }

    private void UpdateKeys()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isKeyPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isKeyPressed = false;
            doOnceKeyPressed = true;
            scorpionScript.StartWalk();
        }
    }

    private void ForceSliderLogic()
    {
        if(isStrenghtGoingUp)
        {
            if(forceSlider.value >= 700)
            {
                isStrenghtGoingUp = false;
                return;
            }
            forceSlider.value += velocityStrenghtBar;
        }
        else
        {
            if (forceSlider.value <= 0)
            {
                isStrenghtGoingUp = true;
                return;
            }
            forceSlider.value -= velocityStrenghtBar;
        }
        
    }
    public void SetStartForce()
    {
        Vector3 direction = ballTarget.transform.position - ball.transform.position;
        direction = direction.normalized;
        direction *= forceSlider.value;
        ballAcceleration = direction;
        gravity = new Vector3(0.0f, -9.81f, 0.0f);
    }
    private void SolverEuler()
    {
        ball.transform.position = ball.transform.position + ballVelocity * Time.deltaTime;
        ballVelocity = ballVelocity + (ballAcceleration + gravity) * Time.deltaTime;
    }

    public void Reset()
    {
        scorpionScript.ResetScorpion();

        ball.transform.position = initialPos;
        ballTarget.transform.position = initialBallTargetPos;
        ballVelocity = new Vector3(0, 0, 0);
        ballAcceleration = new Vector3(0, 0, 0);
        gravity = new Vector3(0.0f, 0.0f, 0.0f);

        octopusScript.ResetTargetTentacles();

        isStrenghtGoingUp = true;
        doOnceKeyPressed = false;
        forceSlider.value = 0;
    }
}
