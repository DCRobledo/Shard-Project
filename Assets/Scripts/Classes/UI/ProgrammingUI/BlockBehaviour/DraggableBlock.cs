using Shard.Lib.Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableBlock : MonoBehaviour
{
    private GameObject currentBlockSpace;
    private bool isInBlockSpace;

    private void OnTriggerEnter2D(Collider2D other) {
        isInBlockSpace = other.gameObject.transform.tag == "Block Space";

        if(isInBlockSpace) currentBlockSpace = other.gameObject;
    }

    public void Place() {
        //If we are not inside a block space, we just remove the block
        if (!isInBlockSpace) Destroy(this.gameObject);

        //Otherwise, we place the block in the block space
        else {
            this.transform.SetParent(currentBlockSpace.transform, false);
            this.transform.position = currentBlockSpace.transform.position; 
        }
    }
}
