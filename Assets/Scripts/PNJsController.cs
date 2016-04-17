using UnityEngine;

public class PNJsController : MonoBehaviour {
    [SerializeField]
    private GameObject[] characters;

    public GameObject[] Pnjs { get { return GameObject.FindGameObjectsWithTag("Destructible"); } }

    public void ShapeShift()
    {
        Debug.Log("ShapeShift");
        
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
        string randomName = "";
        if (shapeShift.objName != null)
        {
            randomName = shapeShift.objName;
        }

        // Get a random shapeShift to apply on the PNJ
        GameObject go = characters[Random.Range(0, characters.Length)];
        while (go.name == shapeShift.objName)
        {
            go = characters[Random.Range(0, characters.Length)];
        }
        shapeShift.ChangeMesh(go);
    }
}

