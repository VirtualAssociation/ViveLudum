using UnityEngine;
using System.Collections;

public class CheersScript : MonoBehaviour {

    [SerializeField]
    private AudioClip _audCheersG;
    [SerializeField]
    private AudioClip _audCheersB;
    [SerializeField]
    private AudioSource _audSrc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoodCheers()
    {
        _audSrc.clip = _audCheersG;
        _audSrc.Play();
    }

    public void BadCheers()
    {
        _audSrc.clip = _audCheersB;
        _audSrc.Play();
    }
}
