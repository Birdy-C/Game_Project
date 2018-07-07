using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCanvas : MonoBehaviour {
	int enemyNumber = 6;
	int teammateNumber = 6;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < enemyNumber; i++) {
			GameObject enemyInfo = (GameObject) Instantiate(Resources.Load("enemy"), transform);
			enemyInfo.name = "enemy" + i.ToString();
			enemyInfo.transform.Translate (new Vector3 (0, -30 * i, 0));
			GameObject teammateInfo = (GameObject) Instantiate(Resources.Load("teammate"), transform);
			teammateInfo.name = "teammate" + i.ToString();
			teammateInfo.transform.Translate (new Vector3 (0, -30 * i, 0));
		}

		foreach (Transform child in transform) {
			if (child.gameObject.name.Contains("enemy") || child.gameObject.name.Contains("teammate")) {
				Sprite sp = Instantiate(Resources.Load("transparent", typeof(Sprite))) as Sprite;
				child.GetChild(1).GetComponent<Image> ().sprite = sp;
				child.GetChild(0).GetComponent<Image> ().color = new Color(0, 0, 0, 0);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("tab")) {
			foreach (Transform child in transform) {
				if (child.gameObject.name.Contains("enemy") || child.gameObject.name.Contains("teammate")) {
					Sprite sp = Instantiate (Resources.Load (child.gameObject.name, typeof(Sprite))) as Sprite;
					child.GetChild(1).GetComponent<Image> ().sprite = sp;
					child.GetChild(0).GetComponent<Image> ().color = new Color(0.2f, 1, 0, 0.6f);
				}
			} 
		}
		else {
			foreach (Transform child in transform) {
				if (child.gameObject.name.Contains("enemy") || child.gameObject.name.Contains("teammate")) {
					Sprite sp = Instantiate(Resources.Load("transparent", typeof(Sprite))) as Sprite;
					child.GetChild(1).GetComponent<Image> ().sprite = sp;
					child.GetChild(0).GetComponent<Image> ().color = new Color(0, 0, 0, 0);
				}
			}
		}
	}
}