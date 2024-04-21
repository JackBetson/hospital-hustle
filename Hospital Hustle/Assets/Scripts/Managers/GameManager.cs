using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _notificationText;
    private GameObject[] _patientDoors;
    private string _currentTargetDoorId;
    private const string MAIN_LEVEL_NAME = "MainLevel";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartNewRound();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == MAIN_LEVEL_NAME)
        {
            RefreshRoomReferences();
            if (_currentTargetDoorId != null)
            {
                UpdateNotification(_currentTargetDoorId);
            }
        }
        else
        {
            _notificationText.enabled = false;
        }
    }

    private void RefreshRoomReferences()
    {
        _patientDoors = GameObject.FindGameObjectsWithTag("PatientRoom");
        if (_patientDoors.Length == 0)
        {
            Debug.LogError("No patient rooms found!");
        }
    }

    public void StartNewRound()
    {
        string selectedDoor = SelectRandomDoor();
        if (selectedDoor != null)
        {
            _currentTargetDoorId = selectedDoor;
            UpdateNotification(_currentTargetDoorId);
        }
    }

    private string SelectRandomDoor()
    {
        if (_patientDoors == null || _patientDoors.Length == 0)
        {
            Debug.LogError("No patient rooms available!");
            return null;
        }
        int randomIndex = Random.Range(0, _patientDoors.Length);
        Door door = _patientDoors[randomIndex].GetComponent<Door>();
        return door != null ? door.doorId : null;
    }

    private void UpdateNotification(string roomId)
    {
        if (_notificationText != null)
        {
            _notificationText.text = "Please go to Room " + roomId;
        }
        else
        {
            Debug.LogError("Notification text UI is not set!");
        }
    }

    public string GetCurrentTargetDoorId()
    {
        return _currentTargetDoorId;
    }
}
