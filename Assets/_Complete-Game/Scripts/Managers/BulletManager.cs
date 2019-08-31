using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace CompleteProject
{
    public class BulletManager : MonoBehaviour
    {
        static public int count;
        Text text;
        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<Text>();
            GunManager.SetType(GunType.Minigun);
            Reload();
        }

        static public void Reload()
        {
            count = Gun.maxBulletsCount;
        }
        static public void Shoot(int count_ = 1)
        {
            count -= count_;
        }
        static public bool Empty()
        {
            return (count == 0);
        }
        // Update is called once per frame
        void Update()
        {
            text.text = "Bullets: " + count;
        }
    }
}