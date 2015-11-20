using UnityEngine;
using System.Collections;
using ColumnStructNS;
using MapStructNS;

public class Controller : MonoBehaviour {


	
	public const float fDistanceColumnX = 0.9f;
	public const float fDistanceColumnY = 0.8f;
	public const float fDelta = 0.1f;
	public const float fSpeedJumpX = 3.0f;
	public const float fSpeedJumpY = 5.0f;
	public const int iMaxCount = 12;
	public const float iMaxHighJumpDown = 3.5f;
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
	public float m_ColumnUp, m_ColumnDown;
	public float m_LeftColumnUp, m_LeftColumnDown;
	public float m_RightColumnUp, m_RightColumnDown;
	public int m_iState;
	public Animator m_animator;
	public int m_iCountAuto = 0;
	public int m_iStartDie = 0;

		
	// Use this for initialization
	void Start () {
		m_iState = 0;
		m_animator = GetComponent<Animator>();
		m_YJump = transform.position.y;
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

		if (Input.GetKeyDown ("space"))
		{
			m_YJump = transform.position.y;
			gameObject.rigidbody2D.velocity = new Vector2(0, fSpeedJumpY);
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
		}


		if (Input.GetKeyDown ("left")) 
		{
			m_animator.SetTrigger ("TriggerGoLeft");
			m_iState = iGoLeft;
		}

		if (Input.GetKeyDown ("right"))
		{
			m_animator.SetTrigger ("TriggerGoRight");
			m_iState = iGoRight;
		}
	}

	void ProcessGoLeft()
	{
		SetGravity (1);

		if (Input.anyKey)
			m_animator.speed = 1;
		else
			if (m_iCountAuto == iMaxCount) m_animator.speed = 0;

		if (Input.GetKeyDown ("space"))
		{
			m_YJump = transform.position.y;
			if (Input.GetKey("left")) 
				gameObject.rigidbody2D.velocity = new Vector2(-fSpeedJumpX, fSpeedJumpY);
			else
				gameObject.rigidbody2D.velocity = new Vector2(0, fSpeedJumpY);
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
		}


		if (Input.GetKey ("left"))
		{
			transform.Translate (-fDelta, 0, 0);
		}
		else
		{
			
		}
		if (Input.GetKey ("right"))
		{
			m_animator.SetTrigger ("TriggerGoRight");
			m_iState = iGoRight;
		}

	}
	void ProcessGoRight()
	{
		SetGravity (1);

		if (Input.anyKey)
			m_animator.speed = 1;
		else
			if (m_iCountAuto == iMaxCount) m_animator.speed = 0;

		if (Input.GetKeyDown ("space"))
		{
			m_YJump = transform.position.y;
			if (Input.GetKey("right"))
			    gameObject.rigidbody2D.velocity = new Vector2(fSpeedJumpX, fSpeedJumpY);
			else
				gameObject.rigidbody2D.velocity = new Vector2(0, fSpeedJumpY);

			m_animator.SetTrigger ("TriggerJumpRight");
			m_iState = iJumpRight;
		}

		if (Input.GetKey ("right"))
		{
			transform.Translate (fDelta, 0, 0);
		}
		else
		{
			
		}

		if (Input.GetKey ("left"))
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
			}
		}
	}

	void ProcessClimbLeft()
	{
		SetGravity (0);
		m_animator.speed = 1;

		if (Input.anyKey)
			m_animator.speed = 1;
		else
			

		if (Input.GetKey ("up"))
		{
			transform.Translate (0, fDelta, 0);
			return;
		}		
		else
		{
			
		}

		if (Input.GetKey ("down"))
		{
			transform.Translate (0, -2 * fDelta, 0);
			return;
		}
		else
		{
			
		}

		if (Input.GetKeyDown ("left"))
		{
			m_iCountAuto = 0;
			m_YJump = transform.position.y;
			m_animator.SetTrigger ("TriggerReachLeft");
			m_iState = iReachLeft;
			return;
		}

		if (Input.GetKeyDown ("right"))
		{
			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbRight");
			m_iState = iClimbRight;
			transform.Translate(2 * fDistanceColumnX, 0, 0);
			return;
		}

		if ((transform.position.y > m_ColumnUp + fDistanceColumnY) || (transform.position.y < m_ColumnDown - fDistanceColumnY))
		{
			m_YJump = transform.position.y;
			m_animator.SetTrigger ("TriggerJumpRight");
			m_iState = iJumpRight;
			return;
		}
	}

	void ProcessClimbRight()
	{
		SetGravity (0);
		m_animator.speed = 1;

		if (Input.anyKey)
			m_animator.speed = 1;
		else
			if (m_iCountAuto == iMaxCount) m_animator.speed = 0;

		if (Input.GetKey ("up"))
		{
			transform.Translate (0, fDelta, 0);
			return;
		}
		else
		{
			
		}

		if (Input.GetKey ("down"))
		{
			transform.Translate (0, -2 * fDelta, 0);
			return;
		}
		else
		{
			
		}


		if (Input.GetKeyDown ("right"))
		{
			m_iCountAuto = 0;
			m_YJump = transform.position.y;
			m_animator.SetTrigger ("TriggerReachRight");
			m_iState = iReachRight;
			return;
		}

		if (Input.GetKeyDown ("left"))
		{
			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbLeft");
			m_iState = iClimbLeft;
			transform.Translate(-2 * fDistanceColumnX, 0, 0);
			return;
		}

		if ((transform.position.y > m_ColumnUp + fDistanceColumnY) || (transform.position.y < m_ColumnDown - fDistanceColumnY))
		{
			m_YJump = transform.position.y;
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
			return;
		}
	}

	void ProcessClimbDouble()
	{
		SetGravity (0);
		m_animator.speed = 1;

		if (Input.anyKey)
			m_animator.speed = 1;
		else
			if (m_iCountAuto == iMaxCount) m_animator.speed = 0;

		if (Input.GetKey ("up"))
		{
			transform.Translate (0, 2 * fDelta, 0);
			return;
		}
		else
		{
			
		}
		if (Input.GetKey ("down"))
		{
			transform.Translate (0, -fDelta, 0);
			return;
		}
		else
		{
			
		}
	}

	void ProcessReachLeft()
	{
		SetGravity(0);
		m_animator.speed = 1;

		if (Input.GetKeyDown ("left"))
		{
			m_YJump = transform.position.y;
			transform.Translate(-0.1f, 0, 0);
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
			return;
		}

		if (Input.GetKeyDown ("right"))
		{
			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbLeft");
			m_iState = iClimbLeft;
			return;
		}
	}

	void ProcessReachRight()
	{
		SetGravity(0);
		m_animator.speed = 1;

		if (Input.GetKeyDown ("right"))
		{
			m_YJump = transform.position.y;
			transform.Translate(0.1f, 0, 0);
			m_animator.SetTrigger ("TriggerJumpRight");
			m_iState = iJumpRight;
			return;
		}

		if (Input.GetKeyDown ("left"))
		{
			m_iCountAuto = 0;
			m_animator.SetTrigger ("TriggerClimbRight");
			m_iState = iClimbRight;
			return;
		}
	}
	
	void Update() {
		m_iCountAuto++;
		if (m_iCountAuto > iMaxCount) 
			m_iCountAuto = iMaxCount;


		if (!((m_iState == iDieLeft) || (m_iState == iDieRight)))
			gameObject.rigidbody2D.isKinematic = false;

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
			m_ColumnUp = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yUp;
			m_ColumnDown = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yDown;
			if ((m_iState == iJumpLeft) || (m_iState == iGoLeft))
			{
				transform.Translate(coll.gameObject.transform.position.x - transform.position.x + fDistanceColumnX, 0, 0);
				m_iCountAuto = 0;
               	m_animator.SetTrigger ("TriggerClimbRight");
				m_iState = iClimbRight;
			}
			else if ((m_iState == iJumpRight)|| (m_iState == iGoRight))
			{
				transform.Translate(coll.gameObject.transform.position.x - transform.position.x - fDistanceColumnX, 0, 0);
				m_iCountAuto = 0;
				m_animator.SetTrigger ("TriggerClimbLeft");
				m_iState = iClimbLeft;
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
			m_YJump = transform.position.y;
		}
	}
}
