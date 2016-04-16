using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

    [SerializeField]
    private float _originalTimer1;

    [SerializeField]
    private float _originalTimer2;

    [SerializeField]
    private float _originalTimer3;

    [SerializeField]
    private GameObject _nightSphere;

    private float _timerPhase1;

    private float _timerPhase2;

    private float _timerPhase3;

    public bool _timerPhase1On = false;

    public bool _timerPhase2On = false;

    public bool _timerPhase3On = false;

	// Use this for initialization
	void Start () {
        _timerPhase1 = _originalTimer1;
        _timerPhase2 = _originalTimer2;
        _timerPhase3 = _originalTimer3;
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
                _nightSphere.SetActive(true); // passage nuit
                _timerPhase2On = true;

            }
        }

        if (_timerPhase2On == true)
        {
            _timerPhase2 -= Time.deltaTime;
            if (_timerPhase2 <= 0f)
            {
                // Déclenchement Nuit
                _timerPhase2On = false;
                _timerPhase2 = _originalTimer2;
                _nightSphere.SetActive(false); // passage jour
                _timerPhase3On = true;
            }
        }

        if (_timerPhase3On == true)
        {
            _timerPhase3 -= Time.deltaTime;
            if (_timerPhase3 <= 0f)
            {
                _timerPhase3On = false;
                _timerPhase3 = _originalTimer3;
                _timerPhase1On = true;
            }
        }
	}
}
