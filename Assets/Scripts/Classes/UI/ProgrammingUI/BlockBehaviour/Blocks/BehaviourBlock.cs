using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shard.UI.ProgrammingUI
{
    public abstract class BehaviourBlock : MonoBehaviour
    {
        public enum BlockType {
            ACTION,
            CONDITIONAL,
            BRANCH
        } 
        
        [SerializeField]
        protected BlockType type;

        protected BlockLocation location;


        public abstract BlockLocation GetNextBlockLocation();

        public abstract new string ToString();
        

        public new BlockType GetType()
        {
            return this.type;
        }

        public void SetType(BlockType type)
        {
            this.type = type;
        }
        
        public BlockLocation GetBlockLocation()
        {
            return this.location;
        }

        public void SetBlockLocation(int index, int indentation)
        {
            this.location = new BlockLocation(index, indentation);
        }
    
        public int GetIndex() {
            return this.location.GetIndex();
        }

        public int GetIndentation() {
            return this.location.GetIndentation();
        }

        public bool Equals(BehaviourBlock block) {
            return GetIndex() == block.GetIndex() && GetIndentation() == block.GetIndentation();
        }
    }

    public class BlockLocation
    {
        private int index;
        private int indentation;

        public BlockLocation(int index, int indentation) {
            this.index = index;
            this.indentation = indentation;
        }

        public int GetIndex()
        {
            return this.index;
        }

        public void SetIndex(int index)
        {
            this.index = index;
        }

        public int GetIndentation()
        {
            return this.indentation;
        }

        public void SetIndentation(int indentation)
        {
            this.indentation = indentation;
        }

    }
}
