using UnityEngine;
using System.Collections;

public class ControllerEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D myTrigger){
		
		if (myTrigger.gameObject.tag == "Fruit")
		{
			if (myTrigger.gameObject.rigidbody2D.gravityScale == 1)
				Destroy(this.gameObject);
		}
	}
}
