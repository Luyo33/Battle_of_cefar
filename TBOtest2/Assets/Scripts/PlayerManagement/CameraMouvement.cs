using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraMouvement : MonoBehaviour //CAMERA MOUVEMENT!!!!!!!!!!!!!!!!!!!!!!!!!
{
    public float dist = 40;
    public float height = 40;
    public GameObject target;
    public float speed = 1;
    public GameObject lookOut;
    private Vector3 pos;
    private float aY;
    private float zoom = 0f;

    public bool shouldMove = false;
    public bool shouldZoomIn = true;
    public bool shouldZoomOut = true;
    private Vector3 delta;


    private IEnumerator Start()
    {
        yield return new WaitWhile(()=> gameObject.scene.GetRootGameObjects().Length > 5);
        target = gameObject.scene.GetRootGameObjects()[5];
        var data = target.GetComponent<Manager>();
        lookOut = Instantiate(lookOut);
        delta = new Vector3(data.sizeX / 2, 0, -data.sizeZ / 2);
        lookOut.transform.position = delta;
        pos = new Vector3(delta.x + dist, delta.y + height, delta.z);
        transform.Translate(pos);
        transform.RotateAround(lookOut.transform.position, Vector3.up, 10);
        transform.LookAt(lookOut.transform);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shouldMove = true;
        } else if (Input.GetMouseButtonUp(0))
        {
            shouldMove = false;
        }
        if (shouldMove)
        {
            transform.RotateAround(lookOut.transform.position, Vector3.up, speed * Input.GetAxis("Mouse X"));
        }
        transform.LookAt(lookOut.transform);
        float limit = Camera.main.transform.position.y;
        if (limit < 20f)
        {
            shouldZoomIn = false;
        }
        else
        {
            shouldZoomIn = true;
        }

        if (limit > 80f)
        {
            shouldZoomOut = false;
        }
        else
        {
            shouldZoomOut = true;
        }
        HandleZoom();
    }

    public void HandleZoom()
    {
        float zoomChangeAmount = 8f;
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                if (shouldZoomIn)
                {
                    zoom += zoomChangeAmount;
                    transform.Translate(0, 0, zoom);
                    zoom = 0f;
                }
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                if (shouldZoomOut)
                {
                    zoom -= zoomChangeAmount;
                    transform.Translate(0, 0, zoom);
                    zoom = 0f;
                }
            }
        }
        
    }

}