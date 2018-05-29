using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SlightMove : MonoBehaviour
{

    public GameObject target;
    public float turnSpeed = 1f;
    private Vector3 camAng;
    public float xrecord = 0, yrecord = 0;

    void Start()
    {
        //camera的初始位置（手动放好）
        camAng = target.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        // 鼠标调整视角
        RotMove();
    }

    void RotMove()
    {
        //鼠标移动视角
        camAng = target.transform.eulerAngles;
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");
        if (Math.Abs(xrecord - x) < 15)
        {
            xrecord -= x;
            camAng.x -= x;
        }
        if (Math.Abs(yrecord + y) < 10)
        {
            yrecord += y;
            camAng.y += y;
        }

        target.transform.eulerAngles = camAng;
        //Debug.Log(camAng);
        //相机保持相对位置
    }
}