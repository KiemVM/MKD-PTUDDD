using UnityEngine;
using System.Collections;

public class MoveRightCtrl : TouchLogicV2
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
        player.bRight = true;
        bTest = player.bLeft;
    }

    public override void OnTouchMoved()
    {
        player.bRight = true;
        bTest = player.bLeft;
    }

    public override void OnTouchStayed()
    {
        player.bRight = true;
        bTest = player.bLeft;
    }
    public override void OnTouchEndedAnywhere()
    {
        player.bRight = false;
        bTest = player.bLeft;
    }
}
