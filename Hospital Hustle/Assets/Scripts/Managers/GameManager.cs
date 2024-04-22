using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsDefibRound { get; private set; } = false;

    private TextMeshProUGUI _notificationText;
    private GameObject[] _patientDoors;
    private string _currentTargetDoorId;
    private const string MAIN_LEVEL_NAME = "MainLevel";

    private Image _suspicionMeter;
    [SerializeField] private Sprite[] _suspicionSprites;
    [SerializeField] private int _maxSuspicion = 9;
    [SerializeField] private int _currentSuspicion = 0;

    private Image _healthBarImage;
    [SerializeField] private Sprite[] _healthBarSprites;
    [SerializeField] private int _maxHealth = 9;
    [SerializeField] private int _currentHealth = 9;

    private bool _initialMainLevelLoad = true;
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
        FindNotificationText();
        FindHealthMeter();
        FindSusMeter();
        StartNewRound();
        StartHealthDecay(20);
        UpdateHealthUI();
        _currentHealth = 8;
        _currentSuspicion = 0;
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
            if (Patient.Instance != null && Patient.Instance.Healed)
            {
                Destroy(Patient.Instance.gameObject);
                StartNewDefibRound();
            }
            FindNotificationText();
            if (_notificationText != null)
            {
                if (_initialMainLevelLoad)
                {
                    _notificationText.enabled = true;
                    _initialMainLevelLoad = false;
                }
            }
        }
        else
        {
            FindNotificationText();

            if (_notificationText != null)
            {
                _notificationText.enabled = false;
            }
        }
    }

    private void FindNotificationText()
    {
        if (_notificationText == null)
        {
            _notificationText = GameObject.FindGameObjectWithTag("NotificationText")?.GetComponent<TextMeshProUGUI>();
            if (_notificationText == null)
            {
                Debug.LogError("Notification text component not found.");
            }
        }
    }

    private void FindSusMeter()
    {
        if (_suspicionMeter == null)
        {
            _suspicionMeter = GameObject.FindGameObjectWithTag("SusMeter")?.GetComponent<Image>();
            if (_suspicionMeter == null)
            {
                Debug.LogError("Sus Meter component not found.");
            }
        }
    }

    private void FindHealthMeter()
    {
        if (_healthBarImage == null)
        {
            _healthBarImage = GameObject.FindGameObjectWithTag("HealthMeter")?.GetComponent<Image>();
            if (_healthBarImage == null)
            {
                Debug.LogError("Sus Meter component not found.");
            }
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
        _currentHealth = 8;
        _currentSuspicion = 0;
        string selectedDoor = SelectRandomDoor();
        if (selectedDoor != null)
        {
            _currentTargetDoorId = selectedDoor;
            UpdateNotification(_currentTargetDoorId);
        }
    }

    public void StartNewDefibRound()
    {
        string selectedDoor = SelectRandomDoor();
        if (selectedDoor != null)
        {
            IsDefibRound = true;
            _currentTargetDoorId = selectedDoor;
            HeartAttackNotification(_currentTargetDoorId);
            StartHealthDecay(10);
        }
        else
        {
            Debug.LogError("No patient rooms available!");
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
        FindNotificationText();
        if (_notificationText != null)
        {
            _notificationText.text = "Please go to Room " + roomId;
        }
        else
        {
            Debug.LogError("Notification text UI is not set!");
        }
    }

    private void HeartAttackNotification(string roomId)
    {
        FindNotificationText();
        if (_notificationText != null)
        {
            _notificationText.text = "Patient in Room " + roomId + " is having a heart attack!";
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
            FindSusMeter();
            _suspicionMeter.sprite = _suspicionSprites[_currentSuspicion];
        }
    }

    public void IncreaseSuspicion(int amount)
    {
        _currentSuspicion += amount;
        _currentSuspicion = Mathf.Clamp(_currentSuspicion, 0, _maxSuspicion);
        UpdateSuspicionUI();
        if (_currentSuspicion == _maxSuspicion)
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

    private void StartHealthDecay(float timeBetween)
    {
        StartCoroutine(HealthDecayRoutine(timeBetween));
    }

    public void StopHealthDecay()
    {
        StopCoroutine(nameof(HealthDecayRoutine));
    }

    private IEnumerator HealthDecayRoutine(float timeBetween)
    {
        while (_currentHealth > 0)
        {
            yield return new WaitForSeconds(timeBetween);
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
            FindHealthMeter();
            _healthBarImage.sprite = _healthBarSprites[_currentHealth - 1];
        }
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealthUI();
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        // Hide HealthBar and SuspicionMeter
        FindSusMeter();
        FindHealthMeter();
        _healthBarImage.gameObject.SetActive(false);
        _suspicionMeter.gameObject.SetActive(false);

        ChangeScene("EndScene");
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
