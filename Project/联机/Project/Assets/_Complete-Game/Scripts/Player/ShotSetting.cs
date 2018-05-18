using UnityEngine;
using UnityEngine.Networking;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{

    public class ShotSetting : NetworkBehaviour
    {
        public int damagePerShot = 20;                  // The damage inflicted by each bullet.
        public float timeBetweenBullets = 0.15f;        // The time between each shot.
        public float range = 100f;                      // The distance the gun can fire.

        [SyncVar]
        public bool whethershoot = false;
        [SyncVar]
        public bool updateshoot = false;

        float timer;                                    // A timer to determine when to fire.
        Ray shootRay = new Ray();                       // A ray from the gun end forwards.
        RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
        public Light faceLight;								// Duh
        float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.

        void Awake()
        {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask("Shootable");
        }



        void Update()
        {


            if (!isLocalPlayer)
            {
                // exit from update if this is not the local player
                return;
            }
            Object ShootObj = GetComponentInChildren<PlayerShooting>();
            if (!ShootObj)
            {
                return;
            }

            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;
            //Debug.Log(timer);
#if !MOBILE_INPUT

            // If the Fire1 button is being press and it's time to fire...
            // if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
            if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
            {
                Debug.Log("Shoot");
                // ... shoot the gun.
                CmdShoot(GetComponentInChildren<PlayerShooting>().transform.position, GetComponentInChildren<PlayerShooting>().transform.forward);
                timer = 0f;

            }
#else
            // If there is input on the shoot direction stick and it's time to fire...
            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
            {
                // ... shoot the gun
                Shoot();
            }
#endif
            // If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
            if (timer >= timeBetweenBullets * effectsDisplayTime)
            {
                CmdStop();
            }

        }


        [Command]
        void CmdShoot(Vector3 ori,Vector3 dire)
        {
            whethershoot = true;
            updateshoot = true;

            Debug.Log("CmdShoot");

            // Reset the timer.

            // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
            shootRay.origin = ori;
            shootRay.direction = dire;

            // Perform the raycast against gameobjects on the shootable layer and if it hits something...
            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                // Try and find an EnemyHealth script on the gameobject hit.
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                // If the EnemyHealth component exist...
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);
                }
            }
        }


        [Command]
        void CmdStop()
        {
            whethershoot = false;
            updateshoot = true;
        }

    }
}