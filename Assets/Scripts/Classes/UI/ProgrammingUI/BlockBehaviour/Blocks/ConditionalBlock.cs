using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Shard.UI.ProgrammingUI
{
    public class ConditionalBlock : BehaviourBlock
    {
        public enum ContionalType {
            IF,
            ELSE_IF,
            ELSE
        }

        [SerializeField]
        private ContionalType conditionalType;

        private enum ConditionalElement{
            WALL,
            VOID,
            SPIKE,
            BOX
        }

        private enum ConditionalState{
            AHEAD,
            BEHIND,
            BELOW,
            ABOVE
        }

        [SerializeField]
        private TMP_Dropdown elementDropDown;

        [SerializeField]
        private TMP_Dropdown stateDropDown;

        private Condition condition;

        private int nextBlockIndex;

        private ConditionalBlock elseBlock = null;


        private void Awake() {
            type = BlockType.CONDITIONAL;

            condition = new Condition("wall", "ahead");

            if(conditionalType != ContionalType.ELSE) {
                elementDropDown.onValueChanged.AddListener(context => SetConditionElement(elementDropDown.value));
                stateDropDown.onValueChanged.AddListener(context => SetConditionState(stateDropDown.value));
            } 
        }


        public override BlockLocation GetNextBlockLocation()
        {
            switch(conditionalType) {
                case ContionalType.IF: case ContionalType.ELSE_IF:

                        // Condition is met
                        if(condition.IsMet(/* ref Robot */)) 
                            return new BlockLocation(location.GetIndex() + 1, -1);                    // Next block

                        // Condition is not met and there is an ELSE block
                        else if (elseBlock != null)
                            return new BlockLocation(elseBlock.location.GetIndex() + 1, -1);          // Next block to the ELSE block

                        // Condition is not met and there is not an ELSE block
                        return new BlockLocation(location.GetIndex() + 1, location.GetIndentation()); // Next block in the same indentation level

                default:
                        return new BlockLocation(elseBlock.location.GetIndex() + 1, -1); 
            }
            
        }

        public override string ToString() {
            string message = "CONDITIONAL -> " + conditionalType.ToString();
            
            if(condition != null)
                message += " " + condition.ToString();
            
            return message;
        }


        public void SetNextBlockIndex(int nextBlockIndex)
        {
            this.nextBlockIndex = nextBlockIndex;
        }

        public void SetElseBlock(GameObject elseBlock) 
        {
            this.elseBlock = elseBlock.GetComponent<ConditionalBlock>();
        }
    
        public void SetConditionElement(int element) {
            condition =  new Condition(((ConditionalElement) element).ToString(), condition.GetState());
        }

        public void SetConditionState(int state) {
            condition =  new Condition(condition.GetElement(), ((ConditionalState) state).ToString());
        }
    }

    public class Condition
    {
        private string element;
        private string state;

        public Condition(string element, string state) {
            this.element = element.ToLower();
            this.state = state.ToLower();
        }


        public bool IsMet(/*ref RobotController robot*/) {
            // TODO
            return true;
        }


        public string GetElement()
        {
            return this.element;
        }

        public void SetElement(string element)
        {
            this.element = element;
        }

        public string GetState()
        {
            return this.state;
        }

        public void SetState(string state)
        {
            this.state = state;
        }

        public new string ToString() {
            return GetElement() + " " + GetState();
        }
    }
}
