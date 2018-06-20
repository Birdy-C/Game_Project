using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour {
	public bool b_alive;

	// Use this for initialization
	void Start () {
		b_alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void bePicked() {
		Destroy(this.gameObject);
	}

	void OnTriggerEnter() {
		b_alive = false;
	}
}
