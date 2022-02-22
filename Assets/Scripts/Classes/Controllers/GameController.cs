using Shard.Patterns.Singleton;
using Shard.Enums;
using Shard.Gameflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.Controllers 
{
    public class GameController : SingletonUnity
    {
        [SerializeField]
        private bool showGameFlowTriggers = false;

        private static GameController instance = null;
        public static new GameController Instance { get { return (GameController) instance; }}

        private GameObject[] checkpoints;
        private Dictionary<EntityEnum.Entity, GameObject> checkpointRecord = new Dictionary<EntityEnum.Entity, GameObject>();

        private GameObject[] deathTriggers;

        private GameObject player;
        private GameObject robot;


        private void Awake() {
            instance = this;

            player = GameObject.Find("player");
            robot = GameObject.Find("robot");

            checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");

            checkpointRecord.Add(EntityEnum.Entity.PLAYER, checkpoints[0]);
            checkpointRecord.Add(EntityEnum.Entity.ROBOT, checkpoints[0]);

            deathTriggers = GameObject.FindGameObjectsWithTag("Death Trigger");
        }

        private void OnEnable() {
            foreach (GameObject checkpoint in checkpoints) {
                checkpoint.GetComponent<Checkpoint>().checkpointItEvent += UpdateCheckpoint;
                checkpoint.GetComponent<SpriteRenderer>().enabled = showGameFlowTriggers;
            }
                
            foreach (GameObject deathTrigger in deathTriggers) {
                deathTrigger.GetComponent<DeathTrigger>().deathTriggerEvent += ReturnToLastCheckpoint;
                deathTrigger.GetComponent<SpriteRenderer>().enabled = showGameFlowTriggers;
            }    
        }


        private void UpdateCheckpoint(string entityTag, GameObject checkpoint) {
            try
            {
                EntityEnum.Entity entity = (EntityEnum.Entity) System.Enum.Parse(typeof(EntityEnum.Entity), entityTag.ToUpper());

                checkpointRecord[entity] = checkpoint;
            }
            catch (System.Exception) {}
        }

        private void ReturnToLastCheckpoint(string entityTag) {
            try
            {
                EntityEnum.Entity entity = (EntityEnum.Entity) System.Enum.Parse(typeof(EntityEnum.Entity), entityTag.ToUpper());

                switch (entity) {
                    case EntityEnum.Entity.PLAYER: player.transform.position = GetLastCheckpoint(entity); break;
                    case EntityEnum.Entity.ROBOT:  robot.transform.position  = GetLastCheckpoint(entity); break;
                }
            }
            catch (System.Exception) {}
        }

        public Vector3 GetLastCheckpoint(EntityEnum.Entity entity) {
            return checkpointRecord[entity].transform.position;
        }
    }
}
