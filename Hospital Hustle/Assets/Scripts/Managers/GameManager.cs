using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _notificationText;
    private GameObject[] _patientDoors;
    private string _currentTargetDoorId;
    private const string MAIN_LEVEL_NAME = "MainLevel";

    [SerializeField] private Image _suspicionMeter; 
    [SerializeField] private Sprite[] _suspicionSprites;
    [SerializeField] private int _maxSuspicion = 9;
    [SerializeField] private int _currentSuspicion = 0;

    [SerializeField] private Image _healthBarImage; 
    [SerializeField] private Sprite[] _healthBarSprites;
    [SerializeField] private int _maxHealth = 9;
    [SerializeField] private int _currentHealth = 9;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            _currentHealth = _maxHealth;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartNewRound();
        StartHealthDecay();
        UpdateHealthUI();
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

    private void UpdateSuspicionUI()
    {
        if (_currentSuspicion < _suspicionSprites.Length)
        {
            _suspicionMeter.sprite = _suspicionSprites[_currentSuspicion];
        }
    }

    public void IncreaseSuspicion(int amount)
    {
        _currentSuspicion += amount;
        _currentSuspicion = Mathf.Clamp(_currentSuspicion, 0, _maxSuspicion);
        UpdateSuspicionUI();
        if(_currentSuspicion == _maxSuspicion)
        {
            EndGame();
        }
    }

    public void DecreaseSuspicion(int amount)
    {
        _currentSuspicion -= amount;
        _currentSuspicion = Mathf.Clamp(_currentSuspicion, 0, _maxSuspicion);
        UpdateSuspicionUI();
    }

    private void StartHealthDecay()
    {
        StartCoroutine(HealthDecayRoutine());
    }

    private IEnumerator HealthDecayRoutine()
    {
        while (_currentHealth > 0)
        {
            yield return new WaitForSeconds(20f);
            DecreaseHealth(1);
        }

        EndGame();
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        UpdateHealthUI();
    }

    public void DecreaseHealth(int amount)
    {
        _currentHealth = Mathf.Max(_currentHealth - amount, 0);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (_currentHealth > 0 && _currentHealth <= _healthBarSprites.Length)
        {
            _healthBarImage.sprite = _healthBarSprites[_currentHealth - 1]; 
        }
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealthUI();
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
    }
}
