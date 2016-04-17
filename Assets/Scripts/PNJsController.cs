using UnityEngine;

public class PNJsController : MonoBehaviour {
    [SerializeField]
    private GameObject[] characters;

    public GameObject[] Pnjs { get { return GameObject.FindGameObjectsWithTag("Destructible"); } }

    public void ShapeShift()
    {
        Debug.Log("ShapeShift");
        foreach(GameObject pnj in Pnjs)
        {
            pnj.GetComponent<PNJ>().Reset();
        }
        int pnjId = Random.Range(0, Pnjs.Length);
        string shName = "";
        ShapeShift sh = Pnjs[pnjId].GetComponent<ShapeShift>();
        PNJ pnjScript = Pnjs[pnjId].GetComponent<PNJ>();
        pnjScript.SetBad();
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

