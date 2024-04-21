using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TrianCatW
{
    public class CharacterController : MonoBehaviour
    {
        public float speed = 1;
        public float rotateSpeed = 100;
        private Animator _animator;

        private void Awake()
        {
            Init();
        }
        private Vector3 velocity;
        void Update()
        {
            velocity.x = direction.x;
            velocity.z = direction.y;
            transform.position += velocity * speed * Time.deltaTime;
            if (velocity != Vector3.zero)
            {
                isMoving = true;
                // 计算角色的运动方向
                Vector3 movementDirection = velocity.normalized;

                // 让角色朝向运动方向
                Quaternion newRotation = Quaternion.LookRotation(movementDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);
            }
            else
            {
                isMoving = false;
            }
            _animator.SetBool("isMoving", isMoving);
        }
        void Init()
        {
            _animator = GetComponent<Animator>();
        }
        
        
        private Vector2 direction;
        private bool isMoving;
        void OnMove(InputValue value)
        {
            direction = value.Get<Vector2>();
            
        }

        public static event Action Interact;
        void OnInteract(InputValue value)
        {
            Interact?.Invoke();
        }
    }
}

