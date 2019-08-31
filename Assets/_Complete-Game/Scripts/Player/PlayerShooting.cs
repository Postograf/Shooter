using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject {
    public class PlayerShooting : MonoBehaviour {
        private int damagePerShot;                  // The damage inflicted by each bullet.
        private float timeBetweenBullets;        // The time between each shot.
        private float range;                      // The distance the gun can fire.


        float timer;                                    // A timer to determine when to fire.
        Ray shootRay = new Ray();                       // A ray from the gun end forwards.
        RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
        int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
        ParticleSystem gunParticles;                    // Reference to the particle system.
        LineRenderer gunLine;                           // Reference to the line renderer.
        AudioSource gunAudio;                           // Reference to the audio source.
        Light gunLight;                                 // Reference to the light component.
        public Light faceLight;								// Duh
        float effectsDisplayTime;                // The proportion of the timeBetweenBullets that the effects will display for.

        public GameObject Bullet;

        void UpdateValues() {
            damagePerShot = Gun.damagePerShot;
            timeBetweenBullets = Gun.timeBetweenBullets;
            range = Gun.range;
            effectsDisplayTime = Gun.effectsDisplayTime;
        }

        void Awake() {
            // Create a layer mask for the Shootable layer.
            shootableMask = LayerMask.GetMask("Shootable");

            // Set up the references.
            gunParticles = GetComponent<ParticleSystem>();
            gunLine = GetComponent<LineRenderer>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();
            //faceLight = GetComponentInChildren<Light> ();

        }


        void Update() {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

#if !MOBILE_INPUT
            // If the Fire1 button is being press and it's time to fire...
            if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) {
                // ... shoot the gun.
                if (!BulletManager.Empty()) {
                    BulletManager.Shoot();
                    //GunManager.current_gun.Shoot();
                    Shoot();
                }
            }

            if (Input.GetButton("Fire2")) {
                BulletManager.Reload();
            }
            if (Input.GetKey(KeyCode.Alpha1)) {
                GunManager.SetType(GunType.Ak47);
                UpdateValues();
            }
            if (Input.GetKey(KeyCode.Alpha2)) {
                GunManager.SetType(GunType.Laser);
                UpdateValues();
            }
            if (Input.GetKey(KeyCode.Alpha3)) {
                GunManager.SetType(GunType.SimpleGun);
                UpdateValues();
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
            if (timer >= timeBetweenBullets * effectsDisplayTime) {
                // ... disable the effects.
                DisableEffects();
            }
        }


        public void DisableEffects() {
            // Disable the line renderer and the light.
            gunLine.enabled = false;
            faceLight.enabled = false;
            gunLight.enabled = false;
        }


        void Shoot() {
            // Reset the timer.
            timer = 0f;

            // Play the gun shot audioclip.
            gunAudio.Play();

            // Enable the lights.
            gunLight.enabled = true;


            // Stop the particles from playing if they were, then start the particles.
            gunParticles.Stop();
            gunParticles.Play();

            faceLight.enabled = true;
            gunLine.enabled = true;
            //GunManager.current_gun.Shoot(ref gunLine, ref shootRay, ref shootHit, shootableMask, faceLight);
            Instantiate(Bullet, transform.position, transform.rotation);
        }
    }
}