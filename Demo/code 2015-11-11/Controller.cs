using UnityEngine;
using System.Collections;
using ColumnStructNS;

public class Controller : MonoBehaviour {


	
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;

	public bool bIsClimb = false;
	private float m_ColumnUp, m_ColumnDown;
	private int m_iState;
	Animator m_animator;

	private int 
		iGoLeft = 1,
		iGoRight = 2,
		iClimbLeft = 3,
		iClimbRight = 4,
		iClimbDouble = 5;

	// Use this for initialization
	void Start () {
		m_animator = GetComponent<Animator>();
	}

	public string GetCurrentPlayingAnimationClip {
				get {
						foreach (AnimationState anim in animation) {
								if (animation.IsPlaying (anim.name)) {
										return anim.name;
								}
						}

						return string.Empty;
				}
		}


	void Update() {

		/*string sAnimationName;

		sAnimationName = GetCurrentPlayingAnimationClip;
		*/

		if (Input.GetKey ("space"))
			transform.Translate (0, 0.1f, 0);
		if (Input.GetKey ("up"))
			transform.Translate (0, 0.1f, 0);
		/*if (Input.GetKey ("down"))
			transform.Translate (0, -0.1f, 0);*/
		if (Input.GetKey ("left"))
			m_animator.SetTrigger ("TriggerGoLeft");
		if (Input.GetKey ("right"))
			m_animator.SetTrigger ("TriggerGoRight");


		if (bIsClimb) 
		{
			if (Input.GetKey ("up"))
				transform.Translate (0, 0.1f, 0);
			if (Input.GetKey ("down"))
				transform.Translate (0, -0.1f, 0);
			if ((transform.position.y > m_ColumnUp) || (transform.position.y < m_ColumnDown))
			{
				bIsClimb = false;
				this.rigidbody2D.isKinematic = true;
				this.rigidbody2D.gravityScale = 1;
				this.rigidbody2D.isKinematic = false;
			}
		} 
		else 
		{
			if (Input.GetKey ("right"))
					transform.Translate (0.1f, 0, 0);
			if (Input.GetKey ("left"))
					transform.Translate (-0.1f, 0, 0);
		}
		
	}



	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Column")
		{
			bIsClimb = true;
  			this.rigidbody2D.isKinematic = true;
			this.rigidbody2D.gravityScale = 0;
			this.rigidbody2D.isKinematic = false;
			m_ColumnUp = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yUp;
			m_ColumnDown = ((ColumnStruct) coll.gameObject.GetComponent("ColumnStruct")).yDown;

		}

	}

	/*void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Column")
		{
			bIsClimb = false;
		}
	}*/

}
