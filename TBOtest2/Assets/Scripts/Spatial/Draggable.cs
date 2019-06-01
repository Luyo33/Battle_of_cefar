﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentToReturnTo;
    public GameObject battlefield;
    public GameObject hand;
    public CardControl invoc;
    public CardTemplate Card;
    public Card_R1 R1;
    public Card_R2 R2;
    public Card_R3 R3;
    public bool used;
    
    //public enum Tile{ PLAIN, MOUNTAIN, LAKE, DESERT, FOREST, MAP}

    private void Start()
    {
        used = false;
        parentToReturnTo = null;
        invoc = hand.GetComponent<CardControl>();
        if (Card)
        {
            if (Card is Card_R1)
            {
                R1 = Card as Card_R1;
            }

            if (Card is Card_R2)
            {
                R2 = Card as Card_R2;
            }

            if (Card is Card_R3)
            {
                R3 = Card as Card_R3;
            }
        }
        
    }

    public void setR()
    {
        Start();
    }

    //public Tile typeOfTile = Tile.PLAIN;
    public void OnBeginDrag(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            var transform1 = this.transform;
            var parent = transform1.parent;
            parentToReturnTo = parent;
            this.transform.SetParent(parent.parent);

            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().alpha = 0f;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.button.ToString());
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            
            R1 = GetComponent<CardDisplay>().CardR1;
            R2 = GetComponent<CardDisplay>().CardR2;
            R3 = GetComponent<CardDisplay>().CardR3;
            if (R1 != null)
            {
                Vector2Int pos = battlefield.GetComponent<Manager>().Vselected();
                if (battlefield.GetComponent<Manager>().GetUnitFromXZ(pos) == null)
                {
                    if (invoc.herob == false /*&& 
                        (pos[0] == 0 || pos[1] == 0 ||
                        pos[0] == battlefield.GetComponent<Manager>().sizeX - 1 ||
                        pos[1] == battlefield.GetComponent<Manager>().sizeZ - 1)*/ ||
                        invoc.hero.GetComponent<UnitMov>().Neighbours.Contains(pos) ||
                        invoc.hero.GetComponent<UnitAtk>().w.Contains(pos))
                    {
                        GameObject u = invoc.CreateUnit1(R1, pos);
                        u.GetComponent<UnitMan>().statUpdate();
                        used = true;
                    }
                    else
                    {
                        used = false;
                    }
                }
            }

            if (R2 != null)
            {
                foreach (GameObject unit in battlefield.GetComponent<Manager>().Units)
                {
                    PlayerComponent p = unit.GetComponent<PlayerComponent>();
                    if (p != null)
                    {
                        if (p.selected)
                        {
                            used = invoc.R1R2(R2, unit);
                            break;
                        }
                    }
                }
            }
            if (R3 != null)
            {
                foreach (GameObject unit in battlefield.GetComponent<Manager>().Units)
                {
                    PlayerComponent p = unit.GetComponent<PlayerComponent>();
                    if (p != null)
                    {
                        if (p.selected)
                        {
                            used = invoc.R2R3(R3, unit);
                            break;
                        }
                    }
                }
            }
            Debug.Log(used);

            if (used)
            {
                Destroy(gameObject);
            }
            else
            {
                this.transform.SetParent(parentToReturnTo);
                GetComponent<CanvasGroup>().blocksRaycasts = true;
                GetComponent<CanvasGroup>().alpha = 1f; 
            }
            
        }
    }
}

