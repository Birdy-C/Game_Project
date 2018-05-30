using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class init : MonoBehaviour
{
    //进度条
    public Slider progressBar;
    
    //下一个场景的名字
    //记得要在building setting里面加入
    public string scene;
        
    //loading场景的图片
    public List<Sprite> imgs;

    //异步对象  
    private AsyncOperation async;

    //读取场景的进度，它的取值范围在0 - 1 之间。  
    private int progress = 0;
    // 目标进度
    private float target = 0;

    void Start()
    {
        Debug.Log("Load Next Scene");

        async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;
        progressBar.value = 0;

        // 开启协程，开始调用加载方法
        StartCoroutine(processLoading());

    }

    //注意这里返回值一定是 IEnumerator  
    IEnumerator processLoading()
    {
        //据说加上这么一句就可以先显示加载画面然后再进行加载
        yield return new WaitForEndOfFrame();
        while (true)
        {
            target = async.progress; // 进度条取值范围0~1
            if (target >= 0.9f)
            {
                target = 1;
                yield break;
            }
            yield return 0;
        } 
    }

    private float waitTime = 2f;
    private float timer;
    void playSlider()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            int num = Random.Range(0, 4);
            GetComponentInChildren<Image>().sprite = imgs[num];
            timer = 0f;
        }      
    }

    private float dtimer = 0;
    void Update()
    {
        playSlider();
        progressBar.value = Mathf.Lerp(progressBar.value, target, dtimer * 0.02f);
        dtimer += Time.deltaTime;
        if (progressBar.value > 0.99f)
        {
            progressBar.value = 1;
            async.allowSceneActivation = true;
        }
    }
}