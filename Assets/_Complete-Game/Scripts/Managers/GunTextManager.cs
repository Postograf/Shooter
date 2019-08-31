using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{
    public class GunTextManager : MonoBehaviour
    {
        Text text;
        void Start()
        {
            text = GetComponent<Text>();
        }
        void Update()
        {
            text.text = "Weapon: " + Gun.string_type;
        }
    }
}
