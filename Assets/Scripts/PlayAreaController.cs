using UnityEngine;

public class PlayAreaController : MonoBehaviour
{
	private GameTimer _timer;
	public GameObject NightSphere;
	public TextMesh Text;

	// Use this for initialization
	void Start ()
	{
		_timer = GetComponent<GameTimer>();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag != "DestructibleChild"){ return; }
		_timer.Stop();
		NightSphere.SetActive(true);
		Text.text = "YOU LOSE. Score: " + _timer.Cycles;
		Debug.Log("You lose");
	}
}
