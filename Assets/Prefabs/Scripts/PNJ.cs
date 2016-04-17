using UnityEngine;
using System.Collections;

public class PNJ : MonoBehaviour {
    public bool IsBad { get; private set; }

    void Start()
    {
        this.IsBad = false;
    }

	public void Reset()
    {
        IsBad = false;
    }

    public void SetBad()
    {
        IsBad = true;
    }
}
