using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour {
    //设置坦克移动的速度
    public float tank_MoveSpeed = 5f;
    //设置坦克转向的速度
    public float tank_AngleSpeed = 5f;
    //用于播放引擎声音的音频源
    public AudioSource tank_MoveAudio;
    //当坦克不移动的时候播放的音频
    public AudioClip tank_EngineIdling;
    //当坦克移动的时候播放的音频
    public AudioClip tank_EngineDriving;
    //发动机噪声音高的变化范围
    public float tank_PitchRange = 0.2f;

    //前后移动的轴名称
    private string tank_MoveAxisName;
    //左右转向的轴名称
    private string tank_AngleAxisName;
    private Rigidbody tank_Rigidbody;
    //运动输入的当前值
    private float tank_MoveInputValue;
    //转向输入的当前值
    private float tank_AngleInputValue;
    //音频源在场景开始时的音高
    private float tank_OriginalPitch;

    private void Awake()
    {
        tank_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //Is Kinematic是否遵循动力学。
        //如果值为true表示该物体运动状态不受外力，碰撞和关节的影响
        //只受动画以及附加在物体上的脚本影响，但该物体仍然能改变其他物体运动状态。
        tank_Rigidbody.isKinematic = false;

        //初始化当前数值
        tank_MoveInputValue = 0f;
        tank_AngleInputValue = 0f;
    }

    private void OnDisable()
    {
        tank_Rigidbody.isKinematic = true;
    }

    // Use this for initialization
    void Start () {
        //如果多个物体可以在后面加playernumber之类的东西便于区分
        tank_MoveAxisName = "Vertical";
        tank_AngleAxisName = "Horizontal";
        tank_OriginalPitch = tank_MoveAudio.pitch;
	}
	
	// Update is called once per frame
	void Update () {
        tank_MoveInputValue = Input.GetAxis(tank_MoveAxisName);
        tank_AngleInputValue = Input.GetAxis(tank_AngleAxisName);

        EngineAudio();
	}

    private void EngineAudio()
    {
        //如果检测到坦克的当前移动值和当前转向值都小于一个设定的值，判定坦克处于静止状态
        if(Mathf.Abs(tank_MoveInputValue) < 0.1f && Mathf.Abs(tank_AngleInputValue) < 0.1f)
        {
            //如果坦克当前的音频是行动时的音频，改成静止时的音频，同时调整音高
            if(tank_MoveAudio.clip == tank_EngineDriving)
            {
                tank_MoveAudio.clip = tank_EngineIdling;
                tank_MoveAudio.pitch = Random.Range(tank_OriginalPitch - tank_PitchRange, tank_OriginalPitch + tank_PitchRange);
                tank_MoveAudio.Play();
            }
        }
        else
        {
            if(tank_MoveAudio.clip == tank_EngineIdling)
            {
                tank_MoveAudio.clip = tank_EngineDriving;
                tank_MoveAudio.pitch = Random.Range(tank_OriginalPitch - tank_PitchRange, tank_OriginalPitch + tank_PitchRange);
                tank_MoveAudio.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement;
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow))
        {
            movement = transform.forward * tank_MoveInputValue * tank_MoveSpeed * Time.deltaTime;
            tank_Rigidbody.MovePosition(tank_Rigidbody.position + movement);
        }
        if(Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow))
        {
            movement = transform.forward * tank_MoveInputValue * tank_MoveSpeed * Time.deltaTime;
            tank_Rigidbody.MovePosition(tank_Rigidbody.position + movement);
        }
    }

    private void Turn()
    {
        float turn;
        Quaternion turnRotation;
        if(Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
        {
            turn = tank_AngleInputValue * tank_AngleSpeed * Time.deltaTime;
            turnRotation = Quaternion.Euler(0f, turn, 0f);
            tank_Rigidbody.MoveRotation(tank_Rigidbody.rotation * turnRotation);
        }
        if(Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow))
        {
            turn = tank_AngleInputValue * tank_AngleSpeed * Time.deltaTime;
            turnRotation = Quaternion.Euler(0f, turn, 0f);
            tank_Rigidbody.MoveRotation(tank_Rigidbody.rotation * turnRotation);
        }
    }
}
