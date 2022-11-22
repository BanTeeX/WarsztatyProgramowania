using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Pawel_Lukaszewski
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        public float movSpeed = 3;
        public float inputSpeedModifier = 4;
        public float gravityScale = 1;

        private Rigidbody2D rigid;

        private float _currentMovSpeed;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            rigid.gravityScale = 0f;
        }

        private void FixedUpdate()
        {
            rigid.velocity = -transform.up * gravityScale + transform.right * (movSpeed + Input.GetAxis("Horizontal") * inputSpeedModifier);
        }
    }
}