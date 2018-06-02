using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    public float moveSpeed = 10f;
    public float rotSpeed = 1f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        // 方向键移动事件
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            Debug.Log("I am coming!");
            //transform.position += (GetComponentInChildren<rayshooter>().gunEnd.position -
            //    GetComponentInChildren<rayshooter>().gunStart.position).normalized * moveSpeed * Time.deltaTime;
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
            //相机跟随
            //Camera.main.transform.Translate(this.transform.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            //Debug.Log("I am back!");
            //transform.position -= (GetComponentInChildren<rayshooter>().gunEnd.position -
            //    GetComponentInChildren<rayshooter>().gunStart.position).normalized * moveSpeed * Time.deltaTime;
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
            //相机跟随
           // Camera.main.transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            //Debug.Log("Turn left!");
            //车辆旋转
            transform.Rotate(0, -rotSpeed, 0);
            //相机跟随旋转
           // Camera.main.transform.RotateAround(transform.position, transform.up, -rotSpeed);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            //Debug.Log("Turn right!");
            //车辆旋转
            transform.Rotate(0,  rotSpeed, 0);
            //相机跟随旋转
           // Camera.main.transform.RotateAround(transform.position,transform.up, rotSpeed);
        }
    }
}
