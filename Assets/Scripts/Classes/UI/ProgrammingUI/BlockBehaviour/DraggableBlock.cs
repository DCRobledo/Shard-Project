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
        public bool isInBlockSpace;

        private void Awake() {
            rectTransform = this.GetComponent<RectTransform>();
        }


        private void OnTriggerEnter2D(Collider2D other) {
            isInBlockSpace = other.gameObject.transform.tag == "Block Space";

            if(isInBlockSpace) {
                // Restore previous block space's color
                if(currentBlockSpace != null)
                    VisualUtils.ChangeObjectImage(ref currentBlockSpace, a: -.07f);

                // Highlight new block space
                currentBlockSpace = other.gameObject;
                VisualUtils.ChangeObjectImage(ref currentBlockSpace, a: .07f);
            }
        }

        public void Place() {
            //If we are not inside a block space, we just remove the block
            if (!isInBlockSpace) Destroy(this.gameObject);

            //Otherwise, we place the block in the block space
            else {
                GameObject.Find("blocks").GetComponent<BlockManagement>().
                PlaceBlock(
                    currentBlockSpace.GetComponent<BlockSpace>().index,
                    this.gameObject
                );

                VisualUtils.ChangeObjectImage(ref currentBlockSpace, a: -.05f);
            }  
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            GameObject.Find("blocks").GetComponent<BlockManagement>().
            RemoveBlock(
                currentBlockSpace.GetComponent<BlockSpace>().index
            );
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


