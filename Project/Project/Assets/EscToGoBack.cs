using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscToGoBack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			//Debug.Log("Esc");
			StartCoroutine(LoadScene("_StartMenu"));
		}
	}

	IEnumerator LoadScene(string sceneName)  
    {  
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);  
        yield return new WaitForEndOfFrame();  
        op.allowSceneActivation = true;  
  
    }
}
