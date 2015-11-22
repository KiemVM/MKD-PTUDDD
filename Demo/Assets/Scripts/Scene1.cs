using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scene1 : MonoBehaviour {

	// Use this for initialization
    private bool bPause = false;
    public Button BtnPause;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickBtnStart()
    {
        Application.LoadLevel(0);

    }

    public void clickBtnPause()
    {
        if (!bPause)
        {
            Time.timeScale = 0;
            bPause = true;
            Text text = BtnPause.GetComponentInChildren<Text>();
            text.text = "Resume";
        }
          
        else
        {
            Time.timeScale = 1;
            bPause = false;
            Text text = BtnPause.GetComponentInChildren<Text>();
            text.text = "Pause";
        }
    }
}
