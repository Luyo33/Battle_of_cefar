using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerComponent : MonoBehaviourPun
{

    public Camera cam;
    public PlayerMotor motor;
    public Select select;
    public bool selected;

    private void OnMouseOver()
    {
        selected = true;
    }

    private void OnMouseExit()
    {
        selected = false;
    }


    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        select = GetComponent<Select>();
    }
    // Update is called once per frame
    
    void Update()
    {
        SelectThis();
        if (select.selected)
        {
            if (Input.GetMouseButtonUp(1))
            {

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject ennemy = hit.collider.gameObject;
                    if (ennemy.GetComponent<UnitMan>() == null)
                    {
                        Vector2Int pos = gameObject.GetComponent<UnitMan>().battle.GetComponent<Manager>()
                            .Vselected();
                        
                        if (gameObject.GetComponent<UnitMov>().Neighbours.Contains(pos))
                        {
                            Debug.Log("Possible Movement");
                            gameObject.GetComponent<UnitMov>().position = pos;
                            Debug.Log("You moved to : " + gameObject.GetComponent<UnitMov>().position);
                            motor.MoveToPoint(hit.point);
                            Debug.Log(gameObject.GetComponent<UnitMan>()
                                .battle.GetComponent<Manager>()
                                .GetCellFromXZ(gameObject.GetComponent<UnitMov>().position));
                        }
                        
                        else
                        {
                            Debug.Log("illegal movement");
                        }
                        
                    }
                    else
                    {
                        UnitAtk atk = gameObject.GetComponent<UnitAtk>();
                        foreach (GameObject target in atk.GetTargets())
                        {
                            if (ennemy == target)
                            {
                                atk.attack(ennemy);
                                break;
                            }
                        }
                    }
                    gameObject.GetComponent<UnitMan>().statUpdate();
                    select.selected = false;
                }
            }
        }
    }

    public void SelectThis()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Select Unit = hit.collider.GetComponent<Select>();
                if (Unit)
                {
                    Unit.SelectMe();
                    //selected = true;
                    Unit.selected = true;
                }
            }
        }
    }
}
