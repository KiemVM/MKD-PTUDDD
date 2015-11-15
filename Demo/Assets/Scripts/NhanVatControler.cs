using UnityEngine;
using System.Collections;

public class NhanVatControler : MonoBehaviour {
	public float speed = 5;
	private Rigidbody2D myBody2d;
	private Animator myAnimator;
	public bool canJump = true;
	private bool facingRight = true;
	// Use this for initialization
	void Start () {
		myBody2d = this.rigidbody2D;
		myAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxisRaw ("Horizontal");
		myAnimator.SetFloat ("speed", Mathf.Abs(move));
		myBody2d.velocity = new Vector2 (move * speed, myBody2d.velocity.y);

		if (facingRight == true && move < 0) {
			facingRight = false;
			transform.rotation = Quaternion.Euler (transform.rotation.x, 180, 
			                                      transform.rotation.z);
		} else if (facingRight == false && move > 0) {
			facingRight = true;
			transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
		}

		if (Input.GetKeyDown (KeyCode.Space) && canJump == true) {
			myAnimator.SetBool("jump",true);
			canJump = false;
			myBody2d.velocity = new Vector2(myBody2d.velocity.x, 10);
		}
		myAnimator.SetBool("jump", false);
		canJump = true;
	}
}
