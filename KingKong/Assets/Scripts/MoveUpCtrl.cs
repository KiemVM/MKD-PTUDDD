using UnityEngine;
using System.Collections;

public class MoveUpCtrl : TouchLogicV2
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
        player.bUp = true;
        bTest = player.bLeft;
    }

    public override void OnTouchMoved()
    {
        player.bUp = true;
        bTest = player.bLeft;
    }

    public override void OnTouchStayed()
    {
        player.bUp = true;
        bTest = player.bLeft;
    }
    public override void OnTouchEndedAnywhere()
    {
        player.bUp = false;
        bTest = player.bLeft;
    }
}
