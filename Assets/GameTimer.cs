using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {


    [SerializeField]
    private float _originalTimer1;
    [SerializeField]
    private float _originalTimer2;
    private float _timerPhase1;
    private float _timerPhase2;
    public bool _timerPhase1On = false;
    public bool _timerPhase2On = false;



	// Use this for initialization
	void Start () {
        _timerPhase1 = _originalTimer1;
        _timerPhase2 = _originalTimer2;
    }
	
	// Update is called once per frame
	void Update () {
	    if (_timerPhase1On == true)
        {
            _timerPhase1 -= Time.deltaTime;
            if (_timerPhase1 <= 0f)
            {
                _timerPhase1On = false;
                _timerPhase1 = _originalTimer1;

                ///Déclenchement Jour
            }
        }
        else
        {
            _timerPhase1 = _originalTimer1;
        }

        if (_timerPhase2On == true)
        {
            _timerPhase2 -= Time.deltaTime;
            if (_timerPhase2 <= 0f)
            {
                // Déclenchement Nuit
                _timerPhase2On = false;
                _timerPhase2 = _originalTimer2;
            }
        }
        else
        {
            _timerPhase2 = _originalTimer2;
        }
	}
}
