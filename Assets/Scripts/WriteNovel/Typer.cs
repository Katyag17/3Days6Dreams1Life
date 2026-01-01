using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Typer : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public string targetText = "Once upon a time, a weird eye thing appeared in front of me and told me I will die in three days. So, I decided to write a novel. The End.";
    private int currentIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (char c in Input.inputString)
            {
                if (currentIndex < targetText.Length)
                {
                    if (c == targetText[currentIndex])
                    {
                        currentIndex++;
                        UpdateDisplay();
                        if (currentIndex >= targetText.Length)
                        {
                            SceneManager.LoadScene("WriteNovelWin");
                        }
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
            }
        }
    }
    void UpdateDisplay()
    {
        string result = "";
        for (int i = 0; i < targetText.Length; i++)
        {
            if (i < currentIndex)
            {
                result += $"<color=black>{targetText[i]}</color>";
            }
            else
            {
                result += $"<color=white>{targetText[i]}</color>";
            }
        }
        displayText.text = result;
    }
}
