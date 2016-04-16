using UnityEngine;
using System.Collections;

public class ControllerCollider : MonoBehaviour
{
    private SteamVR_TrackedObject _trackedObj;

    // Use this for initialization
    void Start()
    {
        _trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        SteamVR_Controller.Device controller = SteamVR_Controller.Input((int)_trackedObj.index);
        bool triggerDown = controller.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger);
        bool triggerUp = controller.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger);

        if (triggerDown)
        {
            Debug.Log("TriggerDown");
        }
    }
}