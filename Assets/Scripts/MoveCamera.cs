// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float ArrowMoveSpeed;
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    public float zoomSpeed = 10.0f;
    [SerializeField] private Camera myCamera;
    private float lastZoom;
    private bool IsSet = false;

    public void setMaid(int maxX, int maxY)
    {
        Debug.Log("setMaid x:" + maxX + " y: " + maxY);
        myCamera.orthographicSize = Math.Max(maxY / 2, maxX / 2) + 2;
        transform.position = new Vector3(maxX / 2, y: -maxY / 2, -10f);
        IsSet = true;
    }

    void Update()
    {
        lastZoom = myCamera.orthographicSize;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            myCamera.orthographicSize += zoomSpeed;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            myCamera.orthographicSize -= zoomSpeed;
        }

        if (myCamera.orthographicSize < 0)
        {
            myCamera.orthographicSize = 0;
        }

        Vector3 pos;
        Vector3 move;
        if (Input.GetAxis("Vertical") < 0)
        {
            pos = Vector3.up;
            move = new Vector3(-pos.x * ArrowMoveSpeed * Time.deltaTime, -pos.y * ArrowMoveSpeed * Time.deltaTime, 0);
            transform.Translate(move, Space.World);
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            pos = Vector3.down;
            move = new Vector3(-pos.x * ArrowMoveSpeed * Time.deltaTime, -pos.y * ArrowMoveSpeed * Time.deltaTime, 0);
            transform.Translate(move, Space.World);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            pos = Vector3.right;
            move = new Vector3(-pos.x * ArrowMoveSpeed * Time.deltaTime, -pos.y * ArrowMoveSpeed * Time.deltaTime, 0);
            transform.Translate(move, Space.World);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            pos = Vector3.left;
            move = new Vector3(-pos.x * ArrowMoveSpeed * Time.deltaTime, -pos.y * ArrowMoveSpeed * Time.deltaTime, 0);
            transform.Translate(move, Space.World);
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        move = new Vector3(-pos.x * dragSpeed * Time.deltaTime, -pos.y * dragSpeed * Time.deltaTime, 0);

        transform.Translate(move, Space.World);
    }

    public void onScrool()
    {
        myCamera.orthographicSize = lastZoom;
    }
}