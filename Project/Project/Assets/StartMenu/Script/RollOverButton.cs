using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RollOverButton : MonoBehaviour {

    private Animator _animator;
	// Use this for initialization
	void Start () {
        _animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		
	}
    //鼠标在物体上面引起的动作  
    public void OnMouseOver()
    {
        _animator.SetBool("RollOver",true);

    }
    //鼠标不再上面引起的动作  
    public void OnMouseExit()
    {
        _animator.SetBool("RollOver", false);
    }
}
