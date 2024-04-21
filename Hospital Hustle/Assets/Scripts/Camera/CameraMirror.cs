using UnityEngine;

public class CameraMirror : MonoBehaviour
{
    void Start()
    {
        if (DoorManager.enteredFromRightDoor)
        {
            GetComponent<Camera>().projectionMatrix = GetComponent<Camera>().projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
            CameraState.IsFlipped = true;
        }
        else
        {
            CameraState.IsFlipped = false;
        }
    }
}
