using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickBtnStart()
    {
        Application.LoadLevel("KingKongJR");
    }

    public void ClickBtnGuide()
    {
        Application.LoadLevel("Guide");
    }
}
