using Shard.Lib.Custom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlockDragging : DragAndDrop, IBeginDragHandler, IEndDragHandler
{
    private GameObject block;

    private new void Awake() {
        base.Awake();
        block = this.transform.GetChild(0).gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        VisualUtils.ChangeImageAlpha(ref block, -.7f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        VisualUtils.ChangeImageAlpha(ref block, .7f);
    }
}
