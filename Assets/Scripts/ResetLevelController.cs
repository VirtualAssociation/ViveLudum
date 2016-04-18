using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetLevelController : MonoBehaviour {
    private SteamVR_TrackedObject _trackedObj;

    // Use this for initialization
    void Start () {
        _trackedObj = this.GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        bool isPushed = SteamVR_Controller.Input((int)_trackedObj.index).GetPressDown(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
        if (isPushed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Application.LoadLevel(Application.loadedLevel);
        }
    }
}
