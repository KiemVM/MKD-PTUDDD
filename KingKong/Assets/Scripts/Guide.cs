using UnityEngine;
using System.Collections;

public class Guide : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickBtnStart()
    {
        Application.LoadLevel("Start");
    }
}
