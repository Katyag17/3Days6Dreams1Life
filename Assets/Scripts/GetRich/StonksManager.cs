using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class StonksManager : MonoBehaviour
{
    public LineRenderer stockLine;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI scoreText;
    public float stockValue = 50f; //starting value
    public float maxChange = 5f; // max change per frame
    public float updateInterval = 0.1f; // how often to update
    public float sellThreshold = 75f; //when to sell
    public float buyThreshold = 25f; //when to buy
    private float timeSinceLastUpdate = 0f;
    private bool isStockUp = true;
    private int points = 0;
    private int maxPoint = 10;
    private string currentHint = "";
    private enum HintState  {None, Buy, Sell};
    private HintState currentState = HintState.None;
    private List<float> stockHistory = new List<float>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Debug.Log("Active scene: " + SceneManager.GetActiveScene().name);

        StartCoroutine(InitializeGraph());
    }

    IEnumerator InitializeGraph()
    {
        yield return null;
        if (scoreText != null)
        {
            scoreText.text = "Money made: $" + 0;
        }
        else
        {
            Debug.LogError("scoreText is NULL — did you forget to assign it?");
        }
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("No MainCamera found!");
            yield break;
        }
        stockLine.positionCount = 100;

        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10f));

        for (int i = 0; i < 100; i++)
        {
            float x = bottomLeft.x + i * 0.1f;
            float y = bottomLeft.y + Random.Range(-0.5f, 0.5f);
            stockLine.SetPosition(i, new Vector3(x, y, 0));
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (points > maxPoint) return;
        if (points == maxPoint)
        {
            SceneManager.LoadScene("GetRichWin");
        }

        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= updateInterval)
        {
            timeSinceLastUpdate = 0f;
            stockValue += isStockUp ? Random.Range(0, maxChange) : -Random.Range(0, maxChange);
            stockValue = Mathf.Clamp(stockValue, 0f, 100f);
            if (Mathf.Abs(stockValue - buyThreshold) <= 2f)
            {
                if (currentState != HintState.Buy)
                {
                    currentState = HintState.Buy;
                    currentHint = "Buy now!";
                    if (feedbackText.text != currentHint)
                    {
                        feedbackText.text = currentHint;
                    }  
              }
            }
            else if (Mathf.Abs(stockValue - sellThreshold) <= 2f)
            {
                if (currentState != HintState.Sell)
                {
                    currentState = HintState.Sell;
                    currentHint = "Sell now!";
                    if (feedbackText.text != currentHint)
                    {
                        feedbackText.text = currentHint;
                    }  
                }
            }
            else
            {
                if (currentState != HintState.None)
                {
                    currentState = HintState.None;
                    currentHint = "";
                    if (feedbackText.text != currentHint)
                    {
                        feedbackText.text = currentHint;
                    }
                }
            }

            if (stockValue == 0f || stockValue == 100f)
            {
                isStockUp = !isStockUp;
            }
            stockHistory.Add(stockValue);
            if (stockHistory.Count > 100)
            {
                stockHistory.RemoveAt(0);
            }
            stockLine.positionCount = stockHistory.Count;
            Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10f));
            for (int i = 0; i < stockHistory.Count; i++)
            {
                float x = bottomLeft.x + i * 0.1f;
                float y = bottomLeft.y + stockHistory[i] * 0.05f;
                stockLine.SetPosition(i, new Vector3(x, y, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            OnSellButtonPressed();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            OnBuyButtonPressed();
        }
    }

    void OnBuyButtonPressed()
    {
        if (Mathf.Abs(stockValue - buyThreshold) <= 2f)
        {
            Debug.Log("nice");
            points ++;
            scoreText.text = "Money made $" + (points * 10000);
            Debug.Log("Points: " + points);
        }
        else
        {
            Debug.Log("Restart");
            RestartGame();
        }
    }

    void OnSellButtonPressed()
    {
        if (Mathf.Abs(stockValue - sellThreshold) <= 2f)
        {
            Debug.Log("nice");
            points ++;
            scoreText.text = "Money made $" + (points * 10000);
            Debug.Log("Points: " + points);

        }
        else
        {
            Debug.Log("Restart");
            RestartGame();
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene("GetRich");

    }
}
