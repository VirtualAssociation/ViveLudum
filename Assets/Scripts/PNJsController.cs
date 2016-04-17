using System.Collections.Generic;
using UnityEngine;

public class PNJsController : MonoBehaviour {

    public float pnjSpeed = 0.1f;
    public float pnjSpeedRandom = 0.05f;

    public int newPNJCount = 2;
    public float popRadius = 1.0f;

    [SerializeField]
    private GameObject[] characters;
    public GameObject[] Pnjs { get { return GameObject.FindGameObjectsWithTag("Destructible"); } }

    [SerializeField]
    private GameObject[] pnjPrefabs;


    public void ShapeShift()
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

        float randomScale = Random.Range(0, 1);
        shapeShift.ChangeMesh(randomPNJ, randomScale + 0.5f);

        foreach (GameObject pnj in Pnjs)
        {
            if (pnj.GetComponent<PNJ>().IsBad == true)
            {
                pnj.GetComponent<PNJSoundController>().PlayNightSound(pnj);
            }
        }
    }

    public void MovePNJsCloser()
    {
        foreach (GameObject pnj in Pnjs)
        {
            Vector3 pnjDirection = this.transform.position - pnj.transform.position;
            float randomSpeed = pnjSpeed + Random.Range(-pnjSpeedRandom, pnjSpeedRandom);
            pnj.transform.position = pnj.transform.position + (pnjSpeed + randomSpeed) * pnjDirection;
        }
    }

    public void GenerateNewPNJs()
    {
        List<float> randomAngles = new List<float>();
        for (int i = 0; i < newPNJCount; ++i)
        {
            GameObject prefab = pnjPrefabs[Random.Range(0, pnjPrefabs.Length)];
            float angle = Random.Range(0, 360);
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * popRadius;
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}

