using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tan_kHealth : MonoBehaviour {

    public Slider health;
    GameObject tank;
    public TankHealth player;
	// Use this for initialization
	void Start ()
    {
        tank = GameObject.Find("_Player");
        player = tank.GetComponent<TankHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        health.value = player.m_CurrentHealth;
    }
}
