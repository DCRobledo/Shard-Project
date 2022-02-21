using Shard.Enums;
using System;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class ActionBlock : BehaviourBlock
    {
        [SerializeField]
        private EntityEnum.Action action;

        public Action executeActionEvent;


        private void Awake() {
            type = BlockEnum.BlockType.ACTION;
        }

        public override BlockLocation Execute() {
            executeActionEvent?.Invoke();

            return new BlockLocation(location.GetIndex() + 1, -1);
        }

        public override string ToString() {
            return "ACTION            -> " + action.ToString();
        }


        public EntityEnum.Action GetAction()
        {
            return this.action;
        }

        public void SetAction(EntityEnum.Action action)
        {
            this.action = action;
        }
    }
}


