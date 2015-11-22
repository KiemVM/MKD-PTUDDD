using UnityEngine;
using System.Collections;
using ColumnStructNS;
using MapStructNS;
using UnityEngine.UI;

public class Controller : MonoBehaviour {


	
	public const float fDistanceColumnX = 0.7f;
	public const float fDistanceColumnY = 0.7f;
	public const float fDelta = 0.1f;
	public const float fSpeedJumpX = 2.0f;
	public const float fSpeedJumpY = 6.0f;
	public const int iMaxCount = 12;
	public const float iMaxHighJumpDown = 2.5f;
	public const float boxSmallX = 0.14f;
	public const float boxSmallY = 0.14f;
	public const float boxBigX = 0.25f;
	public const float boxBigY = 0.14f;
	public bool bIsOnGround = true;
    public GameOver ObGameOver;
    public Text TxGameOver;

	public const int 
		iIdle = 0,
		iGoLeft = 1,
		iGoRight = 2,
		iClimbLeft = 3,
		iClimbRight = 4,
		iClimbDouble = 5,
		iJumpLeft = 6,
		iJumpRight = 7,
		iDieLeft = 8,
		iDieRight = 9, 
		iReachLeft = 10, 
		iReachRight = 11;

	public float m_YJump;
	public float m_ColumnUpY, m_ColumnDownY;
	public float m_ColumnX;
	public float m_LeftColumnUpY, m_LeftColumnDownY;
	public float m_RightColumnUpY, m_RightColumnDownY;
	public float m_LeftColumnX, m_RightColumnX;
	public int m_iState;
	public Animator m_animator;
	public int m_iCountAuto = 0;
	public int m_iStartDie = 0;

    public bool bClick = false;
    public bool bMoveUp = false;
    public bool bMoveDown = false;
    public bool bMoveLeft = false;
    public bool bMoveRight = false;
    public bool bJump = false;

    public bool bMoveUpClick = false;
    public bool bMoveDownClick = false;
    public bool bMoveLeftClick = false;
    public bool bMoveRightClick = false;
    public bool bJumpClick = false;

		
	// Use this for initialization
	void Start () {
		m_iState = 0;
		m_animator = GetComponent<Animator>();
		m_YJump = transform.position.y;
        ObGameOver = FindObjectOfType<GameOver>();
        Time.timeScale = 1;
	}

	void SetGravity(int nValue)
	{
		if (this.rigidbody2D.gravityScale == nValue)
			return;
		this.rigidbody2D.isKinematic = true;
		this.rigidbody2D.gravityScale = nValue;
		this.rigidbody2D.isKinematic = false;
	}

	public string GetCurrentPlayingAnimationClip() {
				
		foreach (AnimationState anim in animation) {
			if (animation.IsPlaying (anim.name)) {
						return anim.name;
				}
		}

		return string.Empty;
				
	}

	void ProcessIdle()
	{
		SetGravity (1);
		m_animator.speed = 1;

		if (InputKeyDown(5)) //Jump
		{
			m_YJump = transform.position.y;
			gameObject.rigidbody2D.velocity = new Vector2(0, fSpeedJumpY);
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
            InputkeyUp(5);
		}


		if (InputKeyDown(3)) //left - 3
		{
			m_animator.SetTrigger ("TriggerGoLeft");
			m_iState = iGoLeft;
            InputkeyUp(3);
		}

		if (InputKeyDown(4)) // right - 4
		{
			m_animator.SetTrigger ("TriggerGoRight");
			m_iState = iGoRight;
            InputkeyUp(4);
		}
	}

	void ProcessGoLeft()
	{
		SetGravity (1);

        if (Input.anyKey || bClick == true)
			m_animator.speed = 1;
		else
			if (m_iCountAuto == iMaxCount) m_animator.speed = 0;

		if (InputKeyDown(5) && bIsOnGround) // jump
		{
			m_YJump = transform.position.y;
			if (Input.GetKey("left") || (bMoveLeft == true && bClick == true)) 
				gameObject.rigidbody2D.velocity = new Vector2(-fSpeedJumpX, fSpeedJumpY);
			else
				gameObject.rigidbody2D.velocity = new Vector2(0, fSpeedJumpY);
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
            InputkeyUp(5);
		}


		if (InputOnKey(3)) // left - 3
		{
			if (bIsOnGround)
			    transform.Translate (-fDelta, 0, 0);
		}
		else
		{
			
		}
		if (InputOnKey(4)) // right - 4
		{
			m_animator.SetTrigger ("TriggerGoRight");
			m_iState = iGoRight;
		}

	}
	void ProcessGoRight()
	{
		SetGravity (1);

		if (Input.anyKey || bClick == true)
			m_animator.speed = 1;
		else
			if (m_iCountAuto == iMaxCount) m_animator.speed = 0;

		if (InputKeyDown(5) && bIsOnGround) // jump
		{
			m_YJump = transform.position.y;
			if (InputOnKey(4)) // right - 4
			    gameObject.rigidbody2D.velocity = new Vector2(fSpeedJumpX, fSpeedJumpY);
			else
				gameObject.rigidbody2D.velocity = new Vector2(0, fSpeedJumpY);

			m_animator.SetTrigger ("TriggerJumpRight");
			m_iState = iJumpRight;
            InputkeyUp(5);
		}

        if (InputOnKey(4)) // right
		{
			if (bIsOnGround)
				transform.Translate (fDelta, 0, 0);
		}
		else
		{
			
		}

		if (InputOnKey(3)) // left - 3
		{
			m_animator.SetTrigger ("TriggerGoLeft");
			m_iState = iGoLeft;
		}
	}

	void ProcessJumpLeft()
	{
		SetGravity (1);
		m_animator.speed = 1;

		/*if (Input.GetKey ("space"))		
			transform.Translate (0, fDelta, 0);

		if (Input.GetKey ("left"))
			transform.Translate (-fDelta, 0, 0);*/

	}
	void ProcessJumpRight()
	{
		SetGravity (1);
		m_animator.speed = 1;

		/*if (Input.GetKey ("space"))
			transform.Translate (0, fDelta, 0);

		if (Input.GetKey ("right"))
			transform.Translate (fDelta, 0, 0);*/
	}

	void ProcessDieLeft()
	{
		SetGravity (1);
		m_animator.speed = 1;

		float fYDown;
		m_iStartDie++;
		if (m_iStartDie > 120)
		{
			fYDown = ((MapStruct)GameObject.FindGameObjectWithTag ("Ground").GetComponent ("MapStruct")).m_YDown;
			if (fYDown < this.transform.position.y - fDistanceColumnY)
			{
				gameObject.rigidbody2D.isKinematic = true;
				transform.Translate (0, -fDelta, 0);
			}
			else
			{
				m_iStartDie = 0;
                Die();
			}
		}
		
	}
	void ProcessDieRight()
	{
		SetGravity (1);
		m_animator.speed = 1;

		float fYDown;
		m_iStartDie++;
		if (m_iStartDie > 120)
		{
			fYDown = ((MapStruct)GameObject.FindGameObjectWithTag ("Ground").GetComponent ("MapStruct")).m_YDown;
			if (fYDown < this.transform.position.y - fDistanceColumnY)
			{
				gameObject.rigidbody2D.isKinematic = true;
				transform.Translate (0, -fDelta, 0);
			}
			else
			{
				m_iStartDie = 0;
                Die();
			}
		}
	}

	void ProcessClimbLeft()
	{
		SetGravity (0);
		m_animator.speed = 1;

        if (InputAnykey())
			m_animator.speed = 1;
		else
			if (m_iCountAuto == iMaxCount) m_animator.speed = 0;

		if (InputOnKey(1)) // up - 1
		{
			transform.Translate (0, fDelta, 0);
		}		

		if (InputOnKey(2)) // down 2
		{
			transform.Translate (0, -2 * fDelta, 0);
		}

		if (InputKeyDown(3)) // left -3 
		{
			m_iCountAuto = 0;
			m_YJump = transform.position.y;
			SetBoxCollider(boxBigX, boxBigY);// vi tri cua khi cung tu dong duoc dich di tuong ung
			m_animator.SetTrigger ("TriggerReachLeft");
			m_iState = iReachLeft;
            InputkeyUp(3);
			return;
		}

		if (InputKeyDown(4)) //right - 4
		{
			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbRight");
			m_iState = iClimbRight;
			transform.Translate(2 * fDistanceColumnX, 0, 0);
            bMoveRightClick = false;
            InputkeyUp(4);
			return;
		}

		if ((transform.position.y > m_ColumnUpY + fDistanceColumnY) || (transform.position.y < m_ColumnDownY - fDistanceColumnY))
		{
			m_YJump = transform.position.y;
			gameObject.transform.Translate(m_ColumnX - gameObject.transform.position.x, 0, 0);
			m_animator.SetTrigger ("TriggerJumpRight");
			m_iState = iJumpRight;
			return;
		}
	}

	void ProcessClimbRight()
	{
		SetGravity (0);
		m_animator.speed = 1;

        if (InputAnykey())
			m_animator.speed = 1;
		else
			if (m_iCountAuto == iMaxCount) m_animator.speed = 0;

		if (InputOnKey(1)) // up -1 
		{
			transform.Translate (0, fDelta, 0);
		}

		if (InputOnKey(2)) // down -2 
		{
			transform.Translate (0, -2 * fDelta, 0);
		}

		if (InputKeyDown(4)) // right - 4
		{
			m_iCountAuto = 0;
			m_YJump = transform.position.y;
			SetBoxCollider(boxBigX, boxBigY);// vi tri cua khi cung tu dong duoc dich di tuong ung
			m_animator.SetTrigger ("TriggerReachRight");
			m_iState = iReachRight;
            InputkeyUp(4);
			return;
		}

		if (InputKeyDown(3)) // left -3
		{
			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbLeft");
			m_iState = iClimbLeft;
			transform.Translate(-2 * fDistanceColumnX, 0, 0);
            InputkeyUp(3);
			return;
		}

		if ((transform.position.y > m_ColumnUpY + fDistanceColumnY) || (transform.position.y < m_ColumnDownY - fDistanceColumnY))
		{
			m_YJump = transform.position.y;
			gameObject.transform.Translate(m_ColumnX - gameObject.transform.position.x, 0, 0);
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
			return;
		}
	}

	void ProcessClimbDouble()
	{
		SetGravity (0);
		m_animator.speed = 1;

		SetBoxCollider ((m_RightColumnX - m_LeftColumnX) / transform.localScale.x - 0.1f / transform.localScale.x , boxBigY);
        if (InputAnykey())
			m_animator.speed = 1;
		else
			if (m_iCountAuto == iMaxCount) m_animator.speed = 0;

		if (InputOnKey(1))// up - 1
		{
			transform.Translate (0, 2 * fDelta, 0);
		}

		if (InputOnKey(2)) // 2
		{
			transform.Translate (0, -fDelta, 0);
		}

		if (InputKeyDown(3)) // left
		{
			m_ColumnUpY = m_LeftColumnUpY;
			m_ColumnDownY = m_LeftColumnDownY;
			m_ColumnX = m_LeftColumnX;
			SetBoxCollider(boxSmallX, boxSmallY);
			transform.Translate(m_ColumnX - transform.position.x + fDistanceColumnX, 0, 0);

			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbRight");
			m_iState = iClimbRight;
            bMoveLeftClick = false;
            InputkeyUp(3);
			return;
		}

		if (InputKeyDown(4)) // 4
		{
			m_ColumnUpY = m_RightColumnUpY;
			m_ColumnDownY = m_RightColumnDownY;
			m_ColumnX = m_RightColumnX;
			SetBoxCollider(boxSmallX, boxSmallY);
			transform.Translate(m_ColumnX - transform.position.x - fDistanceColumnX, 0, 0);

			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbLeft");
			m_iState = iClimbLeft;
            bMoveRightClick = false;
            InputkeyUp(4);
			return;
		}

		if ((transform.position.y > m_LeftColumnUpY + fDistanceColumnY) || (transform.position.y < m_LeftColumnDownY - fDistanceColumnY))
		{
			m_ColumnUpY = m_RightColumnUpY;
			m_ColumnDownY = m_RightColumnDownY;
			m_ColumnX = m_RightColumnX;
			SetBoxCollider(boxSmallX, boxSmallY);
			transform.Translate(m_ColumnX - transform.position.x - fDistanceColumnX, 0, 0);

			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbLeft");
			m_iState = iClimbLeft;
			return;
		}

		if ((transform.position.y > m_RightColumnUpY + fDistanceColumnY) || (transform.position.y < m_RightColumnDownY - fDistanceColumnY))
		{
			m_ColumnUpY = m_LeftColumnUpY;
			m_ColumnDownY = m_LeftColumnDownY;
			m_ColumnX = m_LeftColumnX;
			SetBoxCollider(boxSmallX, boxSmallY);
			transform.Translate(m_ColumnX - transform.position.x + fDistanceColumnX, 0, 0);

			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbRight");
			m_iState = iClimbRight;
			return;
		}

	}

	void ProcessReachLeft()
	{
		SetGravity(0);
		m_animator.speed = 1;

        if (InputKeyDown(3)) // 3
		{
			m_YJump = transform.position.y;
			transform.Translate(-0.1f, 0.1f, 0);
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
            InputkeyUp(3);
			return;
		}

        if (InputKeyDown(4) || InputKeyDown(2) || InputKeyDown(1)) // 4 - 2 -1
		{
			SetBoxCollider(boxSmallX, boxSmallY);
			transform.Translate(m_ColumnX - transform.position.x - fDistanceColumnX, 0, 0);

			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbLeft");
			m_iState = iClimbLeft;
            InputkeyUp(2);
            InputkeyUp(1);
            InputkeyUp(4);
			return;
		}
	}

	void ProcessReachRight()
	{
		SetGravity(0);
		m_animator.speed = 1;

        if (InputKeyDown(4)) // right - 4
		{
			m_YJump = transform.position.y;
			transform.Translate(0.1f, 0.1f, 0);
			m_animator.SetTrigger ("TriggerJumpRight");
			m_iState = iJumpRight;
            InputkeyUp(4);
			return;
		}

        if (InputKeyDown(3) || InputKeyDown(1) || InputKeyDown(2)) // 3 - 1 -2
		{
			SetBoxCollider(boxSmallX, boxSmallY);
			transform.Translate(m_ColumnX - transform.position.x + fDistanceColumnX, 0, 0);

			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbRight");
			m_iState = iClimbRight;
            InputkeyUp(3);
            InputkeyUp(1);
            InputkeyUp(2);
			return;
		}
	}

	void SetBoxCollider (float fX, float fY)
	{
		BoxCollider2D[] listBox = gameObject.GetComponents<BoxCollider2D>();
		for (int i = 0; i < listBox.Length; i++)
			listBox[i].size = new Vector2(fX, fY);
	}

	void Update() {
		m_iCountAuto++;
		if (m_iCountAuto > iMaxCount) 
			m_iCountAuto = iMaxCount;


		if (!((m_iState == iDieLeft) || (m_iState == iDieRight)))
			gameObject.rigidbody2D.isKinematic = false;
		if (!((m_iState == iReachLeft) || (m_iState == iReachRight)
		    || (m_iState == iClimbDouble)))
			SetBoxCollider(boxSmallX, boxSmallY);

		switch (m_iState)
		{
		case iIdle:
			ProcessIdle();
			break;

		case iGoLeft:
			ProcessGoLeft();
			break;

		case iGoRight:
			ProcessGoRight();
			break;

		case iClimbLeft:
			ProcessClimbLeft();
			break;

		case iClimbRight:
			ProcessClimbRight();
			break;

		case iClimbDouble:
			ProcessClimbDouble();
			break;

		case iJumpLeft:
			ProcessJumpLeft();
			break;
			
		case iJumpRight:
			ProcessJumpRight();
			break;

		case iDieLeft:
			ProcessDieLeft();
			break;
			
		case iDieRight:
			ProcessDieRight();
			break;
		
		case iReachLeft:
			ProcessReachLeft();
			break;
		
		case iReachRight:
			ProcessReachRight();
			break;
		}

	}


	void OnCollisionEnter2D(Collision2D coll) {

		gameObject.rigidbody2D.velocity = new Vector2 (0, 0);
		m_animator.speed = 1;

		if ((m_iState == iDieLeft) || (m_iState == iDieRight))
		{
			return;
		}

		if (coll.gameObject.tag == "EnemyRed")
		{
			m_animator.SetTrigger ("TriggerDieLeft");
			m_iState = iDieLeft;
			return;
		}

		if (coll.gameObject.tag == "Column")
		{

			if ((m_iState == iJumpLeft) || (m_iState == iGoLeft))
			{
				m_ColumnUpY = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yUp;
				m_ColumnDownY = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yDown;
				m_ColumnX = coll.gameObject.transform.position.x;

				transform.Translate(coll.gameObject.transform.position.x - transform.position.x + fDistanceColumnX, 0, 0);
				m_iCountAuto = 0;
               	m_animator.SetTrigger ("TriggerClimbRight");
				m_iState = iClimbRight;
			}
			else if ((m_iState == iJumpRight) || (m_iState == iGoRight))
			{
				m_ColumnUpY = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yUp;
				m_ColumnDownY = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yDown;
				m_ColumnX = coll.gameObject.transform.position.x;

				transform.Translate(coll.gameObject.transform.position.x - transform.position.x - fDistanceColumnX, 0, 0);
				m_iCountAuto = 0;
				m_animator.SetTrigger ("TriggerClimbLeft");
				m_iState = iClimbLeft;
			}
			else if ((m_iState == iReachLeft) || (m_iState == iReachRight))
			{
				if (m_ColumnX != coll.gameObject.transform.position.x)
				{
					// toa do cot va cham khac voi cot dang leo --> chuyen sang leo 2 cot
					if (m_iState == iReachLeft)
					{
						m_LeftColumnUpY = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yUp;
						m_LeftColumnDownY = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yDown;
						m_LeftColumnX = coll.gameObject.transform.position.x;

						m_RightColumnUpY = m_ColumnUpY;
						m_RightColumnDownY = m_ColumnDownY;
						m_RightColumnX = m_ColumnX;
					}
					else if (m_iState == iReachRight)
					{
						m_RightColumnUpY = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yUp;
						m_RightColumnDownY = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yDown;
						m_RightColumnX = coll.gameObject.transform.position.x;
						
						m_LeftColumnUpY = m_ColumnUpY;
						m_LeftColumnDownY = m_ColumnDownY;
						m_LeftColumnX = m_ColumnX;
					}

					m_animator.SetTrigger("TriggerClimbDouble");
					m_iState = iClimbDouble;
				}
			}
			return;
		}

		if (coll.gameObject.tag == "Water")
		{
			m_animator.SetTrigger ("TriggerDieLeft");
			m_iState = iDieLeft;
			return;
		}

		if (coll.gameObject.tag == "Ground")
		{
			bIsOnGround = true;
			if ((m_iState == iJumpLeft) || (m_iState == iJumpRight) 
			    || (m_iState == iGoLeft) || (m_iState == iGoRight)
			    || (m_iState == iReachLeft) || (m_iState == iReachRight))
			{
				if (m_YJump - transform.position.y > iMaxHighJumpDown)
				{
					m_animator.SetTrigger("TriggerDieLeft");
					m_iState = iDieLeft;
					return;
				}
			}

			if (m_iState == iJumpLeft)
			{
				m_iCountAuto = 0;
				m_animator.speed = 1;
				m_animator.SetTrigger ("TriggerGoLeft");
				m_iState = iGoLeft;
			}
			else if (m_iState == iJumpRight)
			{
				m_iCountAuto = 0;
				m_animator.speed = 1;
				m_animator.SetTrigger ("TriggerGoRight");
				m_iState = iGoRight;
			}
		}
	}


	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground")
		{
			bIsOnGround = false;
			m_YJump = transform.position.y;
		}
	}

    public void DiTrai()
    {
        bMoveLeft = true;
        bClick = true;
    }

    public void ThoatDiTrai()
    {
        bMoveLeft = false;
        bClick = false;
    }

    public void DiPhai()
    {
        bMoveRight = true;
        bClick = true;
    }

    public void ThoatDiPhai()
    {
        bMoveRight = false;
        bClick = false;
    }

    public void DiLen()
    {
        bMoveUp = true;
        bClick = true;
    }

    public void ThoatDiLen()
    {
        bMoveUp = false;
        bClick = false;
    }

    public void DiXuong()
    {
        bMoveDown = true;
        bClick = true;
    }

    public void ThoatDiXuong()
    {
        bMoveDown = false;
        bClick = false;
    }

    public void Nhay()
    {
        bJump = true;
        bClick = true;
    }
    public void ThoatNhay()
    {
        bJump = false;
    }

    public void Die()
    {
        TxGameOver = ObGameOver.GetComponent<Text>();
        TxGameOver.text = "Game Over";
        Time.timeScale = 0;
    }

    public void ClickLen()
    {
        bMoveUpClick = true;
    }

    public void ClickXuong()
    {
        bMoveDownClick = true;
    }

    public void ClickTrai()
    {
        bMoveLeftClick = true;
    }

    public void ClickPhai()
    {
        bMoveRightClick = true;
    }

    public void ClickNhay()
    {
        bJumpClick = true;
    }

    /*
    1 - Up
    2 - Down
    3- Left
    4 - Right
    5 - Jump*/
    public bool InputOnKey(int ikey)
    {
        bool bRet = false;
        switch (ikey)
        {
            case 1:
                {
                    if (Input.GetKey("up") || (bMoveUp == true && bClick == true))
                    {
                        bRet = true;
                    }
                    break;
                }

            case 2:
                {
                    if (Input.GetKey("down") || (bMoveDown == true && bClick == true))
                    {
                        bRet = true;
                    }
                    break;
                }

            case 3:
                {
                    if (Input.GetKey("left") || (bMoveLeft == true && bClick == true))
                    {
                        bRet = true;
                    }
                    break;
                }

            case 4:
                {
                    if (Input.GetKey("right") || (bMoveRight == true && bClick == true))
                    {
                        bRet = true;
                    }
                    break;
                }

            case 5:
                {
                    if (Input.GetKey("space") || (bJump == true && bClick == true))
                    {
                        bRet = true;
                    }
                    break;
                }
        }
        return bRet;
    }


    /*
     1 - Up
     2 - Down
     3- Left
     4 - Right
     5 - Jump*/
    public bool InputKeyDown(int ikey)
    {
        bool bRet = false;
        switch(ikey)
        {
            case 1:
                {
                    if (Input.GetKeyDown("up") || (bMoveUpClick == true && bClick == true))
                    {
                        bRet = true;
                    }
                    break;
                }

            case 2:
                {
                    if (Input.GetKeyDown("down") || (bMoveDownClick == true && bClick == true))
                    {
                        bRet = true;
                    }
                    break;
                }

            case 3:
                {
                   if( Input.GetKeyDown ("left") || (bMoveLeftClick == true && bClick == true))
                   {
                       bRet = true;
                   }
                   break;
                }

            case 4:
                {
                    if (Input.GetKeyDown("right") || (bMoveRightClick == true && bClick == true))
                    {
                        bRet = true;
                    }
                    break;
                }

            case 5:
                {
                    if (Input.GetKeyDown("space") || (bJumpClick == true && bClick == true))
                    {
                        bRet = true;
                    }
                    break;
                }
        }
        
        return bRet;
    }

    /*
    1 - Up
    2 - Down
    3- Left
    4 - Right
    5 - Jump*/
    public bool InputkeyUp(int ikey)
    {
        bool bRet = false;
        switch (ikey)
        {
            case 1:
                {
                    bMoveUpClick = false;
                    break;
                }

            case 2:
                {
                    bMoveDownClick = false;
                    break;
                }

            case 3:
                {
                    bMoveLeftClick = false;
                    break;
                }

            case 4:
                {
                    bMoveRightClick = false;
                    break;
                }

            case 5:
                {
                    bJumpClick = false;
                    break;
                }
        }
        return bRet;
    }

    public bool InputAnykey()
    {
        if(Input.anyKey || bClick == true)
        {
            return true;
        }
        return false;
    }
}
