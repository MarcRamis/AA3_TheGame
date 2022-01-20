using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    
    [Range(0.01f, 10.0f)]
    [SerializeField] private float velocityStrenghtBar = 5.0f;
    [Range(0.01f, 10.0f)]
    [SerializeField] private float velocityEffectBar = 5.0f;

    private bool isKeyPressed = false;
    private bool doOnceKeyPressed = false;
    //ForceSlider
    private bool isStrenghtGoingUp = true;
    public Slider forceSlider;

    //MagnusSlider
    public Slider effectSlider;
    [Header("Ball")]
    public GameObject ball = null;
    private Vector3 initialPos = new Vector3(0, 0, 0);
    private Vector3 ballVelocity = new Vector3(0, 0, 0);
    private Vector3 ballAcceleration = new Vector3(0, 0, 0);
    private Vector3 gravity = new Vector3(0.0f, 0.0f, 0.0f);
    private float difHorizontalPos = 0.0f;
    private Vector3 hitDirection = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] private float minPos = 0.00178f;
    [Space]

    [SerializeField] private GameObject ballTarget = null;
    private Vector3 initialBallTargetPos = new Vector3(0, 0, 0);

    //Scripts
    [Header("Scripts")]
    [SerializeField] IK_Scorpion scorpionScript = null;

    [SerializeField] IK_tentacles octopusScript = null;

    [SerializeField] ScorpionWalk_Controller scorpionWalContr = null;

    [SerializeField] MagnusEffect magnusEffect = null;

    [SerializeField] MovingBall ballScript = null;

    private Vector3 magnusForce = Vector3.zero;

    //Arrows GUI
    private bool arrowsVisible = true;
    private bool setInitialVelocityArrow = false;

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
        MagnusSliderLogic();
        if(doOnceKeyPressed && ballScript.updateArrows)
        {
            UpdateArrows();
        }
        
    }

    private void FixedUpdate()
    {
        //magnusForce = magnusEffect.MagnusForce(ballVelocity);
        magnusForce = magnusEffect.MagnusForceWithVelocity(ballVelocity, 0.0017f) * effectSlider.value;
        SolverEuler(ballAcceleration + gravity + magnusForce);
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

        if(Input.GetKeyDown(KeyCode.I))
        {
            if(arrowsVisible)
            {
                ballScript.SetArrowVisible(false);
                ballScript.SetStateLine();
                arrowsVisible = false;
            }
            else
            {
                ballScript.SetArrowVisible(true);
                ballScript.SetStateLine();
                arrowsVisible = true;
            }
        }

    }

    private void ForceSliderLogic()
    {
        if(isStrenghtGoingUp)
        {
            if(forceSlider.value >= forceSlider.maxValue)
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
    private void MagnusSliderLogic()
    {
        if (effectSlider.value < effectSlider.maxValue && Input.GetKey(KeyCode.X))
        {
            effectSlider.value += velocityEffectBar;
        }  
        else if (forceSlider.value >= 0 && Input.GetKey(KeyCode.Z))
        {
            effectSlider.value -= velocityEffectBar;
        }
    }
    public void SetStartForce()
    {
        Vector3 direction = ballTarget.transform.position - ball.transform.position;
        direction = direction.normalized;
        direction *= forceSlider.value;
        ballAcceleration = direction;
        gravity = new Vector3(0.0f, -1f, 0.0f);
        
    }

    public void ResetForce()
    {
        //ballAcceleration = Vector3.zero;
    }

    private void SolverEuler(Vector3 _force)
    {
        ball.transform.position = ball.transform.position + ballVelocity * Time.deltaTime;
        ballVelocity = ballVelocity + _force * Time.deltaTime;
    }

    private void UpdateArrows()
    {
        ballScript.velocityArrowDirection = ballVelocity.normalized;
        if(!setInitialVelocityArrow && ballVelocity.normalized != Vector3.zero)
        {
            ballScript.initialVelocityArrowDirection = ballVelocity.normalized;
            setInitialVelocityArrow = true;
        }
        
        ballScript.forceArrowDirection = Vector3.Normalize(ballAcceleration + gravity + magnusForce);
    }

    public void Reset()
    {
        scorpionScript.ResetScorpion();
        scorpionWalContr.ResetLegsStartPos();

        ball.transform.position = initialPos;
        ballTarget.transform.position = initialBallTargetPos;
        ballVelocity = new Vector3(0, 0, 0);
        ballAcceleration = new Vector3(0, 0, 0);
        gravity = new Vector3(0.0f, 0.0f, 0.0f);

        octopusScript.ResetTargetTentacles();

        isStrenghtGoingUp = true;
        doOnceKeyPressed = false;
        forceSlider.value = 0;

        setInitialVelocityArrow = false;
        ballScript.ResetArrows();

        magnusEffect.doOnce = false;
    }
}
