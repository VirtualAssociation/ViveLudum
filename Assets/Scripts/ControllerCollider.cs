using UnityEngine;
using System.Collections;

public class ControllerCollider : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private GameObject pickup;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        SteamVR_Controller.Device controller = SteamVR_Controller.Input((int)trackedObj.index);
        bool triggerDown = controller.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger);
        bool triggerUp = controller.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger);

        if (triggerDown && pickup != null)
        {
            pickup.transform.parent = this.transform;
            pickup.GetComponent<Rigidbody>().useGravity = false;
        }
        if (triggerUp && pickup != null)
        {
            pickup.transform.parent = null;
            pickup.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        pickup = collider.gameObject;
    }

    private void OnTriggerExit(Collider collider)
    {
        pickup = null;
    }
}