using Shard.Lib.Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shard.UI.ProgrammingUI
{
    public class DraggableBlock : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;

        [SerializeField]
        private float dragSpeed = .05f;

        private Vector3 velocity = Vector3.zero;

        private GameObject currentBlockSpace;
        private bool isInBlockSpace;
        private bool isInScrollableBlockArea;

        private void Awake() {
            rectTransform = this.GetComponent<RectTransform>();
        }


        private void OnTriggerEnter2D(Collider2D other) {
            //isInScrollableBlockArea = other.gameObject.transform.tag == "Scrollable Block Area";
            isInBlockSpace = other.gameObject.transform.tag == "Block Space";

            if(isInBlockSpace) {
                // Restore previous block space's color
                if(currentBlockSpace != null)
                    HighlightBlockSpace(false);
                    
                // Highlight new block space
                currentBlockSpace = other.gameObject;
                HighlightBlockSpace(true);
            }
        }


        public void Place() {
            //If we are not inside a block space, we just remove the block
            if (!isInBlockSpace) {
                if(currentBlockSpace != null)
                    HighlightBlockSpace(false);

                Destroy(this.gameObject);
            } 

            //Otherwise, we place the block in the block space
            else {
                GameObject.Find("blocks").GetComponent<BlockManagement>().
                PlaceBlock(
                    currentBlockSpace.GetComponent<BlockSpace>().index,
                    this.gameObject
                );

                HighlightBlockSpace(false);
            }  
        }

        private void HighlightBlockSpace(bool highlight) {
            float alphaModifier = highlight ? .07f : -.07f;

            GameObject blockSpaceImage = currentBlockSpace.transform.GetChild(0).gameObject;
            VisualUtils.ChangeObjectImage(ref blockSpaceImage, a: alphaModifier);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            GameObject.Find("blocks").GetComponent<BlockManagement>().
            RemoveBlock(
                currentBlockSpace.GetComponent<BlockSpace>().index
            );

            this.transform.SetParent(GameObject.Find("block_behaviour").transform, false);
            this.transform.position = currentBlockSpace.transform.position;

            HighlightBlockSpace(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(RectTransformUtility.ScreenPointToWorldPointInRectangle(
                rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var globalMousePosition
            )) {
                rectTransform.position =
                Vector3.SmoothDamp(
                    rectTransform.position,
                    globalMousePosition,
                    ref velocity,
                    dragSpeed
                );
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Place();
        }
    }
}


