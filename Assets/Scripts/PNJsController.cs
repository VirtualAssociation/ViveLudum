﻿using UnityEngine;

public class PNJsController : MonoBehaviour {
    [SerializeField]
    private GameObject[] characters;

    public GameObject[] Pnjs { get { return GameObject.FindGameObjectsWithTag("Destructible"); } }

    void Update()
    {
        if (Input.GetButtonUp("ChangeForm"))
        {
            ShapeShift();
        }
    }

    public void ShapeShift()
    {
        int pnjId = Random.Range(0, Pnjs.Length);
        Debug.Log(Pnjs);
        string shName = "";
        ShapeShift sh = Pnjs[pnjId].GetComponent<ShapeShift>();
        if (sh.objName != null)
        {
            shName = sh.objName;
        }
        GameObject go = characters[Random.Range(0, characters.Length)];
        while (go.name == sh.objName)
        {
            go = characters[Random.Range(0, characters.Length)];
        }
        
        sh.ChangeMesh(go);
    }
}

