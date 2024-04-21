using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _notificationText;
    private GameObject[] _patientRooms;
    private string _currentTargetRoomId;
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
            Debug.Log("Main level loaded");
            RefreshRoomReferences();
            if (_currentTargetRoomId != null)
            {
                UpdateNotification(_currentTargetRoomId);
            }
        }
    }

    private void RefreshRoomReferences()
    {
        _patientRooms = GameObject.FindGameObjectsWithTag("PatientRoom");
        if (_patientRooms.Length == 0)
        {
            Debug.LogError("No patient rooms found!");
        }
    }

    public void StartNewRound()
    {
        GameObject selectedRoom = SelectRandomRoom();
        if (selectedRoom != null)
        {
            _currentTargetRoomId = selectedRoom.name;
            UpdateNotification(_currentTargetRoomId);
        }
    }

    private GameObject SelectRandomRoom()
    {
        if (_patientRooms == null || _patientRooms.Length == 0)
        {
            Debug.LogError("No patient rooms available!");
            return null;
        }
        int randomIndex = Random.Range(0, _patientRooms.Length);
        return _patientRooms[randomIndex];
    }

    private void UpdateNotification(string roomId)
    {
        if (_notificationText != null)
        {
            _notificationText.text = "Please go to Room: " + roomId;
        }
        else
        {
            Debug.LogError("Notification text UI is not set!");
        }
    }

    public string GetCurrentTargetRoomId()
    {
        return _currentTargetRoomId;
    }
}
