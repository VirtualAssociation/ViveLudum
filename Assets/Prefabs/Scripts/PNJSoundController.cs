using UnityEngine;
using System.Collections;

public class PNJSoundController : MonoBehaviour {

    [SerializeField]
    private AudioClip[] _pnjAudList;
    private AudioClip _pnjAud;
    private AudioSource _audSrc;
    private int _currentAudIndex;
    private int _randomIndex;
    private bool _test = true;

    [SerializeField]
    private AudioClip[] _pnjAudListNight;
    private PNJ _pnj;
    private float _timerAudNight;
    private bool _timerAudNightOn = false;

	// Use this for initialization
	void Start () {
        _audSrc = this.GetComponent<AudioSource>();
        _currentAudIndex = Random.Range(0, _pnjAudList.Length - 1);
        _pnjAud = _pnjAudList[_currentAudIndex];
        _audSrc.clip = _pnjAud;


    }
	
	// Update is called once per frame
	void Update () {
        if (_timerAudNightOn == true)
        {
            _timerAudNight -= Time.deltaTime;
            if (_timerAudNight <= 0f)
            {
                _audSrc.clip = _pnjAudList[_currentAudIndex];
                _timerAudNightOn = false;
            }
        }
	}

    public void PlayMonsterSound()
    {
        _audSrc.Play();
    }

    public void ChangeMonsterSound()
    {
        _randomIndex = Random.Range(0, _pnjAudList.Length - 1);
        if (_randomIndex != _currentAudIndex)
        {
            _pnjAud = _pnjAudList[_randomIndex];
            _currentAudIndex = _randomIndex;
        }
        else ChangeMonsterSound();
    }

    public void ChangeMonsterSound(int index)
    {
        _pnjAud = _pnjAudList[index];
        _currentAudIndex = index;
    }

    public void PlayNightSound()
    {
        if (_pnj.IsBad == true)
        {
            _audSrc.clip = _pnjAudListNight[Random.Range(0, _pnjAudListNight.Length - 1)];
            _timerAudNight = _audSrc.clip.length;
            _timerAudNightOn = true;
        }
    }
}
