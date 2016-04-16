using UnityEngine;
using System.Collections;

public class PNJ : MonoBehaviour {

    public Color Color;

	// Use this for initialization
	void Start () {
        Renderer renderer = this.GetComponent<Renderer>();
        renderer.material.color = Color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
