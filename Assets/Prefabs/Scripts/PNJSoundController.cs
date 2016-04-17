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

	// Use this for initialization
	void Start () {
        _audSrc = this.GetComponent<AudioSource>();
        _currentAudIndex = Random.Range(0, _pnjAudList.Length - 1);
        _pnjAud = _pnjAudList[_currentAudIndex];
        _audSrc.clip = _pnjAud;


    }
	
	// Update is called once per frame
	void Update () {
	    if (_test == true)
        {
            _test = false;
            
        }
	}

    public void PlayMonsterSound()
    {
        _audSrc.clip = _pnjAud;
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
}
