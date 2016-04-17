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

    private int _currentAfternoonTrack = 0;

    private float _timerDay;
    private float _timerNight;
    private float _timerMorning;

    public bool _timerDayOn = false;
    public bool _timerNightOn = false;
    public bool _timerMorningOn = false;

	public LaserPointer[] _laserPointers; 

    private PNJsController _pnjCtrl;

	private int _nbOfCycles = 0;

	public int Cycles { get { return _nbOfCycles; } }

    private bool _soundPlaying = false;
    private bool _sound2PLaying = false;
    

	// Use this for initialization
	void Start () {
        //_timerDay = _dayTime;
        _timerDay = (_audDay.clip.length * ((100 / _audDay.pitch)/100));
        _audDay.Play();
        _timerNight = (_audNight.clip.length * ((100 / _audNight.pitch) / 100));
        //_timerNight = _nightTime;
        _timerMorning = (_audMorning.clip.length * ((100 / _audMorning.pitch) / 100));
        _timerMorning = _morningTime;
        _pnjCtrl = this.GetComponent<PNJsController>();
		_laserPointers = this.GetComponentsInChildren<LaserPointer>();
	}
	
	// Update is called once per frame
	void Update () {

        _pnjCtrl.ReorientPNJs();

        if (_timerDayOn)
        {
            _timerDay -= Time.deltaTime;
            if (_timerDay <= 0f)
            {
                DayToNight();
            }
        }

        if (_timerNightOn)
        {
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
        }

        if (_timerMorningOn)
        {
            _timerMorning -= Time.deltaTime;
            if (_timerMorning <= 0f)
            {
                _pnjCtrl.newPNJCount = _nbOfCycles / 2;
                MorningToDay();
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
        _timerNightOn = false;
        _timerNight = _nightTime;
        _timerMorningOn = true;
        _timerMorning = (_audMorning.clip.length * ((100 / _audMorning.pitch) / 100));
        _soundPlaying = false;
        _sound2PLaying = false;
        _nightSphere.SetActive(false);
        _audMorning.Play();
		foreach(LaserPointer laser in _laserPointers)
		{
			laser.enabled = true;
		}
    }

    void DayToNight()
    {
	    _nbOfCycles++;
        _timerDayOn = false;
        _timerDay = _dayTime;
        _timerNightOn = true;
        _nightSphere.SetActive(true);
        _timerNight = (_audNight.clip.length * ((100 / _audNight.pitch) / 100));
        _audNight.Play();
    }

    void MorningToDay()
    {
        _audSrcNoon.Play();
        _timerMorningOn = false;
        _timerMorning = _morningTime;
        _timerDayOn = true;

        _pnjCtrl.GenerateNewPNJs();
        _pnjCtrl.MovePNJsCloser();

        if (_currentAfternoonTrack < 15)
        {
            _audDay.clip = _audListDay[_currentAfternoonTrack + 1];
            _currentAfternoonTrack += 1;
        }
        _timerDay = (_audDay.clip.length * ((100 / _audDay.pitch) / 100));
        _audDay.Play();
		foreach (LaserPointer laser in _laserPointers)
		{
			laser.enabled = false;
		}
	}
}
