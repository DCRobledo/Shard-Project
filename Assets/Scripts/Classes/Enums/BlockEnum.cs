namespace Shard.Enums
{
    public static class BlockEnum
    {
        public enum BlockType {
            ACTION,
            CONDITIONAL,
            BRANCH
        }

        public enum ConditionalType {
            IF,
            ELSE_IF,
            ELSE
        } 

        public enum ConditionalElement{
            GROUND,
            VOID,
            SPIKE,
            BOX
        }

        public enum ConditionalState {
            AHEAD,
            BEHIND,
            BELOW,
            ABOVE
        }
    }
}
