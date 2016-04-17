using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShapeShift : MonoBehaviour {

    [HideInInspector]
    public string objName { get; private set; }

    [SerializeField]
    private GameObject[] _monsters;

	// Use this for initialization
	void Start () {

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

    public void ChangeBodyForm(GameObject pnj)
    {
        Random.seed = (int)Time.time;

        GameObject currentMonster = pnj.transform.GetChild(0).gameObject;

        // Find another monster
        GameObject otherMonster = null;

        bool indexOK = false;
        int otherMonsterIndex = 0;
        while (!indexOK)
        {
            Random.seed = (int)Time.time;
            int index = Random.Range(1, 6);
            if (currentMonster.name != "Monster_" + otherMonsterIndex)
            {
                indexOK = true;
                otherMonsterIndex = index;
            }
        }

        Debug.Log("from " + currentMonster.name + " to Monster_" + otherMonsterIndex);

        // Instanciate Monster
        foreach (GameObject m in _monsters)
        {
            if (m.name != currentMonster.name)
            {
                otherMonster = GameObject.Instantiate(m);
                break;
            }
        }

        GameObject currentBody = currentMonster.transform.GetChild(0).gameObject;
        GameObject otherBody = otherMonster.transform.GetChild(0).gameObject;

        // Apply current material to the new monster body
        Material currentMaterial = currentBody.GetComponent<Renderer>().material;
        otherBody.GetComponent<Renderer>().material = currentMaterial;

        // Save current transform
        otherMonster.transform.parent = currentMonster.transform.parent;
        otherMonster.transform.position = currentMonster.transform.position;
        otherMonster.transform.rotation = currentMonster.transform.rotation;
        otherMonster.transform.localScale = currentMonster.transform.localScale;

        // Destroy old body
        string currentName = currentMonster.name;
        Object.Destroy(currentMonster);

        otherMonster.name = currentName;

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