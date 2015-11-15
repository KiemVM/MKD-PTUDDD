using UnityEngine;
using System.Collections;

public class FruitController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	GameObject m_obPlayer;
	// Update is called once per frame
	void Update () {
	
	}

	void SetGravity(int nValue)
	{
		if (this.rigidbody2D.gravityScale == nValue)
			return;
		this.rigidbody2D.isKinematic = true;
		this.rigidbody2D.gravityScale = nValue;
		this.rigidbody2D.isKinematic = false;
	}

	void OnTriggerEnter2D(Collider2D myTrigger){
		
		if (myTrigger.gameObject.tag == "Player")
		{
			SetGravity (1);
		}
		if (myTrigger.gameObject.tag == "Water")
		{
			Destroy(this.gameObject);
		}
	}

}
