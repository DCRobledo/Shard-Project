using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities
{
    public class PlayerMovement : EntityMovement
    {
        
        private void Awake() {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            
        }

        public override void Move(float x, float y) 
        {
            Vector3 targetVelocity = new Vector2(x, y) * speed;
            rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocity, ref velocity, smoothingFactor);
        }

        public override void Jump() 
        {
            Debug.Log("Jump!");
        } 
    }
}


