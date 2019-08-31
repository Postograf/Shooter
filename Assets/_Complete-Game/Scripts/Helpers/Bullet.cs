using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject {
    public class Bullet : MonoBehaviour
    {
        Rigidbody rb;
        float bulletSpeed = Gun.bulletSpeed;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        }

        void OnTriggerEnter(Collider other) {
            if (other.tag == "Wall") {
                Destroy(gameObject);
            } else if (other.tag == "Enemy") {
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(Gun.damagePerShot, transform.position);
                    Destroy(gameObject);
                }
                
            }
        }
    }
}
