using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities
{
    public abstract class EntityMovement : MonoBehaviour
    {
        [SerializeField]
        protected float speed = 10f;
        [SerializeField]
        protected float jumpForce = 400f;

        [SerializeField]
        protected LayerMask whatIsGround;

        [SerializeField] [Range(0, .3f)]
        protected float smoothingFactor = .05f;

        protected Vector3 velocity = Vector3.zero;

        protected Rigidbody2D rigidBody;
        protected CircleCollider2D circleCollider; 

        protected bool shouldJump = false;
        protected bool isFacingRight = true;
        protected bool isGrounded = true;


        public abstract void Move(float x, float y);

        public abstract void Jump();

        public abstract bool IsGrounded();
    }
}


