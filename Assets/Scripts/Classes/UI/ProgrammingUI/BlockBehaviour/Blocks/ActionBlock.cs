using Shard.UI.ProgrammingUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class ActionBlock : BehaviourBlock
    {
        public enum BlockAction {
            WALK,
            FLIP,
            JUMP
        }

        [SerializeField]
        private BlockAction action;


        private void Awake() {
            type = BlockType.ACTION;
        }

        public override BlockLocation Execute() {
            //Debug.Log(action.ToString());

            return new BlockLocation(location.GetIndex() + 1, -1);
        }

        public override string ToString() {
            return "ACTION            -> " + action.ToString();
        }


        public BlockAction GetAction()
        {
            return this.action;
        }

        public void SetAction(BlockAction action)
        {
            this.action = action;
        }
    }
}


