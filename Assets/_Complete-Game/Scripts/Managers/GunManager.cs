using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CompleteProject
{
    public enum GunType
    {
        SimpleGun,
        Ak47,
        Laser
    }
    public class Gun : MonoBehaviour
    {
        public static string string_type;
        public static int damagePerShot;
        public static float timeBetweenBullets;
        public static float range;
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

        virtual public void Shoot(
           ref LineRenderer gunLine,
           ref Ray shootRay,
           ref RaycastHit shootHit,
           int shootableMask,
            Light faceLight)
        {
            //
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);


            //shootRay.origin = transform.position;
            //shootRay.direction = transform.forward;


            //if (Physics.Raycast(shootRay, out shootHit, range, shootableMask)) {

            //EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();


            //if (enemyHealth != null) {

            //enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            //}



            //gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            //}

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
            maxBulletsCount = 35;
        }
        //
    }
    public class Laser : Gun
    {
        public Laser()
        {
            SetType("Laser");
            damagePerShot = 5;
            timeBetweenBullets = 0.00001f;
            range = 100f;
            effectsDisplayTime = 0.00001f;
            maxBulletsCount = 500;
        }
        //
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
            maxBulletsCount = 6;
        }
        //
    }

    public class GunManager : MonoBehaviour
    {
        Text text;
        static public Gun current_gun;
        static public void SetType(GunType type)
        {
            switch (type)
            {
                case (GunType.Ak47):
                    current_gun = new Ak47();
                    break;
                case (GunType.Laser):
                    current_gun = new Laser();
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
        void Start()
        {
            text = GetComponent<Text>();
            SetType(GunType.Laser);
        }
        void Update()
        {
            text.text = "Weapon: " + Gun.string_type;
        }

    }

}
