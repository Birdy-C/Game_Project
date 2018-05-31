using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public List<GameObject> ItemList; 
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        foreach (var Item in ItemList)
        {
            Item.transform.rotation = transform.rotation;
        }
    }
}
