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
    [SerializeField] private bool crossCameraSize;

    public void setMaid(int maxX, int maxY)
    {
        Debug.Log("setMaid x:" + maxX + " y: " + maxY);
        myCamera.orthographicSize = Math.Max(maxY / 2, maxX / 2) + 2;
        transform.position = new Vector3(maxX / 2, y: -maxY / 2, -10f);
        IsSet = true;
    }

    void Update()
    {
        float deltatime;
        deltatime = Time.fixedDeltaTime;
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
            myCamera.orthographicSize = 0.1f;
        }

        Vector3 pos;
        Vector3 move;
        if (Input.GetAxis("Vertical") < 0)
        {
            pos = Vector3.up;
            move = new Vector3(-pos.x * ArrowMoveSpeed * deltatime, -pos.y * ArrowMoveSpeed * deltatime, 0);
            MovingCamera(move);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            pos = Vector3.down;
            move = new Vector3(-pos.x * ArrowMoveSpeed * deltatime, -pos.y * ArrowMoveSpeed * deltatime, 0);
            MovingCamera(move);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            pos = Vector3.right;
            move = new Vector3(-pos.x * ArrowMoveSpeed * deltatime, -pos.y * ArrowMoveSpeed * deltatime, 0);
            MovingCamera(move);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            pos = Vector3.left;
            move = new Vector3(-pos.x * ArrowMoveSpeed * deltatime, -pos.y * ArrowMoveSpeed * deltatime, 0);
            MovingCamera(move);
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        move = new Vector3(-pos.x * dragSpeed * deltatime, -pos.y * dragSpeed * deltatime, 0);

        MovingCamera(move);
    }

    private void MovingCamera(Vector3 move)
    {
        if (crossCameraSize)
        {
            // transform.Translate(move * myCamera.orthographicSize, Space.World);
            transform.position += move;
        }
        else
        {
            transform.position += move;
        }
    }

    public void onScrool()
    {
        myCamera.orthographicSize = lastZoom;
    }
}