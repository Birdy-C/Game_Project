using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SynHealth : MonoBehaviour {
    public Slider m_Slider_from;
    public Slider m_Slider_to;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_Slider_to.value = m_Slider_from.value;
    }
}
