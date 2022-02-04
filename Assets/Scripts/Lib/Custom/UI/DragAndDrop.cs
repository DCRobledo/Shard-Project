using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shard.Lib.Custom
{
    public class DragAndDrop : MonoBehaviour, IDragHandler
    {
        [SerializeField]
        private float dragSpeed = .05f;

        private Vector3 velocity = Vector3.zero;
        
        private RectTransform rectTransform;

        protected void Awake() {
            rectTransform = this.GetComponent<RectTransform>();
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
    }
}


