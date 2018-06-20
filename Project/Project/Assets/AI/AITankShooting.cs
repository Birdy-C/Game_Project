using UnityEngine;
using UnityEngine.UI;

public class AITankShooting : MonoBehaviour
{

    public float shootSpeed = 0.3f;  //表示每秒发射子弹的个数 俗称子弹的发射速率

    private float shootTimer = 0;  //表示子弹的生成时间间隔 用来控制子弹的发射间隔

    private float shootTimerInterval = 0;  //表示子弹的间隔这个是一个固定的时间
    

    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
    public float m_MinLaunchForce = 10f;        // The force given to the shell if the fire button is not held.
    public float m_MaxLaunchForce = 20f;        // The force given to the shell if the fire button is held for the max charge time.
    public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.

    private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    public float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool m_Fired;                       // Whether or not the shell has been launched with this button press.

    private bool m_AIPrepare;
    private float m_timer;
    public float m_totalTime;

    private void Start()
    {
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
        m_AIPrepare = false;

        shootTimerInterval = 1 / shootSpeed;
    }


    private void Update()
    {

        shootTimer += Time.deltaTime;  //让子弹的时间控制器不断加等时间间隔

        if (shootTimer > shootTimerInterval)
        {  //如果子弹发射的时间间隔超过时间控制器　　那么我们就发射子弹

            if (m_AIPrepare)
            {
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                m_timer += Time.deltaTime;
                if (m_timer >= m_totalTime)
                {
                    m_AIPrepare = false;
                    Fire();
                }
            }
        }

    }
    
    private void Fire()
    {
        // Set the fired flag so only Fire is only called once.
        m_Fired = true;

        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        // Reset the launch force.  This is a precaution in case of missing button events.
        m_CurrentLaunchForce = m_MinLaunchForce;

        shootTimer = 0;
    }

    public void AIFire(float dist)
    {
        m_AIPrepare = true;
        m_timer = 0;
        m_totalTime = dist/40;

        // ... reset the fired flag and reset the launch force.
        m_Fired = false;
        m_CurrentLaunchForce = m_MinLaunchForce;

        // Change the clip to the charging clip and start it playing.
        m_ShootingAudio.clip = m_ChargingClip;
        m_ShootingAudio.Play();

    }

    public bool b_idle()
    {
        return !m_AIPrepare;
    }
}