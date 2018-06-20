using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public float shootSpeed = 2f;  //表示每秒发射子弹的个数 俗称子弹的发射速率

    private float shootTimer = 0f;  //表示子弹的生成时间间隔 用来控制子弹的发射间隔

    private float shootTimerInterval = 0f;  //表示子弹的间隔这个是一个固定的时间

    public int m_CurrentNumofShells;
    public int m_MaxNumofShells = 10;
    public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
    public float m_MinLaunchForce = 10f;        // The force given to the shell if the fire button is not held.
    public float m_MaxLaunchForce = 20f;        // The force given to the shell if the fire button is held for the max charge time.
    public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.
    public bool isNull = false;


    private string m_FireButton;                // The input axis that is used for launching shells.
    private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool m_Fired;                       // Whether or not the shell has been launched with this button press.


    private void OnEnable()
    {
        // When the tank is turned on, reset the launch force and the UI
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        m_CurrentNumofShells = m_MaxNumofShells;
    }


    private void Start()
    {
        // The fire axis is based on the player number.
        m_FireButton = "Fire" + m_PlayerNumber;

        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

        shootTimerInterval = 1 / shootSpeed;
    }


    private void Update()
    {

        shootTimer += Time.deltaTime;  //让子弹的时间控制器不断加等时间间隔

        // The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;

        if (m_CurrentNumofShells > 0)
        {
            if (shootTimer > shootTimerInterval)
            {
                if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
                {
                    // ... use the max force and launch the shell.
                    m_CurrentLaunchForce = m_MaxLaunchForce;
                    Fire();
                }
                // Otherwise, if the fire button has just started being pressed...
                else if (Input.GetButtonDown(m_FireButton))
                {
                    // ... reset the fired flag and reset the launch force.
                    m_Fired = false;
                    m_CurrentLaunchForce = m_MinLaunchForce;

                    // Change the clip to the charging clip and start it playing.
                    m_ShootingAudio.clip = m_ChargingClip;
                    m_ShootingAudio.Play();
                }
                // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
                else if (Input.GetButton(m_FireButton) && !m_Fired)
                {
                    // Increment the launch force and update the slider.
                    m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

                    m_AimSlider.value = m_CurrentLaunchForce;
                }
                // Otherwise, if the fire button is released and the shell hasn't been launched yet...
                else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
                {
                    // ... launch the shell.
                    Fire();
                }
            }
            // If the max force has been exceeded and the shell hasn't yet been launched...


        }
        else if (m_CurrentNumofShells == 0)
        {
            isNull = true;
            //屏幕显示
        }

    }

    private void OnGUI()
    {
        if (isNull)
            GUI.Label(new Rect(Screen.width * 0.5f - 40, Screen.height * 0.5f - 15, 100, 30), "Out of Shells！");
    }


    public void AddNumofShells() {
        if (m_CurrentNumofShells < m_MaxNumofShells)
        {
            m_CurrentNumofShells++;
        }
    }

    public bool IsMax() {
        if (m_CurrentNumofShells == m_MaxNumofShells)
        {
            return true;
        }
        else return false;
    }

    private void Fire()
    {
        // Set the fired flag so only Fire is only called once.
        m_Fired = true;

        // Create an instance of the shell and store a reference to it's rigidbody.
        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        // Reset the launch force.  This is a precaution in case of missing button events.
        m_CurrentLaunchForce = m_MinLaunchForce;

        m_CurrentNumofShells--;

        shootTimer = 0;
    }
}