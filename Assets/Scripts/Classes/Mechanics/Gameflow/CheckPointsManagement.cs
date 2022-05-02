using Shard.Patterns.Singleton;
using Shard.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Shard.Gameflow 
{
    public class CheckPointsManagement : MonoBehaviour
    {
        [SerializeField]
        private bool showVisuals = false;

        [SerializeField]
        private float respawnDelay = 1f;

        [SerializeField]
        private GameObject checkpoints;

        [SerializeField]
        private GameObject deathTriggers;

        private Dictionary<EntityEnum.Entity, Vector3> checkpointRecord = new Dictionary<EntityEnum.Entity, Vector3>();

        private GameObject player;
        private GameObject robot;

        public static Action playerDeathEvent;
        public static Action robotDeathEvent;  

        public static Action<EntityEnum.Entity> returnToLastCheckpointEvent;


        private void Start() {
            player = GameObject.Find("player");
            robot = GameObject.Find("robot");

            checkpoints.GetComponent<TilemapRenderer>().enabled = showVisuals;
            deathTriggers.GetComponent<TilemapRenderer>().enabled = showVisuals;

            checkpointRecord.Add(EntityEnum.Entity.PLAYER, player.transform.position);
            checkpointRecord.Add(EntityEnum.Entity.ROBOT, robot.transform.position);
        }

        private void OnEnable() {
            Checkpoint.checkpointEvent += UpdateCheckpoint;
            DeathTrigger.deathTriggerEvent += ReturnToLastCheckpoint;

            returnToLastCheckpointEvent += ReturnToLastCheckpoint; 
        }

        private void OnDisable() {
            Checkpoint.checkpointEvent -= UpdateCheckpoint;
            DeathTrigger.deathTriggerEvent -= ReturnToLastCheckpoint;

            returnToLastCheckpointEvent -= ReturnToLastCheckpoint; 
        }


        private void UpdateCheckpoint(string entityTag, Vector3 checkpoint) {
            // We record pairs (ENTITY, CHECKPOINT)
            try
            {
                EntityEnum.Entity entity = (EntityEnum.Entity) System.Enum.Parse(typeof(EntityEnum.Entity), entityTag.ToUpper());

                checkpointRecord[entity] = checkpoint;
            }
            catch (System.Exception) {}
        }

        private void ReturnToLastCheckpoint(EntityEnum.Entity entity) { ReturnToLastCheckpoint(entity.ToString()); }

        private void ReturnToLastCheckpoint(string entityTag) {
            try {
                EntityEnum.Entity entity = (EntityEnum.Entity) System.Enum.Parse(typeof(EntityEnum.Entity), entityTag.ToUpper());
                
                switch (entity) {
                    case EntityEnum.Entity.PLAYER:
                        StartCoroutine(ReturnToLastCheckpointCoroutine(player, GetLastCheckpoint(entity)));
                        playerDeathEvent?.Invoke();
                    break;

                    case EntityEnum.Entity.ROBOT:
                        StartCoroutine(ReturnToLastCheckpointCoroutine(robot, GetLastCheckpoint(entity)));
                        robotDeathEvent?.Invoke();
                    break;
                }
                
            }
            catch (System.Exception) {}
        }

        private IEnumerator ReturnToLastCheckpointCoroutine(GameObject entity, Vector3 lastCheckpoint) {
            yield return new WaitForSeconds(respawnDelay);

            entity.transform.position = lastCheckpoint;
        }

        public Vector3 GetLastCheckpoint(EntityEnum.Entity entity) {
            return checkpointRecord[entity];
        }
    }
}
