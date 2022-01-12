using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities
{
    public class PlayerMovement : EntityMovement
    {
        private Rigidbody2D rigidbody2D;


        private void Awake() {
            rigidbody2D = this.GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            
        }


        public override void Move(float x, float y) 
        {
            Debug.Log(x + " " + y);
        }

        public override void Jump() 
        {
            Debug.Log("Jump!");
        } 
    }
}


