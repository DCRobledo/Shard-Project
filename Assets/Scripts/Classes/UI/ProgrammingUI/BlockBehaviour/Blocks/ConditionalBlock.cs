using System;
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

        [SerializeField]
        private TMP_Dropdown elementDropDown;

        [SerializeField]
        private TMP_Dropdown stateDropDown;

        private Condition condition;
    

        public int endOfConditional;
        public int nextBlockIndex;
        private ConditionalBlock elseBlock = null;


        private void Awake() {
            type = BlockType.CONDITIONAL;

            condition = conditionalType == ConditionalType.ELSE ? null : new Condition(0, 0);

            if(conditionalType != ConditionalType.ELSE) {
                elementDropDown.onValueChanged.AddListener(context => SetConditionElement(elementDropDown.value));
                stateDropDown.onValueChanged.AddListener(context => SetConditionState(stateDropDown.value));
            } 
        }


        public override BlockLocation Execute()
        {
            // If the next block index has been modified, we should follow it
            if (nextBlockIndex != 0) {
                // This propagates the information in multiple IF-ELSE-IF-ELSE combinations
                elseBlock?.SetNextBlockIndex(endOfConditional);

                return new BlockLocation(nextBlockIndex, -1);
            } 

            // If the condition is meet, we follow the usual path and modify the elseblock's nextBlockIndex
            if (condition != null) {
                if(condition.IsMet()) {
                    elseBlock?.SetNextBlockIndex(endOfConditional);

                    return new BlockLocation(GetIndex() + 1, GetIndentation() + 1);
                }
                else {
                    elseBlock?.SetNextBlockIndex(0);

                    return elseBlock != null ? new BlockLocation(elseBlock.GetIndex(), -1) : new BlockLocation(endOfConditional, -1);
                }
            }


            return new BlockLocation(this.GetIndex() + 1, -1);
        }

        public override string ToString() {
            string message = "CONDITIONAL -> " + conditionalType.ToString();
            
            if(condition != null)
                message += " " + condition.ToString();
            
            return message;
        }


        public ConditionalType GetConditionalType() {
            return this.conditionalType;
        }

        public void SetNextBlockIndex(int nextBlockIndex)
        {
            this.nextBlockIndex = nextBlockIndex;
        }

        public void SetEndOfConditional(int endOfConditional) {
            this.endOfConditional = endOfConditional;
        }

        public void SetElseBlock(ConditionalBlock elseBlock) 
        {
            this.elseBlock = elseBlock;
        }

        public ConditionalBlock GetElseBlock() {
            return this.elseBlock;
        }

        public void SetConditionElement(int element) {
            condition.SetElement(element);
        }

        public void SetConditionState(int state) {
            condition.SetState(state);
        }
    
        public Condition GetCondition() {
            return this.condition;
        }
    }

    public class Condition
    {
        public enum ConditionalElement{
            GROUND,
            VOID,
            SPIKE,
            BOX
        } private ConditionalElement element;

        public enum ConditionalState {
            AHEAD,
            BEHIND,
            BELOW,
            ABOVE
        } private ConditionalState state;

        public Func<string> isMetEvent;

        public Condition(int element, int state) {
            SetElement(element);
            SetState(state);
        }


        public bool IsMet() {
            return isMetEvent?.Invoke()?.ToLower() == element.ToString().ToLower();
        }


        public ConditionalElement GetElement()
        {
            return this.element;
        }

        public void SetElement(int element)
        {
            this.element = (ConditionalElement) element;
        }

        public ConditionalState GetState()
        {
            return this.state;
        }

        public void SetState(int state)
        {
            this.state = (ConditionalState) state;
        }

        public new string ToString() {
            return GetElement() + " " + GetState();
        }
    }
}
