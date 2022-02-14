using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Shard.UI.ProgrammingUI
{
    public class ConditionalBlock : BehaviourBlock
    {
        public enum ConditionalType {
            IF,
            ELSE_IF,
            ELSE
        }

        [SerializeField]
        private ConditionalType conditionalType;

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

        private BlockBehaviour trueSubBehaviour;
        private BlockBehaviour falseSubBehaviour;


        private void Awake() {
            type = BlockType.CONDITIONAL;

            condition = conditionalType == ConditionalType.ELSE ? null : new Condition("wall", "ahead");

            if(conditionalType != ConditionalType.ELSE) {
                elementDropDown.onValueChanged.AddListener(context => SetConditionElement(elementDropDown.value));
                stateDropDown.onValueChanged.AddListener(context => SetConditionState(stateDropDown.value));
            } 
        }


        public override BlockLocation Execute()
        {
            // If the condition is meet, we execute the true sub-behaviour, and if it's not, the false sub-behaviour
            if(condition.IsMet()) trueSubBehaviour.ExecuteBehavior();
            else                  falseSubBehaviour.ExecuteBehavior();

            // In both cases, the next block to execute is the end of the conditional (i.e. the end of the false sub-behaviour, if it exists)
            BlockLocation nextBlockLocation = falseSubBehaviour == null ? new BlockLocation(trueSubBehaviour.GetMaxIndex(), this.GetIndentation()) : new BlockLocation(falseSubBehaviour.GetMaxIndex(), this.GetIndentation());
            return nextBlockLocation;
        }

        public override string ToString() {
            string message = "CONDITIONAL -> " + conditionalType.ToString();
            
            if(condition != null)
                message += " " + condition.ToString();

            message += "\n";

            if(trueSubBehaviour != null) {
                for(int k = 0; k < location.GetIndentation() - 1; k++) message += "\t";
                message += "#TRUE\n"  + trueSubBehaviour.ToString(location.GetIndentation());

                for(int k = 0; k < location.GetIndentation() - 1; k++) message += "\t";
                message += "#END_TRUE\n";
            }  

            if(falseSubBehaviour != null) {
                for(int k = 0; k < location.GetIndentation() - 1; k++) message += "\t";
                message += "#FALSE\n"  + falseSubBehaviour.ToString(location.GetIndentation());
                
                for(int k = 0; k < location.GetIndentation() - 1; k++) message += "\t";
                message += "#END_FALSE\n";
            }

            // if(elseBlock != null)
            //     message += " (ELSE_BLOCK = [" + elseBlock.GetIndex() + ", " + elseBlock.GetIndentation() + "])";
            
            return message;
        }


        public ConditionalType GetConditionalType() {
            return this.conditionalType;
        }

        public void SetNextBlockIndex(int nextBlockIndex)
        {
            this.nextBlockIndex = nextBlockIndex;
        }

        public void SetElseBlock(ConditionalBlock elseBlock) 
        {
            this.elseBlock = elseBlock;
        }

        public ConditionalBlock GetElseBlock() {
            return this.elseBlock;
        }

        public void SetConditionElement(int element) {
            condition =  new Condition(((ConditionalElement) element).ToString(), condition.GetState());
        }

        public void SetConditionState(int state) {
            condition =  new Condition(condition.GetElement(), ((ConditionalState) state).ToString());
        }
    
        public void SetSubBehaviour(bool selector, BlockBehaviour subBehaviour) {
            if(selector) this.trueSubBehaviour = subBehaviour;
            else         this.falseSubBehaviour = subBehaviour;
        }

        public BlockBehaviour GetSubBehaviour(bool selector) {
            if (selector) return trueSubBehaviour;
                          return falseSubBehaviour;
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
            return element == "wall";
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
