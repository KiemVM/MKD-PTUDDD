using UnityEngine;
using System.Collections;

public class NhanVatControler : MonoBehaviour {
	public float speed = 5;
	private Rigidbody2D myBody2d;
	private Animator myAnimator;
	public bool canJump = true;
	private bool facingRight = true;

	private bool bDiChuyenDoc = false;
    public int iDem = 0;
    public int myState = 0;
    private const int
        iDungIm = 0,
        iDi = 1,
        iNhay = 2,
        iTreoDay = 3,
        iRoi = 4,
        iVoiDay = 5;

    private float myClumnStart, myColumnEnd;

    //bien de nhan nut android
    public bool bJump = false;
    public bool bLeft = false;
    public bool bRight = false;
    public bool bUp = false;
    public bool bDown = false;
		
	// Use this for initialization
	void Start () {
        myState = iDungIm;
		myBody2d = this.rigidbody2D;
		myAnimator = GetComponent<Animator> ();
	}

    void SetGravity(int nValue)
    {
        if (this.rigidbody2D.gravityScale == nValue)
            return;
        this.rigidbody2D.isKinematic = true;
        this.rigidbody2D.gravityScale = nValue;
        this.rigidbody2D.isKinematic = false;
    }

    public string GetCurrentPlayingAnimationClip()
    {

        foreach (AnimationState anim in animation)
        {
            if (animation.IsPlaying(anim.name))
            {
                return anim.name;
            }
        }

        return string.Empty;

    }



	// Update is called once per frame
	void Update () {
        if(Time.timeScale == 0)
        {
            return;
        }
        if ((myState == iTreoDay)
            || (myState == iNhay)
            || (myState == iVoiDay))
            SetGravity(0);
        else
            SetGravity(2);

        //Luan chuyen qua cac trang thai
        switch (myState)
        {
            case iDungIm:
                ProcessDungIm();
                break;
            case iDi:
                ProcessDi();
                break;
            case iNhay:
                ProcessNhay();
                break;
            case iRoi:
                ProcessRoi();
                break;
            case iTreoDay:
                ProcessTreoDay();
                break;
            case iVoiDay:
                processVoiDay();
                break;
        }
	}

// 	void OnCollisionEnter2D(Collision2D coll)
// 	{
// 		if(coll.gameObject.tag == "Column1")
// 		{
// 			bDiChuyenDoc = true;
// 
// 		}
// 	}

    void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "Column1")
		{
			bDiChuyenDoc = false;
            myClumnStart = ((ColumnControler)coll.gameObject.GetComponent("ColumnControler")).yStart;
            myColumnEnd = ((ColumnControler)coll.gameObject.GetComponent("ColumnControler")).yEnd;
            if(myState == iDi)
            {
                transform.Translate(coll.gameObject.transform.position.x - transform.position.x + 1.0f, 0, 0);
                myAnimator.SetBool("bLeo", true);
                myState = iTreoDay;
            }

            else if(myState == iNhay)
            {
                transform.Translate(coll.gameObject.transform.position.x - transform.position.x - 1.0f, 0, 0);
                myAnimator.SetBool("bLeo", true);
                myState = iTreoDay;
            }
		}
	}

    void ProcessDungIm()
    {
        float move = Input.GetAxisRaw("Horizontal");
        myAnimator.SetFloat("speed", Mathf.Abs(move));
        myBody2d.velocity = new Vector2(move * speed, myBody2d.velocity.y);

        if (facingRight == true && move < 0 )
        {
            facingRight = false;
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180,
                                                  transform.rotation.z);
        }
        else if (facingRight == false && move > 0)
        {
            facingRight = true;
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            myAnimator.SetBool("jump", true);
            canJump = false;
            myBody2d.velocity = new Vector2(myBody2d.velocity.x, 10);
           
        }
        myAnimator.SetBool("jump", false);
        canJump = true;   
    }

    void ProcessDi()
    {


    }

    void ProcessNhay()
    {

    }

    void ProcessRoi()
    {

    }

    void ProcessTreoDay()
    {
        myAnimator.SetBool("jump", false);
        float move = Input.GetAxisRaw("vertical");
        myAnimator.SetFloat("speed", Mathf.Abs(move));
        myBody2d.velocity = new Vector2(move * speed, myBody2d.velocity.y);

        if (facingRight == true && move < 0)
        {
            facingRight = false;
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180,
                                                  transform.rotation.z);
        }
        else if (facingRight == false && move > 0)
        {
            facingRight = true;
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }

//         if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
//         {
//             myAnimator.SetBool("jump", true);
//             canJump = false;
//             myBody2d.velocity = new Vector2(myBody2d.velocity.x, 10);
//         }
//         myAnimator.SetBool("jump", false);
//         canJump = true;   

    }

    void processVoiDay()
    {

    }
}
