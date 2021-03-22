// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using System;
using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    public float zoomSpeed = 10.0f;
    [SerializeField] private Camera myCamera;

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
  


        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            myCamera.orthographicSize += zoomSpeed;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            myCamera.orthographicSize -= zoomSpeed;
        }

        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(2)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);

        transform.Translate(move, Space.World);
    }
}