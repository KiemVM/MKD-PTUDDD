using UnityEngine;
using System.Collections;

public class NoveDownCtrl : TouchLogicV2
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
        player.bDown = true;
        bTest = player.bLeft;
    }

    public override void OnTouchMoved()
    {
        player.bDown = true;
        bTest = player.bLeft;
    }

    public override void OnTouchStayed()
    {
        player.bDown = true;
        bTest = player.bLeft;
    }
    public override void OnTouchEndedAnywhere()
    {
        player.bDown = false;
        bTest = player.bLeft;
    }
}
