using UnityEngine;
using System.Collections;

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
	public bool active = true;
	public Color color;
    public float thickness = 0.002f;
	public GameObject holder;
	public GameObject pointer;
	bool isActive = false;
	public bool addRigidBody = false;
	public Transform reference;
	public event LaserPointerEventHandler PointerIn;
	public event LaserPointerEventHandler PointerOut;

	Transform previousContact = null;

    private SteamVR_TrackedObject _trackedObj;
    private bool _triggerPressed = false;

    public event ClickedEventHandler TriggerClicked;
    public event ClickedEventHandler TriggerUnclicked;

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
		Material newMaterial = new Material(Shader.Find("Unlit/Color"));
		newMaterial.SetColor("_Color", color);
        pointer.GetComponent<MeshRenderer>().material = newMaterial;
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

    public virtual void OnTriggerClicked(ClickedEventArgs e)
    {
        if (TriggerClicked != null)
            Debug.Log("OnTriggerClicked");
        else
            Debug.Log("OnTriggerClicked null");
    }


    // Update is called once per frame
    void Update()
	{
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

        SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>();
        Vector2 axis = SteamVR_Controller.Input((int)_trackedObj.index).GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        if (bHit && axis.x >= 1)
        {
            GameObject go = hit.collider.gameObject;
            if (go.tag == "DestructibleChild")
            {
                Object.DestroyImmediate(hit.collider.gameObject);
                SteamVR_Controller.Input((int)_trackedObj.index).TriggerHapticPulse(1500);
            }
        }

        if (axis.x < 1)
        {
            
        }
        pointer.transform.localScale = new Vector3(thickness, thickness, dist);
        pointer.transform.localPosition = new Vector3(0f, 0f, - dist / 2f);
	}
}
