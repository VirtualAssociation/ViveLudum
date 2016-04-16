using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShapeShift : MonoBehaviour {

    [SerializeField]
    private GameObject _gameObjectTest;

    private MeshFilter _meshFilter;

    private Transform _currentTransform;

    private int _currentMesh = 0;
    
    private Vector3 _currentPos;

    private Quaternion _currentRot;


	// Use this for initialization
	void Start () {
        _meshFilter = this.GetComponent<MeshFilter>();
        _currentTransform = this.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        ////////////////Pour les debugs

	    if (Input.GetButtonUp("ChangeForm"))
        {
            ChangeMesh(_gameObjectTest);
        }
    }


    public void ChangeMesh(GameObject newObj)
    {
        _currentRot = _currentTransform.rotation;
        Destroy(this.transform.GetChild(0).gameObject);
        GameObject objInst = Instantiate(newObj, Vector3.zero, _currentRot) as GameObject;
        objInst.transform.parent = this.transform;
        objInst.transform.localPosition = Vector3.zero;
        
        
        
    }

    /*public void ChangeMesh(int meshNum, GameObject newObj)
    {
        _currentPos = _currentTransform.position;
        _currentRot = _currentTransform.rotation;

        Instantiate(newObj, _currentPos, _currentRot);
    }

    public void ChangeMesh(int meshNum, GameObject newObj, Vector3 position)
    {
        _currentPos = _currentTransform.position;
        _currentRot = _currentTransform.rotation;

        Instantiate(newObj, _currentPos, _currentRot);

    }

    public void ChangeMesh(int meshNum, GameObject newObj, Vector3 position, Vector3 rotation)
    {
        _currentPos = _currentTransform.position;
        _currentRot = _currentTransform.rotation;

        Instantiate(newObj, _currentPos, _currentRot);
    }

    public void ChangeMesh(int meshNum, GameObject newObj, Vector3 position, Vector3 rotation, float scale)
    {
        _currentPos = _currentTransform.position;
        _currentRot = _currentTransform.rotation;

        Instantiate(newObj, _currentPos, _currentRot);
    }*/
}
