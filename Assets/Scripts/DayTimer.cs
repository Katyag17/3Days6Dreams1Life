using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class DayTimer : MonoBehaviour
{
    public static DayTimer Instance;
    public float dayDuration = 300f; //5 minutes for now, can change later
    private float timeLeft;
    public int currentDay = 1;
    public int totalDays = 3;
    public List<string> activeTimerScenes;
    public TextMeshProUGUI timerText;
    private bool isTimerRunning = false;
    private void Awake()
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTimerRunning) return;
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timeLeft = 0;
            EndDay();
        }
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timerText = GameObject.FindWithTag("TimerText")?.GetComponent<TextMeshProUGUI>();
        isTimerRunning = activeTimerScenes.Contains(scene.name);
        if (isTimerRunning)
        {
            if (timeLeft == 0 || timeLeft == dayDuration)
            {
                timeLeft = dayDuration;
            }

            if (timerText != null)
            {
                UpdateTimerUI();
            }
        }
        else
        {
            if (timerText != null)
            {
                timerText.text = "";
            }
        }
    }

    void EndDay()
    {
        //Later: trigger next day or game over
        Debug.Log("Day over!");
        currentDay++;

        if (currentDay > totalDays)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("EndScene");
        }
        else
        {
            StartNewDay();
            SceneManager.LoadScene("DreamList");
        }
    }
    public void StartNewDay() {
        timeLeft = dayDuration;
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }
}
