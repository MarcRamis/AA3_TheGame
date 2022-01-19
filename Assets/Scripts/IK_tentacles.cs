using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OctopusController;



public class IK_tentacles : MonoBehaviour
{

    [SerializeField]
    Transform[] _tentacles = new Transform[4];

    [SerializeField]
    Transform[] _randomTargets;

    [SerializeField] private GameObject[] randomTargetObj = new GameObject[4];

    MyOctopusController _myController = new MyOctopusController();
    


    [Header("Exercise 3")]
    [SerializeField, Range(0, 360)]
    float _twistMin ;

    [SerializeField, Range(0, 360)]
    float _twistMax;

    [SerializeField, Range(0, 360)]
    float _swingMin;

    [SerializeField, Range(0, 360)]
    float _swingMax;

    [SerializeField]
    bool _updateTwistSwingLimits = false;

    [HideInInspector] public int myShootCount;


    [SerializeField]
    float TwistMin{set{ _myController.TwistMin = value; }}

    [SerializeField] public Animation robot1Animation;
    [SerializeField] public Animation robot2Animation;
    [SerializeField] public Animation robot3Animation;
    [SerializeField] public Animation robot4Animation;

    #region public methods


    public void NotifyTarget(Transform target, Transform region)
    {
        _myController.NotifyTarget(target, region);

    }

    public void NotifyShoot()
    {
        _myController.NotifyShoot();
        myShootCount++;

        if (myShootCount % 2 == 0)
        {
            robot1Animation.Play("Disbelief");
            robot2Animation.Play("Disbelief");
            robot3Animation.Play("Victory");
            robot4Animation.Play("Victory");

            //robot1Animation.PlayQueued("Talking");
            //robot2Animation.PlayQueued("Talking");
            //robot3Animation.PlayQueued("Talking");
            //robot4Animation.PlayQueued("Talking");
        }
        else
        {
            robot1Animation.Play("Victory");
            robot2Animation.Play("Victory");
            robot3Animation.Play("Disbelief");
            robot4Animation.Play("Disbelief");

            //robot1Animation.PlayQueued("Talking");
            //robot2Animation.PlayQueued("Talking");
            //robot3Animation.PlayQueued("Talking");
            //robot4Animation.PlayQueued("Talking");
        }
    }


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        
        _myController.TestLogging(gameObject.name);
        _myController.Init(_tentacles, _randomTargets);

        _myController.TwistMax = _twistMax;
        _myController.TwistMin = _twistMin;
        _myController.SwingMax = _swingMax;
        _myController.SwingMin = _swingMin;

    }



    // Update is called once per frame
    void Update()
    {
        _myController.UpdateTentacles();

        if (_updateTwistSwingLimits) {
            _myController.TwistMax = _twistMax;
            _myController.TwistMin = _twistMin;
            _myController.SwingMax = _swingMax;
            _myController.SwingMin = _swingMin;
            _updateTwistSwingLimits = false;
        }

        robot1Animation.PlayQueued("Talking", QueueMode.CompleteOthers);
        robot2Animation.PlayQueued("Talking", QueueMode.CompleteOthers);
        robot3Animation.PlayQueued("Talking", QueueMode.CompleteOthers);
        robot4Animation.PlayQueued("Talking", QueueMode.CompleteOthers);
    }

    public void ResetTargetTentacles()
    {
        _myController.ResetBallHit();

        for(int i = 0; i < _randomTargets.Length; i++)
        {
            _randomTargets[i] = randomTargetObj[i].transform;
        }
        //_myController.ResetTentacles(_randomTargets);
    }

}
