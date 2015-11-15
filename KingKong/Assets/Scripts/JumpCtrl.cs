using UnityEngine;
using System.Collections;

public class JumpCtrl : TouchLogicV2
{

    NhanVatControler player;
    public bool bTest = false;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<NhanVatControler>();
    }

    public override void OnTouchBegan()
    {
        player.bJump = true;
        bTest = player.bLeft;
    }

    public override void OnTouchMoved()
    {
        player.bJump = true;
        bTest = player.bLeft;
    }

    public override void OnTouchStayed()
    {
        player.bJump = true;
        bTest = player.bLeft;
    }
    public override void OnTouchEndedAnywhere()
    {
        player.bJump = false;
        bTest = player.bLeft;
    }
}
