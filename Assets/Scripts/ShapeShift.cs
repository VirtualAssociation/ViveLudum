using UnityEngine;
using System.Collections.Generic;

public class ShapeShift : MonoBehaviour {

    [SerializeField]
    private List<Mesh> _meshList = new List<Mesh>();

    private MeshFilter _meshFilter;

    private Transform _currentTransform;

    private int _currentMesh = 0;


	// Use this for initialization
	void Start () {
        _meshFilter = this.GetComponent<MeshFilter>();
        _currentTransform = this.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        ////////////////Pour les debugs
        /*
	    if (Input.GetButtonUp("ChangeForm"))
        {
            _currentMesh = _currentMesh + 1;
            Debug.Log(_currentMesh);
            if (_currentMesh >= _meshList.Count)
            {
                _currentMesh = 0;
            }
            ChangeMesh(_currentMesh);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentMesh = _currentMesh + 1;
            Debug.Log(_currentMesh);
            if (_currentMesh >= _meshList.Count)
            {
                _currentMesh = 0;
            }
            ChangeMesh(_currentMesh, new Vector3(this.transform.position.x + 1f, this.transform.position.y + 1f, this.transform.position.z + 1f));
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentMesh = _currentMesh + 1;
            Debug.Log(_currentMesh);
            if (_currentMesh >= _meshList.Count)
            {
                _currentMesh = 0;
            }
            ChangeMesh(_currentMesh, new Vector3(this.transform.position.x + 1f, this.transform.position.y + 1f, this.transform.position.z + 1f),new Vector3 (this.transform.position.x +45f, this.transform.position.y, this.transform.position.z));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentMesh = _currentMesh + 1;
            Debug.Log(_currentMesh);
            if (_currentMesh >= _meshList.Count)
            {
                _currentMesh = 0;
            }
            ChangeMesh(_currentMesh, new Vector3(this.transform.position.x + 1f, this.transform.position.y + 1f, this.transform.position.z + 1f), new Vector3(this.transform.position.x + 45f, this.transform.position.y, this.transform.position.z), 2f);
        }
        */
    }

    public void ChangeMesh(int meshNum)
    {
        _meshFilter.mesh = _meshList[meshNum];
    }

    public void ChangeMesh(int meshNum, Vector3 position)
    {
        ChangeMesh(meshNum);
        _currentTransform.position = position;
    }

    public void ChangeMesh(int meshNum, Vector3 position, Vector3 rotation)
    {
        ChangeMesh(meshNum, position);
        _currentTransform.rotation = Quaternion.Euler(rotation);
    }

    public void ChangeMesh(int meshNum, Vector3 position, Vector3 rotation, float scale)
    {
        ChangeMesh(meshNum, position, rotation);
        _currentTransform.localScale = new Vector3(scale, scale, scale);
    }
}
