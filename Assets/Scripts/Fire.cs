using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	public float speed = 4.0f;
	public int damage = 5;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0,0,speed*Time.deltaTime);
	}
	private void OnTriggerEnter(BoxCollider collision){
		Debug.Log ("ha");
		if (collision.gameObject.layer == 8) {
			GameManager.LIFE--;
			Debug.Log ("haha");
			Destroy (gameObject);
		}
	}


}
