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

    public void ChangeBodyForm(GameObject pnj)
    {
        Random.seed = System.DateTime.Now.Millisecond;

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

        // Instanciate Monster
        foreach (GameObject m in _monsters)
        {
            if (m.name != currentMonster.name)
            {
                otherMonster = GameObject.Instantiate(m);
                break;
            }
        }

        GameObject currentBody = currentMonster.transform.GetChild(0).GetChild(0).gameObject;
        GameObject otherBody = otherMonster.transform.GetChild(0).GetChild(0).gameObject;

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

}