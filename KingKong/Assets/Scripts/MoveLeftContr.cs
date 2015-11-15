using UnityEngine;
using System.Collections;

public class MoveLeftContr : TouchLogicV2{

    NhanVatControler player;
    public bool bTest = false;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<NhanVatControler>();
	}

    public override void OnTouchBegan()
    {
        player.bLeft = true;
        bTest = player.bLeft;
    }

    public override void OnTouchMoved()
    {
        player.bLeft = true;
        bTest = player.bLeft;
    }

    public override void OnTouchStayed()
    {
        player.bLeft = true;
        bTest = player.bLeft;
    }
    public override void OnTouchEndedAnywhere()
    {
        player.bLeft = false;
        bTest = player.bLeft;
    }
}
