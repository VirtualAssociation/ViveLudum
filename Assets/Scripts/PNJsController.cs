using UnityEngine;

public class PNJsController : MonoBehaviour {

    public float pnjSpeed = 0.1f;
    public float pnjSpeedRandom = 0.05f;

    [SerializeField]
    private GameObject[] characters;
    public GameObject[] Pnjs { get { return GameObject.FindGameObjectsWithTag("Destructible"); } }

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

        GameObject go = characters[Random.Range(0, characters.Length)]; // Get a random shapeShift to apply on the PNJ
        while (go.name == shapeShift.objName)
        {
            go = characters[Random.Range(0, characters.Length)];
        }
        shapeShift.ChangeMesh(go);

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
}

