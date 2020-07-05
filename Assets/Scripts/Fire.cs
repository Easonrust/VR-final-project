using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	public float speed = 8.0f;
	public int damage = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0,0,speed*Time.deltaTime);
	}
	private void OnTriggerEnter(Collider collision){
		if (collision.gameObject.tag == "Player") {
			GameManager.LIFE--;
			Destroy (gameObject);
		}
	}


}
