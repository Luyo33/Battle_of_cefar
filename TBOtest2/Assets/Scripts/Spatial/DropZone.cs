using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    //public Draggable.Tile typeOfTile = Draggable.Tile.MAP;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if (d != null)
        {
            //if (typeOfTile == d.typeOfTile )|| typeOfTile == Draggable.Tile.MAP)
            {
                d.parentToReturnTo = this.transform;
            }
        }
    }
}
