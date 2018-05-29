using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceMouse : MonoBehaviour
{
    public Texture mouse;
    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {
        Cursor.visible = false;//隐藏鼠标指针
    }

    void OnGUI()
    {
        Vector2 msPos=Input.mousePosition;//鼠标的位置
        GUI.DrawTexture(new Rect(msPos.x, Screen.height-msPos.y, mouse.width, mouse.height), mouse);
    }

}
