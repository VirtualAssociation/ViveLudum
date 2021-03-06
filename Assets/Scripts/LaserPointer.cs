﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor;

public struct LaserPointerEventArgs
{
	public uint controllerIndex;
	public uint flags;
	public float distance;
	public Transform target;
}

public delegate void LaserPointerEventHandler(object sender, LaserPointerEventArgs e);

public class LaserPointer : MonoBehaviour
{
	public Color color;
    public Material material;
    public float thickness = 0.002f;
	public GameObject holder;
	public GameObject pointer;
	public bool addRigidBody = false;
	public Transform reference;
	public event LaserPointerEventHandler PointerIn;
	public event LaserPointerEventHandler PointerOut;

	Transform previousContact = null;

    private SteamVR_TrackedObject _trackedObj;

    private AudioSource _audSrcLaser;
    private bool _audIsPLaying = false;

    [SerializeField]
    private float _laserSoundRefresh = 0.5f;
    private float _laserSoundInitial;

    private PNJsController _pnjCtrl;
    private GameTimer _gameTimer;

    [SerializeField]
    private CheersScript _cheers;

    // Use this for initialization
    void Start()
	{
        _trackedObj = GetComponent<SteamVR_TrackedObject>();

        holder = new GameObject("Ray");
		holder.transform.parent = this.transform;
		holder.transform.localPosition = Vector3.zero;

		pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
		pointer.transform.parent = holder.transform;
		pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
		pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
        pointer.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        BoxCollider collider = pointer.GetComponent<BoxCollider>();
		if (addRigidBody)
		{
			if (collider)
			{
				collider.isTrigger = true;
			}
			Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
			rigidBody.isKinematic = true;
		}
		else
		{
			if (collider)
			{
				Object.Destroy(collider);
			}
		}

        pointer.GetComponent<MeshRenderer>().material = material;
        pointer.GetComponent<MeshRenderer>().receiveShadows = false;

        _audSrcLaser = this.GetComponent<AudioSource>();
        _laserSoundInitial = _laserSoundRefresh;

        if (SceneManager.GetActiveScene().name == "Game")
        {
            _pnjCtrl = GameObject.Find("Main Camera (origin)").GetComponent<PNJsController>();
            _gameTimer = this.transform.parent.GetComponent<GameTimer>();
        }
    }

	public virtual void OnPointerIn(LaserPointerEventArgs e)
	{
		if (PointerIn != null)
			PointerIn(this, e);
	}

	public virtual void OnPointerOut(LaserPointerEventArgs e)
	{
		if (PointerOut != null)
			PointerOut(this, e);
	}

    // Update is called once per frame
    void Update()
	{

        if (_audIsPLaying)
        {
            _laserSoundRefresh -= Time.deltaTime;
            if (_laserSoundRefresh <= 0)
            {
                _audIsPLaying = false;
                _laserSoundRefresh = _laserSoundInitial;
            }
        }

		float dist = 100f;

        Ray raycast = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		bool bHit = Physics.Raycast(raycast, out hit);

        if (previousContact && previousContact != hit.transform)
		{
			LaserPointerEventArgs args = new LaserPointerEventArgs();
			args.distance = 0f;
			args.flags = 0;
			args.target = previousContact;
			OnPointerOut(args);
			previousContact = null;
		}
		if (bHit && previousContact != hit.transform)
		{
			LaserPointerEventArgs argsIn = new LaserPointerEventArgs();
			argsIn.distance = hit.distance;
			argsIn.flags = 0;
			argsIn.target = hit.transform;
			OnPointerIn(argsIn);
			previousContact = hit.transform;
		}

		if (!bHit)
		{
			previousContact = null;
		}
		if (bHit && hit.distance < 100f)
		{
			dist = hit.distance;
		}

        pointer.transform.localScale = new Vector3(thickness, thickness, -dist);
        pointer.transform.localPosition = new Vector3(0f, 0f, - dist / 2f);

        Vector2 axis = SteamVR_Controller.Input((int)_trackedObj.index).GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);

        if (bHit && hit.collider.name == "Monster_Start" && axis.x >= 1)
        {
            PlayLaserSound();
            SceneManager.LoadScene("Game");
            return;
        }

        if (_gameTimer == null)
            return;

        if (_gameTimer.step == GameTimer.STEP.DAY || _gameTimer.step == GameTimer.STEP.NIGHT)
            return;
        
        if (axis.x >= 1)
        {
            PlayLaserSound();
        }

        if (bHit && axis.x >= 1)
        {
            GameObject go = hit.collider.gameObject;
            if (go.tag == "DestructibleChild")
            {
                SteamVR_Controller.Input((int)_trackedObj.index).TriggerHapticPulse(1500);
                if (go.GetComponentInParent<PNJ>().IsBad)
                {
                    Object.DestroyImmediate(hit.collider.gameObject);
                    _cheers.GoodCheers();
                    _pnjCtrl.goodKill = true;
                    //_pnjCtrl.MakeHappyAnimation();
                }
                else
                {
                    Object.DestroyImmediate(hit.collider.gameObject);
                    _cheers.BadCheers();
                }
                _gameTimer.goToNext = true;
            }
        }
	}

    void PlayLaserSound()
    {
        if (_audIsPLaying == false)
        {
            _audSrcLaser.Play();
            _audIsPLaying = true;
        }
    }
}
