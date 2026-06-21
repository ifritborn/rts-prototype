
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour
{


    [SerializeField] private Tilemap tilemap;

    [SerializeField] private int mapPadding;

    [SerializeField] private int panThresholdPercent;

    [SerializeField] private float cameraSpeedMouse;

    [SerializeField] private float cameraSpeedKeyBoard;
    [SerializeField] private float dragSensitivity;
    [SerializeField] private float zoomSensitivity;


    private enum Bumper
    {
        Right,
        Left,
        Up,
        Down
    }

    // establishes left and right bumper for camera movement
    private int RLThresh;
    private float rBumper;
    private float lBumper;

    // establishes up and down bumper for camera movement
    private int UDThresh;
    private float topBumper;
    private float botBumper;

    // establishes how fast the camera moves
    private float mtime;

    // establish map bounds
    private Bounds bounds;

    // Get camera dimentions and position
    private Camera cam;
    private float aspect;
    private float camHalfWidth;
    private float camHalfHeight;

    // Set min/max for x y camera movement
    private float minX;
    private float maxX;

    private float minY;
    private float maxY;

    private float zoomMax = 12.5f;
    private float zoomMin = 1;

    private Vector3 mouseLastPos;

    // ----------------------------------------------------------------------------------------------------------------


    void Start()
    {
        // establishes left and right bumper for camera movement
        RLThresh = Screen.width / panThresholdPercent;
        rBumper = Screen.width - RLThresh;
        lBumper = RLThresh;

        // establishes up and down bumper for camera movement
        UDThresh = Screen.height / panThresholdPercent;
        topBumper = Screen.height - UDThresh;
        botBumper = UDThresh;


        // Establish map bounds
        bounds = tilemap.localBounds;

        // Get camera dimentions and position
        cam = Camera.main;
        aspect = cam.aspect;
        camHalfWidth = cam.orthographicSize * aspect;
        camHalfHeight = cam.orthographicSize;

        // Set min/max for x y camera movement
        minX = bounds.min.x + camHalfWidth - mapPadding;
        maxX = bounds.max.x - camHalfWidth + mapPadding;
        minY = bounds.min.y + camHalfHeight - mapPadding;
        maxY = bounds.max.y - camHalfHeight + mapPadding;
    }

    // Update is called once per frame
    void Update()
    {
        // establishes how fast the camera moves
        mtime = Time.deltaTime;

        moveMapWithMouse();
        moveMapWithKeyboard();

    }


    // ----------------------------------------------------------------------------------------------------------------


    private void moveMapWithKeyboard()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Vector3 moveDelta = Vector3.right * cameraSpeedKeyBoard * mtime;
            Vector3 newPos = transform.position + moveDelta;
            float rBoundry = Mathf.Clamp(newPos.x, minX, maxX);
            transform.position = new Vector3(rBoundry, newPos.y, newPos.z);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 moveDelta = Vector3.left * cameraSpeedKeyBoard * mtime;
            Vector3 newPos = transform.position + moveDelta;
            float rBoundry = Mathf.Clamp(newPos.x, minX, maxX);
            transform.position = new Vector3(rBoundry, newPos.y, newPos.z);
        }

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 moveDelta = Vector3.up * cameraSpeedKeyBoard * mtime;
            Vector3 newPos = transform.position + moveDelta;
            float dBoundary = Mathf.Clamp(newPos.y, minY, maxY);
            transform.position = new Vector3(newPos.x, dBoundary, newPos.z);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 moveDelta = Vector3.down * cameraSpeedKeyBoard * mtime;
            Vector3 newPos = transform.position + moveDelta;
            float dBoundary = Mathf.Clamp(newPos.y, minY, maxY);
            transform.position = new Vector3(newPos.x, dBoundary, newPos.z);
        }
    }



    private void moveMapWithMouse()
    {
        Vector3 mpos = Input.mousePosition;
        Vector3 camPos = cam.transform.position;

        float currentZoom = cam.orthographicSize;
        float scrollDelta = Input.mouseScrollDelta.y * mtime * zoomSensitivity;

        // Mouse zoom controls using mouse wheel
        if (scrollDelta != 0)
        {
            //TODO: map bounds need to be adjusted on zoom otherwise when zoomed in cant scroll all the way to edge of map
            float newZoom = currentZoom + (scrollDelta * -1);
            float zoomBoundary = Mathf.Clamp(newZoom, zoomMin, zoomMax);
            cam.orthographicSize = zoomBoundary;
        }

        if (Input.GetMouseButtonDown(2))
        {
            mouseLastPos = Input.mousePosition;

        }

        if (Input.GetMouseButton(2))
        {
            Vector3 mouseCurrentPos = Input.mousePosition;
            Vector3 mouseDelta = mouseCurrentPos - mouseLastPos;

            Vector3 newPos = transform.position + (mouseDelta / dragSensitivity) * -1;

            float clampX = Mathf.Clamp(newPos.x, minX, maxX);
            float clampY = Mathf.Clamp(newPos.y, minY, maxY);
            transform.position = new Vector3(clampX, clampY, newPos.z);
            mouseLastPos = mouseCurrentPos;
        }

        if (!Input.GetMouseButton(2))
        {
            // At right edge move camera to the Right
            if (mpos.x <= Screen.width && mpos.x >= rBumper)
            {
                float mvSpdCalc = speedMult(mpos, Bumper.Right, rBumper, RLThresh);
                Vector3 moveDelta = Vector3.right * mvSpdCalc;
                Vector3 newPos = transform.position + moveDelta;
                float rBoundry = Mathf.Clamp(newPos.x, minX, maxX);
                transform.position = new Vector3(rBoundry, newPos.y, newPos.z);
            }

            // At left edge move camera to the Left
            if (mpos.x >= 0 && mpos.x <= lBumper)
            {
                float mvSpdCalc = speedMult(mpos, Bumper.Left, lBumper, RLThresh);
                Vector3 moveDelta = Vector3.left * mvSpdCalc;
                Vector3 newPos = transform.position + moveDelta;
                float lBoundry = Mathf.Clamp(newPos.x, minX, maxX);
                transform.position = new Vector3(lBoundry, newPos.y, newPos.z);
            }

            // At top edge move camera to the Left move camera up
            if (mpos.y <= Screen.height && mpos.y >= topBumper)
            {
                float mvSpdCalc = speedMult(mpos, Bumper.Up, topBumper, UDThresh);
                Vector3 moveDelta = Vector3.up * mvSpdCalc;
                Vector3 newPos = transform.position + moveDelta;
                float uBoundary = Mathf.Clamp(newPos.y, minY, maxY);
                transform.position = new Vector3(newPos.x, uBoundary, newPos.z);
            }

            // At bottom edge move camera to the Left move camera down
            if (mpos.y >= 0 && mpos.y <= botBumper)
            {
                float mvSpdCalc = speedMult(mpos, Bumper.Down, botBumper, UDThresh);
                Vector3 moveDelta = Vector3.down * mvSpdCalc;
                Vector3 newPos = transform.position + moveDelta;
                float dBoundary = Mathf.Clamp(newPos.y, minY, maxY);
                transform.position = new Vector3(newPos.x, dBoundary, newPos.z);
            }
        }
    }

    // this is the linear gradient that makes the camera go faster or slower while in the bumper zone
    private float speedMult(Vector3 mpos, Bumper b, float bumper, float thresh)
    {
        float ratio = 0f;

        switch (b)
        {
            case Bumper.Right:
                ratio = (mpos.x - bumper) / thresh;
                break;
            case Bumper.Left:
                ratio = 1 - (mpos.x / thresh);
                break;
            case Bumper.Up:
                ratio = (mpos.y - bumper) / thresh;
                break;
            case Bumper.Down:
                ratio = 1 - (mpos.y / thresh);
                break;
        }

        float spdMulti = Mathf.Clamp(ratio, 0f, 1f);
        float mvSpdCalc = mtime * cameraSpeedMouse * spdMulti;
        return mvSpdCalc;
    }

}

