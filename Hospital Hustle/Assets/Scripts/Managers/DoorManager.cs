using UnityEngine;

public static class DoorManager
{
    public static bool enteredFromRightDoor = false;
    public static Vector2 lastDoorEnteredPosition;

    public static void SetLastDoorEnteredPosition(Vector2 position)
    {
        lastDoorEnteredPosition = position;
        Debug.Log("Last door entered position set to: " + lastDoorEnteredPosition);
    }
}
