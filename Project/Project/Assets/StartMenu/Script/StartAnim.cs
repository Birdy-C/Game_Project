using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class StartAnim : MonoBehaviour {
    public Animator Camera1;
    public Animator Mouse1;
    public Animator Mouse2;

    // Use this for initialization
    void Start()
    {
        Mouse1.enabled = true;
        Mouse2.StopPlayback();
        Camera1.enabled = true;
        StartCoroutine("MyEvent");
    }
    // Update is called once per frame
    void Update () {
    }
    private IEnumerator MyEvent()
    {
        yield return new WaitForSeconds(1f);
        Mouse2.Play("exit");
    }
}
