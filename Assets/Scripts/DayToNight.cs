using UnityEngine;
using System.Collections;

public class DayToNight : MonoBehaviour {

    public float nightDuration = 3.0f;
    private float _start;
	// Use this for initialization
	void Start () {
        _start = Time.time;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float perc = Time.time - _start / nightDuration;
        GameObject nightQuad = GameObject.Find("CameraNightQuad");

    }

    private void UpdatePNJs()
    {
        
    }
}
