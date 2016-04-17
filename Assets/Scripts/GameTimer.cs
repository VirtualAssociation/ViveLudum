using UnityEngine;
using System.Collections;


public class GameTimer : MonoBehaviour {

    [SerializeField]
    private float _dayTime;

    [SerializeField]
    private float _nightTime;

    [SerializeField]
    private float _morningTime;

    [SerializeField]
    private GameObject _nightSphere;

    [SerializeField]
    private AudioSource _audSrcMainCam;

    [SerializeField]
    private AudioSource _audSrcNoon;

    [SerializeField]
    private AudioSource _audDay;

    [SerializeField]
    private AudioSource _audNight;

    [SerializeField]
    private AudioSource _audMorning;

    [SerializeField]
    private AudioClip[] _audListDay;

    [SerializeField]
    private LaserPointer _laserLeft;

    [SerializeField]
    private LaserPointer _laserRight;

    [SerializeField]
    private ChangeIcon _iconLeft;

    [SerializeField]
    private ChangeIcon _iconRight;

    private int _currentAfternoonTrack = 0;

    private float _timerDay;
    private float _timerNight;
    private float _timerMorning;

    public bool _timerDayOn = false;
    public bool _timerNightOn = false;
    public bool _timerMorningOn = false;

    public bool goToNext = false;

    private PNJsController _pnjCtrl;

	private int _nbOfCycles = 0;

	public int Cycles { get { return _nbOfCycles; } }

    private bool _soundPlaying = false;
    private bool _sound2PLaying = false;

	public TextMesh helpText;

    public bool happyAnimationStart = false;
    public bool angryAnimationStart = false;
    public float animationStartTime;

    public enum STEP {
        DAY,
        NIGHT,
        MORNING
    };

    public STEP step;

    // Use this for initialization
    void Start () {
    
        _timerDay = (_audDay.clip.length * ((100 / _audDay.pitch)/100));
        _audDay.Play();
        _timerNight = (_audNight.clip.length * ((100 / _audNight.pitch) / 100));

        _timerMorning = (_audMorning.clip.length * ((100 / _audMorning.pitch) / 100));
        _timerMorning = _morningTime;
        _pnjCtrl = this.GetComponent<PNJsController>();

        step = STEP.DAY;
    }
	
	// Update is called once per frame
	void Update () {

        if (happyAnimationStart)
        {
            if (Time.time - animationStartTime <= 2.0)
            {
                foreach (GameObject pnj in _pnjCtrl.Pnjs)
                {
                    pnj.transform.position += new Vector3(0.0f, 0.005f * Mathf.Cos(3.14f * 4f * (Time.time - animationStartTime)), 0.0f);
                }
            }
        }

        _pnjCtrl.ReorientPNJs();

        if (_timerDayOn)
        {
            step = STEP.DAY;
            _timerDay -= Time.deltaTime;
            if (_timerDay <= 0f)
            {
                DayToNight();
            }

			// if first cycle
			if (_nbOfCycles == 0)
            {
                helpText.text = "Memorize the shapes and colors of the blobs.";
			}
        }

        if (_timerNightOn)
        {
            step = STEP.NIGHT;
            _timerNight -= Time.deltaTime;

            if (_timerNight <= _nightTime / 2f && _soundPlaying == false)
            {
                _soundPlaying = true;
                _pnjCtrl.ShapeShift(_nbOfCycles+1);
            }

            if (_timerNight <= _nightTime /4f && _sound2PLaying == false)
            {
                _audSrcMainCam.Play();
                _sound2PLaying = true;
            }

            if (_timerNight <= 0f)
            {
                NightToMorning();
            }
			// if first cycle
			if (_nbOfCycles == 0)
            {
                helpText.text = "Be careful, the boss can make noise during the night.";
			}
        }

        if (_timerMorningOn)
        {
            step = STEP.MORNING;
            _timerMorning -= Time.deltaTime;
            if (_timerMorning <= 0f || goToNext)
            {
                MorningToDay();
            }
			// if first cycle
			if (_nbOfCycles == 0)
            {
                helpText.text = "Eliminate the blob which has changed or they will all move forward!";
			}
        }
	}

	public void Stop()
	{
		_timerDayOn = false;
		_timerNightOn = false;
		_timerMorningOn = false;
	}

    void NightToMorning()
    {
        _iconLeft.ChangePhase();
        _iconRight.ChangePhase();

        _timerNightOn = false;
        _timerNight = _nightTime;
        _timerMorningOn = true;
        _timerMorning = (_audMorning.clip.length * ((100 / _audMorning.pitch) / 100));
        _soundPlaying = false;
        _sound2PLaying = false;
        _nightSphere.SetActive(false);
        _audMorning.Play();

        _pnjCtrl.goodKill = false;
    }

    void DayToNight()
    {

        _iconLeft.ChangePhase();
        _iconRight.ChangePhase();
        
        _timerDayOn = false;
        _timerDay = _dayTime;
        _timerNightOn = true;
        _nightSphere.SetActive(true);
        _timerNight = (_audNight.clip.length * ((100 / _audNight.pitch) / 100));
        _audNight.Play();
    }

    void MorningToDay()
    {
        goToNext = false;
        _iconLeft.ChangePhase();
        _iconRight.ChangePhase();
        
        if (goToNext)
        {
            _audMorning.Stop();
            _timerMorning = 0;
        }
        else
        {
            _timerMorning = _morningTime;
        }
        
        _audSrcNoon.Play();
        _timerMorningOn = false;
        _timerDayOn = true;

        _pnjCtrl.GenerateNewPNJs();

        if (!_pnjCtrl.goodKill)
            _pnjCtrl.MovePNJsCloser();

        if (_currentAfternoonTrack < 15)
        {
            _audDay.clip = _audListDay[_currentAfternoonTrack + 1];
            _currentAfternoonTrack += 1;
        }

        _timerDay = (_audDay.clip.length * ((100 / _audDay.pitch) / 100));
        _audDay.Play();
        _nbOfCycles++;
        Object.Destroy(helpText);
	}
}
