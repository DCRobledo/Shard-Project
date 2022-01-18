using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Monobehaviour.Entities

{
    public class EntityActions : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectToReCall;

        public void ReCall()
        {
            Debug.Log(objectToReCall.transform.tag);
        }

        public void ChangeObjectToReCall(GameObject objectToReCall) { this.objectToReCall = objectToReCall; }
    }

}

