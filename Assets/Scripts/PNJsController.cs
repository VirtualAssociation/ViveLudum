using System.Collections.Generic;
using UnityEngine;

public class PNJsController : MonoBehaviour {

    public float pnjSpeed = 0.1f;
    public float pnjSpeedRandom = 0.05f;

    public int newPNJCount = 1;
    public float popRadius = 1.0f;

    [SerializeField]
    private GameObject[] characters;
    public GameObject[] Pnjs { get { return GameObject.FindGameObjectsWithTag("Destructible"); } }

    [SerializeField]
    private GameObject[] pnjPrefabs;

    public bool moveCloser = false;

    public int bodyColorHappensOnCycle = 1;
    public int pnjSizeHappensOnCycle = 3;
    public int eyeColorHappensOnCycle = 5;
    public int pupilColorHappensOnCycle = 7;

    public void ShapeShift(int cycleNum)
    {
        // Reset All PNJs
        foreach(GameObject pnj in Pnjs)
        {
            pnj.GetComponent<PNJ>().Reset();
        }

        // Get a random PNJ
        int randomID = Random.Range(0, Pnjs.Length);
        GameObject randomPNJ = Pnjs[randomID];
        ShapeShift shapeShift = randomPNJ.GetComponent<ShapeShift>();
        PNJ pnjScript = randomPNJ.GetComponent<PNJ>();

        // ShapeShift the random PNJ

        pnjScript.SetBad();

        if (cycleNum >= bodyColorHappensOnCycle)
            shapeShift.ChangeBodyColor(randomPNJ);
        if (cycleNum >= pnjSizeHappensOnCycle)
            shapeShift.ChangeScale(randomPNJ);
        if (cycleNum >= eyeColorHappensOnCycle)
            shapeShift.ChangeEyeColor(randomPNJ);
        if (cycleNum >= pupilColorHappensOnCycle)
            shapeShift.ChangePupilColor(randomPNJ);

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
        if (!moveCloser)
        {
            moveCloser = true;
            return;
        }
        foreach (GameObject pnj in Pnjs)
        {
            Vector3 pnjDirection = this.transform.position - pnj.transform.position;
            float randomSpeed = pnjSpeed + Random.Range(-pnjSpeedRandom, pnjSpeedRandom);
            pnj.transform.position = pnj.transform.position + (pnjSpeed + randomSpeed) * pnjDirection;
        }
    }

    public void GenerateNewPNJs()
    {
        Random.seed = (int)Time.time;
        for (int i = 0; i < newPNJCount; ++i)
        {
            GameObject prefab = pnjPrefabs[Random.Range(0, pnjPrefabs.Length)];
            float angle = Random.Range(0, 360);
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * popRadius;
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}

