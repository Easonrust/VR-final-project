using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 || Input.GetMouseButton(0)) // if touching the screen, meaning when the player is pressing the cardboard viewer button (also if mouse click for testing purposes)
		{
			Vector3 moveVec = new Vector3(transform.forward.x * GameManager.move_velocity * Time.deltaTime,0, transform.forward.z * GameManager.move_velocity * Time.deltaTime);
			gameObject.transform.Translate(moveVec, Space.World);
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		Debug.Log("trigger:" + collider.gameObject.tag);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("collision:" + collision.gameObject.tag);
	}

}
