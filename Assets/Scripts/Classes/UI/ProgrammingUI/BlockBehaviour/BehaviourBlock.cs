using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public class BehaviourBlock : MonoBehaviour
    {
        public enum BlockType {
            IF,
            ELSE,
            WALK,
            JUMP,
            FLIP,
            BRANCH
        } public BlockType blockType;
    }
}
