﻿using System.Collections.Generic;
using UnityEngine;

public class PNJsController : MonoBehaviour {

    public float pnjSpeed = 0.1f;
    public float pnjSpeedRandom = 0.05f;

    public int newPNJCount;
    public float popRadius = 1.0f;
    public float popRadiusRandom = 0.2f;

    public GameObject[] Pnjs { get { return GameObject.FindGameObjectsWithTag("Destructible"); } }

    [SerializeField]
    private GameObject[] pnjPrefabs;

    public bool goodKill = false;

    public void ShapeShift(int cycleNum)
    {
        Random.seed = System.DateTime.Now.Millisecond;

        // Reset All PNJs
        foreach(GameObject pnj in Pnjs)
        {
            pnj.GetComponent<PNJ>().Reset();
        }
        
        // Get a random PNJ
        GameObject randomPNJ = null;
        while (randomPNJ == null)
        {
            int randomID = Random.Range(0, Pnjs.Length);
            GameObject pnjTmp = Pnjs[randomID];
            if (pnjTmp.transform.childCount > 0 && pnjTmp.transform.GetChild(0).childCount > 0)
            {
                randomPNJ = pnjTmp;
            }
        }
        
        ShapeShift shapeShift = this.GetComponent<ShapeShift>();
        PNJ pnjScript = randomPNJ.GetComponent<PNJ>();

        // ShapeShift the random PNJ

        pnjScript.SetBad();

        shapeShift.ChangeBodyForm(randomPNJ);

        foreach (GameObject pnj in Pnjs)
        {
            if (pnj.GetComponent<PNJ>().IsBad)
            {
                pnj.GetComponent<PNJSoundController>().PlayNightSound(pnj);
            }
        }
    }

    public void MovePNJsCloser()
    {
        foreach (GameObject pnj in Pnjs)
        {
            if (pnj.transform.childCount > 0)
            {
                Vector3 pnjDirection = this.transform.position - pnj.transform.position;
                float randomSpeed = pnjSpeed + Random.Range(-pnjSpeedRandom, pnjSpeedRandom);
                pnj.transform.position = pnj.transform.position + (pnjSpeed + randomSpeed) * pnjDirection;
            }
        }
    }

    public void GenerateNewPNJs()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        for (int i = 0; i < newPNJCount; ++i)
        {
            GameObject prefab = pnjPrefabs[Random.Range(0, pnjPrefabs.Length)];
            float angle = Random.Range(0, 360);
            float radius = popRadius + Random.Range(-popRadiusRandom, popRadiusRandom);
            Vector3 pos = this.transform.position + new Vector3(Mathf.Cos(angle), 0.05f, Mathf.Sin(angle)) * radius;
            Instantiate(prefab, pos, Quaternion.identity);
            prefab.transform.parent = GameObject.Find("pnjs").transform;
        }
    }

    public void ReorientPNJs()
    {
        foreach (GameObject pnj in Pnjs)
        {
            if (pnj.transform.childCount > 0)
            {
                GameObject monster = pnj.transform.GetChild(0).transform.gameObject;
                monster.transform.LookAt(this.transform);
                monster.transform.Rotate(new Vector3(0, 1, 0), -90);
            }
        }
    }

    public void MakeHappyAnimation()
    {
        foreach(GameObject pnj in Pnjs)
        {
            try {
                GameObject monster = pnj.transform.GetChild(0).gameObject;
                if (monster == null)
                {
                    continue;
                }

                Animation anim = monster.GetComponent<Animation>();
                if (anim == null)
                {
                    continue;
                }
                anim.Play();
            }
            catch (UnityException e)
            {
                Debug.Log("Exception avoid: " + e);
            }
        }
    }

    public void MakeAngryAnimation()
    {
        foreach (GameObject pnj in Pnjs)
        {
            GameObject submonster = pnj.transform.GetChild(0).GetChild(0).gameObject;
            if (submonster == null)
            {
                continue;
            }
            Animation anim = submonster.GetComponent<Animation>();
            if (anim == null)
            {
                continue;
            }
            anim.GetClip("monster_loose");
            anim.Play();

        }
    }
}