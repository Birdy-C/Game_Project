using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		// Debug.Log(collosion.collider.name);
		if(other.gameObject.name == "_Player"){
            if (GameObject.Find("_Player").GetComponent<TankShooting>().IsMax() == true)
            { }
            else
            {
                Destroy(this.gameObject);
                GameObject.Find("_Player").GetComponent<TankShooting>().AddNumofShells();
            }
		}
	}
}
