using UnityEngine;
using System.Collections;

public class ChangeIcon : MonoBehaviour {

    [SerializeField]
    private Sprite _day;

    [SerializeField]
    private Sprite _night;

    [SerializeField]
    private Sprite _morning;

    [SerializeField]
    private SpriteRenderer _sprRend;

	// Use this for initialization
	void Start () {
        //_sprRend = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangePhase()
    {
        if (_sprRend.sprite == _day)
        {
            _sprRend.sprite = _night;
        }
        else if (_sprRend.sprite == _night)
        {
            _sprRend.sprite = _morning;
        }
        else if (_sprRend.sprite == _morning)
        {
            _sprRend.sprite = _day;
        }
    }
}
