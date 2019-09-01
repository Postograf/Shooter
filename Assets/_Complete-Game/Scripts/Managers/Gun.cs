using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CompleteProject
{
    public enum GunType
    {
        SimpleGun,
        Ak47,
        Minigun,
        Shotgun
    }

    public class Gun : MonoBehaviour
    {
        public static string string_type;
        public static GunType enum_type;

        public static float effectsDisplayTime;
        public static float timeBetweenBullets;
        public static float bulletSpeed;
        public static int maxBulletsCount;
        public static int damagePerShot;

        protected float range;

        //Vector3 right;
        //Vector3 forward;

        static public ParticleSystem gunParticles; // Reference to the particle system.
        static public AudioSource gunAudio; // Reference to the audio source.
        static public Light gunLight; // Reference to the light component.
        static public Light faceLight; // Duh
        //public GameObject bullet;

        static public void SetType(string string_type_, GunType enum_type_)
        {
            string_type = string_type_;
            enum_type = enum_type_;
        }

        public float GetRange(float angle_range, Vector3 nowForward)
        {
            //print(right + " " + forward);
            float angel1 = Vector3.Angle(PlayerShooting.right, nowForward);
            float angel2 = Vector3.Angle(PlayerShooting.forward, nowForward);
            if (angel1 > 90 || (angel1 == angel2 * 2))
                angel2 *= -1;
            return Random.Range(-angle_range + angel2, angle_range + angel2);
        }

        public Gun()
        {

        }


        virtual public List<float> Shoot(Vector3 nowForward)
        {
            gunAudio.Play();
            gunLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();

            faceLight.enabled = true;
            List<float> result = new List<float>();
            result.Add(GetRange(0f, nowForward));
            return result;
        }
        void Start()
        {
            GunManager.SetType(GunType.Minigun);
            gunParticles = GetComponent<ParticleSystem>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();
            faceLight = GetComponentInChildren<Light>();
            //right = transform.right;
            //forward = transform.forward;
        }
    }
    public class Ak47 : Gun
    {
        public Ak47()
        {
            SetType("Ak47", GunType.Ak47);
            damagePerShot = 25;
            timeBetweenBullets = 0.07f;
            range = 100f;
            effectsDisplayTime = 0.1f;
            bulletSpeed = 100f;
            maxBulletsCount = 35;
        }
        //
    }
    public class Minigun : Gun
    {
        public Minigun()
        {
            SetType("Minigun", GunType.Minigun);
            damagePerShot = 5;
            timeBetweenBullets = 0.00001f;
            range = 100f;
            effectsDisplayTime = 0.00001f;
            bulletSpeed = 150f;
            maxBulletsCount = 500;
        }
        override public List<float> Shoot(Vector3 nowForward)
        {
            gunAudio.Play();
            gunLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();

            faceLight.enabled = true;
            List<float> result = new List<float>();
            result.Add(GetRange(10f, nowForward));
            return result;
        }

        //
    }
    public class SimpleWeapon : Gun
    {
        public SimpleWeapon()
        {
            SetType("SimpleWeapon", GunType.SimpleGun);
            damagePerShot = 50;
            timeBetweenBullets = 0.5f;
            range = 100f;
            effectsDisplayTime = 0.5f;
            bulletSpeed = 50f;
            maxBulletsCount = 6;
        }
    }
    public class Shotgun : Gun
    {
        public Shotgun()
        {
            SetType("Shotgun", GunType.Shotgun);
            damagePerShot = 15;
            timeBetweenBullets = 0.5f;
            range = 100f;
            effectsDisplayTime = 0.5f;
            bulletSpeed = 65f;
            maxBulletsCount = 7;
        }
        override public List<float> Shoot(Vector3 nowForward)
        {
            gunAudio.Play();
            gunLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();

            faceLight.enabled = true;
            List<float> result = new List<float>();
            for (int i = 0; i < 7; i++) {
                result.Add(GetRange(30f, nowForward));
            }
            return result;
        }

        //
    }

    public class GunManager : Gun
    {
        static public Gun current_gun;
        static public void SetType(GunType type)
        {
            switch (type)
            {
                case (GunType.Ak47):
                    current_gun = new Ak47();
                    break;
                case (GunType.Minigun):
                    current_gun = new Minigun();
                    break;
                case (GunType.SimpleGun):
                    current_gun = new SimpleWeapon();
                    break;
                case (GunType.Shotgun):
                    current_gun = new Shotgun();
                    break;
                default:
                    break;
            }
            BulletManager.Reload();
        }
    }

}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public enum GunType
    {
        SimpleGun,
        Ak47,
        Minigun,
        Shotgun
    }
    public class Gun : MonoBehaviour
    {
        public static float range;
        public static string string_type;
        public static int damagePerShot;
        public static float timeBetweenBullets;
        public static float effectsDisplayTime;
        public static float bulletSpeed;
        //public static int bulletsCount;
        public static int maxBulletsCount;

        static public void SetType(string string_type_)
        {
            string_type = string_type_;
        }

        public Gun()
        {
            string_type = "no weapon";
        }

        virtual public GunType Shoot()
        {
            // Play the gun shot audioclip.
            GunManager.gunAudio.Play();

            // Enable the lights.
            GunManager.gunLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            GunManager.gunParticles.Stop();
            GunManager.gunParticles.Play();

            GunManager.faceLight.enabled = true;
            return GunType.Ak47;
        }

    }
    public class Ak47 : Gun
    {
        public Ak47()
        {
            SetType("Ak47");
            damagePerShot = 25;
            timeBetweenBullets = 0.07f;
            range = 100f;
            effectsDisplayTime = 0.1f;
            bulletSpeed = 100f;
            maxBulletsCount = 35;
        }
        //
    }
    public class Minigun : Gun
    {
        public Minigun()
        {
            SetType("Minigun");
            damagePerShot = 5;
            timeBetweenBullets = 0.00001f;
            range = 100f;
            effectsDisplayTime = 0.00001f;
            bulletSpeed = 150f;
            maxBulletsCount = 500;
        }

        public override GunType Shoot()
        {
            // Play the gun shot audioclip.
            GunManager.gunAudio.Play();
            // Enable the lights.
            GunManager.gunLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            GunManager.gunParticles.Stop();
            GunManager.gunParticles.Play();

            GunManager.faceLight.enabled = true;
            return GunType.Minigun;
        }
    }

    public class SimpleWeapon : Gun
    {
        public SimpleWeapon()
        {
            SetType("SimpleWeapon");
            damagePerShot = 50;
            timeBetweenBullets = 0.5f;
            range = 100f;
            effectsDisplayTime = 0.5f;
            bulletSpeed = 50f;
            maxBulletsCount = 6;
        }
        //
    }

    public class Shotgun : Gun
    {
        public Shotgun()
        {
            SetType("Shotgun");
            damagePerShot = 50;
            timeBetweenBullets = 0.5f;
            range = 100f;
            effectsDisplayTime = 0.5f;
            bulletSpeed = 100f;
            maxBulletsCount = 7;
        }
        public override GunType Shoot()
        {
            // Play the gun shot audioclip.
            GunManager.gunAudio.Play();

            // Enable the lights.
            GunManager.gunLight.enabled = true;

            // Stop the particles from playing if they were, then start the particles.
            GunManager.gunParticles.Stop();
            GunManager.gunParticles.Play();

            GunManager.faceLight.enabled = true;
            return GunType.Shotgun;
        }
        //
    }

    public class GunManager : MonoBehaviour
    {
        static public Gun current_gun;
        static public ParticleSystem gunParticles;                    // Reference to the particle system.
        static public AudioSource gunAudio;                           // Reference to the audio source.
        static public Light gunLight;                                 // Reference to the light component.
        static public Light faceLight;								// Duh
 
        static public void SetType(GunType type)
        {
            switch (type)
            {
                case (GunType.Ak47):
                    current_gun = new Ak47();
                    break;
                case (GunType.Minigun):
                    current_gun = new Minigun();
                    break;
                case (GunType.SimpleGun):
                    current_gun = new SimpleWeapon();
                    break;
                case (GunType.Shotgun):
                    current_gun = new Shotgun();
                    break;
                default:
                    break;
            }
            BulletManager.Reload();
        }
        // Start is called before the first frame update
        void Start()
        {
            SetType(GunType.Minigun);
            gunParticles = GetComponent<ParticleSystem>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();
            faceLight = GetComponentInChildren<Light>();
        }
    }
}*/

