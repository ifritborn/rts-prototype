
using UnityEngine;


public class CameraManager : MonoBehaviour
{

    private enum Bumper
    {
        Right,
        Left,
        Up,
        Down
    }

    private void moveMapWithMouse()
    {
        Vector3 mpos = Input.mousePosition;

        int RLThresh = Screen.width / 10; //192
        float rBumper = Screen.width - RLThresh; //1728
        float lBumper = RLThresh;

        int UDThresh = Screen.height / 10;
        float topBumper = Screen.height - UDThresh;
        float botBumper = UDThresh;

        float mtime = Time.deltaTime;
        float cameraSpeed = 5f;
        
        // float mCalc = mtime * cameraSpeed * speedMult;

        // Move camera to the Right
        if ( mpos.x <= Screen.width && mpos.x >= rBumper)
        {
            Vector3 moveRight = Vector3.right;
            float sMult = speedMult(mpos, Bumper.Right, rBumper, RLThresh);
            float mCalc = mtime * cameraSpeed * sMult;
            transform.position += (moveRight * mCalc);
        }

        // Move camera to the Left
        if (mpos.x >= 0 && mpos.x <= lBumper)
        {
            Vector3 moveLeft = Vector3.left;
            float sMult = speedMult(mpos, Bumper.Left, lBumper, RLThresh);
            float mCalc = mtime * cameraSpeed * sMult;
            transform.position += (moveLeft * mCalc);
        }

        // Move camera up
        if (mpos.y <= Screen.height && mpos.y >= topBumper)
        {
            Vector3 moveUp = Vector3.up;
            float sMult = speedMult(mpos, Bumper.Up, topBumper, UDThresh);
            float mCalc = mtime * cameraSpeed * sMult;
            transform.position += (moveUp * mCalc);
        }

        // Move camera down
        if (mpos.y >= 0 && mpos.y <= botBumper)
        {
            Vector3 moveDown = Vector3.down;
            float sMult = speedMult(mpos, Bumper.Down, botBumper, UDThresh);
            float mCalc = mtime * cameraSpeed * sMult;
            transform.position += (moveDown * mCalc);
        }
    }

    private float speedMult(Vector3 mpos, Bumper b, float bumper, float thresh)
    {
        switch (b)
        {
            case Bumper.Right:
                return Mathf.Clamp(((mpos.x - bumper) / thresh), 0f, 1f);
            case Bumper.Left:
                return Mathf.Clamp(1 - (mpos.x / thresh), 0f, 1f);
            case Bumper.Up:
                return Mathf.Clamp(((mpos.y - bumper) / thresh), 0f, 1f);
            case Bumper.Down:
                return Mathf.Clamp(1 - (mpos.y / thresh), 0f, 1f);;
            default:
                return 0f;
        }
  
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        moveMapWithMouse();        
        
    }
}
