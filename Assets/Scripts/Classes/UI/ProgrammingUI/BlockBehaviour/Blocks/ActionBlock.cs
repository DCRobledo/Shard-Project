using System;
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

        public Action executeActionEvent;


        private void Awake() {
            type = BlockType.ACTION;
        }

        public override BlockLocation Execute() {
            executeActionEvent?.Invoke();

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


