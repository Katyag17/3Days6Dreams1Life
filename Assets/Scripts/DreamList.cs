using UnityEngine;
using UnityEngine.SceneManagement;

public class DreamList : MonoBehaviour
{
    public void LoadDream(string dreamSceneName)
    {
        SceneManager.LoadScene(dreamSceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
