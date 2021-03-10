// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using System;
using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float zoomSpeed = 10.0f;
    public int boundary = 50;
    public float minSize;
    public float maxSize;
    private int width;
    private int height;
    [SerializeField]
    private Camera myCamera;

    void Awake()
    {
        width = Screen.width;
        height = Screen.height;
    }

    public void setMaid(int maxX, int maxY)
    {
        Debug.Log("setMaid x:"+ maxX + " y: "+ maxY);
        myCamera.orthographicSize = Math.Max(maxY/2, maxX/2) + 2;
        transform.position = new Vector3(maxX/2,maxY/2,-10f);
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

        myCamera.orthographicSize =
        Mathf.Clamp(myCamera.orthographicSize, minSize,maxSize);

        if (Input.mousePosition.x > width - boundary)
        {
            transform.Translate(new Vector3(+Time.deltaTime * moveSpeed, 0.0f));
        }

        if (Input.mousePosition.x < 0 + boundary)
        {
            transform.Translate(new Vector3(-Time.deltaTime * moveSpeed, 0.0f));
        }

        if (Input.mousePosition.y > height - boundary)
        {
            transform.Translate(new Vector3(0.0f, +Time.deltaTime * moveSpeed));
        }

        if (Input.mousePosition.y < 0 + boundary)
        {
            transform.Translate(new Vector3(0.0f, -Time.deltaTime * moveSpeed));
        }
    }
}