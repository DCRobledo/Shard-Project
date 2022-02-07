using Shard.Lib.Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shard.UI.ProgrammingUI
{
    public class BlockDragging : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField]
        private GameObject draggableBlockPrefab;

        [SerializeField]
        private float dragSpeed = .05f;

        private Vector3 velocity = Vector3.zero;

        private GameObject draggableBlock;
        private RectTransform draggableBlockRectTransform;

        private GameObject blockImage;


        private void Awake() {
            blockImage = this.transform.GetChild(0).gameObject;
        }

        public void OnDrag(PointerEventData eventData) {
            if(RectTransformUtility.ScreenPointToWorldPointInRectangle(
                draggableBlockRectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var globalMousePosition
            )) {
                draggableBlockRectTransform.position =
                Vector3.SmoothDamp(
                    draggableBlockRectTransform.position,
                    globalMousePosition,
                    ref velocity,
                    dragSpeed
                );
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            // Enter dragging state
            VisualUtils.ChangeObjectImage(ref blockImage, a:-.7f);

            // Create the draggable block
            CreateDraggableBlock();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // End dragging state
            VisualUtils.ChangeObjectImage(ref blockImage, a:.7f);

            // Place the draggable block
            draggableBlock.GetComponent<DraggableBlock>().Place();
        }

        private void CreateDraggableBlock(){
            draggableBlock = Instantiate(draggableBlockPrefab);
            draggableBlockRectTransform = draggableBlock.GetComponent<RectTransform>();

            draggableBlock.transform.SetParent(GameObject.Find("blocks").transform, false);
            draggableBlock.transform.position = this.transform.position;   
        }
    }
}


