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

    private float _timerDay;
    private float _timerNight;
    private float _timerMorning;

    public bool _timerDayOn = false;
    public bool _timerNightOn = false;
    public bool _timerMorningOn = false;

    private PNJsController _pnjCtrl;

    private bool _soundPlaying = false;

	// Use this for initialization
	void Start () {
        _timerDay = _dayTime;
        _timerNight = _nightTime;
        _timerMorning = _morningTime;
        _pnjCtrl = this.GetComponent<PNJsController>();
    }
	
	// Update is called once per frame
	void Update () {
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
            if (_timerNight <= _nightTime / 2 && _soundPlaying == false)
            {
                _soundPlaying = true;
                _audSrcMainCam.Play();
                _pnjCtrl.ShapeShift();
                _pnjCtrl.MovePNJsCloser();
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
                _pnjCtrl.GenerateNewPNJs();
                MorningToDay();
            }
        }
	}

    void NightToMorning()
    {
        _timerNightOn = false;
        _timerNight = _nightTime;
        _timerMorningOn = true;
        _soundPlaying = false;
        _nightSphere.SetActive(false);
    }

    void DayToNight()
    {
        _timerDayOn = false;
        _timerDay = _dayTime;
        _timerNightOn = true;
        _nightSphere.SetActive(true);
    }

    void MorningToDay()
    {
        _timerMorningOn = false;
        _timerMorning = _morningTime;
        _timerDayOn = true;
    }
}
