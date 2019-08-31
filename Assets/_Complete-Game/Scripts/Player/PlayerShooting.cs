using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject {
    public class PlayerShooting : MonoBehaviour {
        private float timeBetweenBullets;        // The time between each shot.
        float effectsDisplayTime;                // The proportion of the timeBetweenBullets that the effects will display for.

        float timer;                                    // A timer to determine when to fire.

        Light gunLight;                                 // Reference to the light component.
        Light faceLight;                              // Duh
        Vector3 right;
        Vector3 forward;

        public GameObject Bullet;

        void UpdateValues() {
            timeBetweenBullets = Gun.timeBetweenBullets;
            effectsDisplayTime = Gun.effectsDisplayTime;
        }

        void Awake() {
            gunLight = GetComponent<Light>();
            faceLight = GetComponentInChildren<Light> ();
            right = transform.right;
            forward = transform.forward;
        }


        void Update() {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

#if !MOBILE_INPUT
            // If the Fire1 button is being press and it's time to fire...
            if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0) {
                // ... shoot the gun.
                if (!BulletManager.Empty()) {
                    timer = 0f;
                    BulletManager.Shoot();
                    if (GunType.Ak47 == GunManager.current_gun.Shoot())
                    {
                        float angel1 = Vector3.Angle(right, transform.forward);
                        float angel2 = Vector3.Angle(forward, transform.forward);
                        if (angel1 > 90 || (angel1 == angel2 * 2))
                            angel2 *= -1;
                        Instantiate(Bullet, transform.position, Quaternion.AngleAxis(Random.Range(-5f + angel2, 5f + angel2), Vector3.up));
                    }
                    //GunManager.current_gun.Shoot();
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
                GunManager.SetType(GunType.Minigun);
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
            faceLight.enabled = false;
            gunLight.enabled = false;
        }
    }
}