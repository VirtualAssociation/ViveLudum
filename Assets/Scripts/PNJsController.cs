using UnityEngine;

public class PNJsController : MonoBehaviour {
    public GameObject[] Pnjs { get { return GameObject.FindGameObjectsWithTag("Destructible"); } }

    public void ShapeShift()
    {
        int pnjId = Random.Range(0, Pnjs.Length);
        ShapeShift sh = Pnjs[pnjId].GetComponent<ShapeShift>();
        sh.ChangeMesh(1);
    }
}

