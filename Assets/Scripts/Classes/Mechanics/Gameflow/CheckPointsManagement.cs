using Shard.Patterns.Singleton;
using Shard.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Gameflow 
{
    public class CheckPointsManagement : MonoBehaviour
    {
        [SerializeField]
        private bool showVisuals = false;

        [SerializeField]
        private float respawnDelay = 1f;

        private GameObject[] checkpoints;
        private Dictionary<EntityEnum.Entity, Vector3> checkpointRecord = new Dictionary<EntityEnum.Entity, Vector3>();

        private GameObject[] deathTriggers;

        private GameObject player;
        private GameObject robot;

        public static Action playerDeathEvent;
        public static Action robotDeathEvent;  


        private void Awake() {
            player = GameObject.Find("player");
            robot = GameObject.Find("robot");

            checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
            deathTriggers = GameObject.FindGameObjectsWithTag("Death Trigger");
        }

        private void OnEnable() {
            foreach (GameObject checkpoint in checkpoints) {
                checkpoint.GetComponent<Checkpoint>().checkpointItEvent += UpdateCheckpoint;
                checkpoint.GetComponent<SpriteRenderer>().enabled = showVisuals;
            }
                
            foreach (GameObject deathTrigger in deathTriggers) {
                deathTrigger.GetComponent<DeathTrigger>().deathTriggerEvent += ReturnToLastCheckpoint;
                deathTrigger.GetComponent<SpriteRenderer>().enabled = showVisuals;
            }    
        }


        private void UpdateCheckpoint(string entityTag, Vector3 checkpoint) {
            try
            {
                EntityEnum.Entity entity = (EntityEnum.Entity) System.Enum.Parse(typeof(EntityEnum.Entity), entityTag.ToUpper());

                checkpointRecord[entity] = checkpoint;
            }
            catch (System.Exception) {}
        }

        private void ReturnToLastCheckpoint(string entityTag) {
            try {
                EntityEnum.Entity entity = (EntityEnum.Entity) System.Enum.Parse(typeof(EntityEnum.Entity), entityTag.ToUpper());

                switch (entity) {
                    case EntityEnum.Entity.PLAYER: playerDeathEvent?.Invoke(); StartCoroutine(ReturnToLastCheckPoint(player, GetLastCheckpoint(entity))); break;
                    case EntityEnum.Entity.ROBOT:  robotDeathEvent?.Invoke();  StartCoroutine(ReturnToLastCheckPoint(robot, GetLastCheckpoint(entity)));  break;
                }
                
            }
            catch (System.Exception) {}
        }

        private IEnumerator ReturnToLastCheckPoint(GameObject entity, Vector3 lastCheckpoint) {
            yield return new WaitForSeconds(respawnDelay);

            entity.transform.position = lastCheckpoint;
        }

        public Vector3 GetLastCheckpoint(EntityEnum.Entity entity) {
            return checkpointRecord[entity];
        }
    }
}
