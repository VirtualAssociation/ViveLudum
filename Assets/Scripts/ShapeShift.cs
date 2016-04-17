using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShapeShift : MonoBehaviour {

    [HideInInspector]
    public string objName { get; private set; }

	// Use this for initialization
	void Start () {
        objName = this.transform.GetChild(0).gameObject.name;
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void ChangeScale(GameObject pnj)
    {
        float randomScale = Random.Range(2, 3);
        GameObject child = pnj.transform.GetChild(0).gameObject;
        child.transform.localScale *= randomScale;
    }

    public void ChangeBodyColor(GameObject pnj)
    {
        Debug.Log("ChangeBodyColor");
        GameObject body = pnj.transform.GetChild(0).GetChild(0).gameObject;
        ChangeObjectColor(body, Random.ColorHSV());
    }

    public void ChangeEyeColor(GameObject pnj)
    {
        Debug.Log("ChangeEyeColor");
        GameObject eye = pnj.transform.GetChild(0).GetChild(1).gameObject;
        ChangeObjectColor(eye, Random.ColorHSV());
    }

    public void ChangePupilColor(GameObject pnj)
    {
        Debug.Log("ChangePupilColor");
        GameObject pupil = pnj.transform.GetChild(0).GetChild(1).gameObject;
        ChangeObjectColor(pupil, Random.ColorHSV());
    }

    private void ChangeObjectColor(GameObject obj, Color color)
    {
        Debug.Log("ChangeObjectColor");
        Material mat = obj.GetComponent<Renderer>().material;
        mat.color = color;
    }
}