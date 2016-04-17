using UnityEngine;

public class PlayAreaController : MonoBehaviour
{
	private GameTimer _timer;

	// Use this for initialization
	void Start ()
	{
		_timer = GetComponent<GameTimer>();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag != "DestructibleChild"){ return; }

		Debug.Log("YOU LOSE. Score: " + _timer.Cycles);
	}
}
