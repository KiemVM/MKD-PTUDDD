using UnityEngine;
using System.Collections;
using ColumnStructNS;

public class Controller : MonoBehaviour {


	
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;

	public bool bIsClimb = false;
	private float m_ColumnUp, m_ColumnDown;
	public int m_iState;
	Animator m_animator;

	private const int 
		iIdle = 0,
		iGoLeft = 1,
		iGoRight = 2,
		iClimbLeft = 3,
		iClimbRight = 4,
		iClimbDouble = 5,
		iJumpLeft = 6,
		iJumpRight = 7;
		
	// Use this for initialization
	void Start () {
		m_iState = 0;
		m_animator = GetComponent<Animator>();
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


		if (Input.GetKey ("space"))
		{
			transform.Translate (0, 0.1f, 0);
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
		}


		if (Input.GetKey ("left")) 
		{
			m_animator.SetTrigger ("TriggerGoLeft");
			m_iState = iGoLeft;
		}

		if (Input.GetKey ("right"))
		{
			m_animator.SetTrigger ("TriggerGoRight");
			m_iState = iGoRight;
		}
	}

	void ProcessGoLeft()
	{


		if (Input.GetKey ("space"))
		{
			transform.Translate (0, 0.1f, 0);
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
		}

		if (Input.GetKey ("left"))
			transform.Translate (-0.1f, 0, 0);
		if (Input.GetKey ("right"))
		{
			m_animator.SetTrigger ("TriggerGoRight");
			m_iState = iGoRight;
		}

	}
	void ProcessGoRight()
	{

		if (Input.GetKey ("space"))
		{
			transform.Translate (0, 0.1f, 0);
			m_animator.SetTrigger ("TriggerJumpRight");
			m_iState = iJumpRight;
		}
		if (Input.GetKey ("right"))
			transform.Translate (0.1f, 0, 0);
		if (Input.GetKey ("left"))
		{
			m_animator.SetTrigger ("TriggerGoLeft");
			m_iState = iGoLeft;
		}
	}

	void ProcessJumpLeft()
	{
		if (Input.GetKey ("space"))		
			transform.Translate (0, 0.1f, 0);

		if (Input.GetKey ("left"))
			transform.Translate (-0.1f, 0, 0);

	}
	void ProcessJumpRight()
	{
		if (Input.GetKey ("space"))
			transform.Translate (0, 0.1f, 0);

		if (Input.GetKey ("right"))
			transform.Translate (0.1f, 0, 0);
	}

	void ProcessClimbLeft()
	{


		if (Input.GetKey ("up"))
			transform.Translate (0, 0.1f, 0);
		if (Input.GetKey ("down"))
			transform.Translate (0, -0.2f, 0);

		if (Input.GetKey ("left"))
		{
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
		}

		if ((transform.position.y > m_ColumnUp) || (transform.position.y < m_ColumnDown))
		{
			m_animator.SetTrigger ("TriggerJumpRight");
			m_iState = iJumpRight;
		}
	}

	void ProcessClimbRight()
	{


		if (Input.GetKey ("up"))
			transform.Translate (0, 0.1f, 0);
		if (Input.GetKey ("down"))
			transform.Translate (0, -0.2f, 0);

		if (Input.GetKey ("right"))
		{
			m_animator.SetTrigger ("TriggerJumpRight");
			m_iState = iJumpRight;
		}

		if ((transform.position.y > m_ColumnUp) || (transform.position.y < m_ColumnDown))
		{
			m_animator.SetTrigger ("TriggerJumpLeft");
			m_iState = iJumpLeft;
		}
	}

	void ProcessClimbDouble()
	{
		if (Input.GetKey ("up"))
			transform.Translate (0, 0.2f, 0);
		if (Input.GetKey ("down"))
			transform.Translate (0, -0.1f, 0);
	}

	void Update() {

		/*string sAnimationName;

		sAnimationName = GetCurrentPlayingAnimationClip;
		*/
		//string sAnimation = GetCurrentPlayingAnimationClip ();

		if ((m_iState == iClimbLeft) 
			|| (m_iState == iClimbRight)
			|| (m_iState == iClimbDouble))
			SetGravity (0);
		else
			SetGravity (1);

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
		}
		


		
	}



	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.tag == "Column")
		{
			//bIsClimb = true;

			m_ColumnUp = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yUp;
			m_ColumnDown = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yDown;
			if (m_iState == iJumpLeft)
			{
				transform.Translate(coll.gameObject.transform.position.x - transform.position.x + 1.0f, 0, 0);

               	m_animator.SetTrigger ("TriggerClimbRight");
				m_iState = iClimbRight;
			}
			else if (m_iState == iJumpRight)
			{
				transform.Translate(coll.gameObject.transform.position.x - transform.position.x - 1.0f, 0, 0);

				m_animator.SetTrigger ("TriggerClimbLeft");
				m_iState = iClimbLeft;
			}
		}

		if (m_iState == iJumpLeft)
		{
			m_animator.SetTrigger ("TriggerGoLeft");
			m_iState = iGoLeft;
		}
		else if (m_iState == iJumpRight)
		{
			m_animator.SetTrigger ("TriggerGoRight");
			m_iState = iGoRight;
		}
	}

	/*void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Column")
		{
			bIsClimb = false;
		}
	}*/

}
