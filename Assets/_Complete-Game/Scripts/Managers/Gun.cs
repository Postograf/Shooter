using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
    public enum GunType
    {
        SimpleGun,
        Ak47,
        Minigun
    }

    public class Gun : MonoBehaviour {
        public static string string_type;
        public static GunType enum_type;

        public static float effectsDisplayTime;
        public static float timeBetweenBullets;
        public static float bulletSpeed;
        public static int   maxBulletsCount;
        public static int   damagePerShot;

        protected float range;

        Vector3 right;
        Vector3 forward;

        static ParticleSystem gunParticles; // Reference to the particle system.
        static AudioSource gunAudio; // Reference to the audio source.
        static Light gunLight; // Reference to the light component.
        static Light faceLight; // Duh
        public GameObject bullet;

        static public void SetType(string string_type_, GunType enum_type_) {
            string_type = string_type_;
            enum_type = enum_type_;
        }

        protected float GetRange(float angle_range) {
            float angel1 = Vector3.Angle(right, transform.forward);
            float angel2 = Vector3.Angle(forward, transform.forward);
            if (angel1 > 90 || (angel1 == angel2 * 2))
                angel2 *= -1;
            return Random.Range(-angle_range + angel2, angle_range + angel2);
        }

        public Gun()
        {
            
        }


        virtual public void Shoot() {
            gunAudio.Play();
            gunLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();

            faceLight.enabled = true;
            //Bullet a = gameObject.AddComponent("te") as Bullet;
            Instantiate(
                bullet,
                transform.position,
                Quaternion.AngleAxis(
                    GetRange(10),
                    Vector3.up
                )
            );
            

        }
        void Start() {
            GunManager.SetType(GunType.Minigun);
            gunParticles = GetComponent<ParticleSystem>();
            gunAudio = GetComponent<AudioSource>();
            gunLight = GetComponent<Light>();
            faceLight = GetComponentInChildren<Light>();
            right = transform.right;
            forward = transform.forward;
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
       /* public override void Shoot() {
            //gunAudio.Play();
            gunLight.enabled = true;
            gunParticles.Stop();
            gunParticles.Play();

            faceLight.enabled = true;
            Instantiate(
                Bullet,
                transform.position,
                Quaternion.AngleAxis(GetRange(30f), Vector3.up)
            );
        }*/

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
        //
    }

    public class GunManager : Gun
    {
        static public Gun current_gun;
        static public void SetType(GunType type){
            switch (type){
                case (GunType.Ak47):
                    current_gun = new Ak47();
                    break;
                case (GunType.Minigun):
                    current_gun = new Minigun();
                    break;
                case (GunType.SimpleGun):
                    current_gun = new SimpleWeapon();
                    break;
                default:
                    break;
            }
            BulletManager.Reload();
        }
        // Start is called before the first frame update
        
    }

}
