using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Vector2 _defaultSpawnPoint;

    void Start()
    {
        if (_player != null)
        {
            Debug.Log("Player found");
            if (DoorManager.lastDoorEnteredPosition != Vector2.zero)
            {
                Debug.Log("Last door entered position: " + DoorManager.lastDoorEnteredPosition);
                _player.transform.position = DoorManager.lastDoorEnteredPosition;
            }
            else
            {
                _player.transform.position = _defaultSpawnPoint;
            }
        }
    }
}
