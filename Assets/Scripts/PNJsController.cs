using UnityEngine;

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
        ShapeShift sh = Pnjs[pnjId].GetComponent<ShapeShift>();
        sh.ChangeMesh(characters[Random.Range(0, characters.Length)]);
    }
}

