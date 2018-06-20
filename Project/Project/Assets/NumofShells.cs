using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumofShells : MonoBehaviour {

    public Slider k;
    GameObject tank;
    public TankShooting player;
    // Use this for initialization
    void Start()
    {
        tank = GameObject.Find("_Player");
        player = tank.GetComponent<TankShooting>();
    }

    // Update is called once per frame
    void Update () {
        k.value = player.m_CurrentNumofShells;
    }
}
