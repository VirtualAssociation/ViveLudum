using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShapeShift : MonoBehaviour {

    private MeshFilter _meshFilter;

    private Transform _currentTransform;

    private int _currentMesh = 0;
    
    private Vector3 _currentPos;

    private Quaternion _currentRot;

    [HideInInspector]
    public string objName { get; private set; }


	// Use this for initialization
	void Start () {
        _meshFilter = this.GetComponent<MeshFilter>();
        _currentTransform = this.GetComponent<Transform>();
        objName = this.transform.GetChild(0).gameObject.name;
    }
	
	// Update is called once per frame
	void Update () {

        ////////////////Pour les debugs

	   
       /*if (Input.GetButtonUp("ChangeForm"))
        {
            ChangeMesh(_gameObjectTest);
        }*/
    }


    public void ChangeMesh(GameObject newObj)
    {
        _currentRot = _currentTransform.rotation;
        if(this.transform.GetChild(0) != null)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
        GameObject objInst = Instantiate(newObj, Vector3.zero, _currentRot) as GameObject;
        objName = newObj.name;
        objInst.transform.parent = this.transform;
        objInst.transform.localPosition = Vector3.zero;
        
        
        
    }

    public void ChangeMesh(GameObject newObj, Vector3 position)
    {
        _currentRot = _currentTransform.rotation;
        Destroy(this.transform.GetChild(0).gameObject);
        GameObject objInst = Instantiate(newObj, Vector3.zero, _currentRot) as GameObject;
        objName = newObj.name;
        objInst.transform.parent = this.transform;
        objInst.transform.localPosition = position;
    }

    public void ChangeMesh(GameObject newObj, Vector3 position, Quaternion rotation)
    {
        _currentRot = _currentTransform.rotation;
        Destroy(this.transform.GetChild(0).gameObject);
        GameObject objInst = Instantiate(newObj, Vector3.zero, _currentRot) as GameObject;
        objName = newObj.name;
        objInst.transform.parent = this.transform;
        objInst.transform.localPosition = position;
        objInst.transform.localRotation = rotation;
    }

    public void ChangeMesh(GameObject newObj, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        _currentRot = _currentTransform.rotation;
        Destroy(this.transform.GetChild(0).gameObject);
        GameObject objInst = Instantiate(newObj, Vector3.zero, _currentRot) as GameObject;
        objName = newObj.name;
        objInst.transform.parent = this.transform;
        objInst.transform.localPosition = position;
        objInst.transform.localRotation = rotation;
        objInst.transform.localScale = scale;

    }

   
}