using UnityEngine;
using System.Collections;

public class PlayAreaController : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag != "DestructibleChild"){ return; }

		Debug.Log("YOU LOSE");
	}
}
