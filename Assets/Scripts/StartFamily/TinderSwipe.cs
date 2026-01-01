using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class TinderSwipe : MonoBehaviour
{
    public List<Profile> profiles;
    private int currentIndex = 0;
    private Profile CurrentProfile => profiles[currentIndex];
    public GameObject profileCardPrefab;
    public Transform spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (profiles.Count == 0)
        {
            return;
        }

        ShowProfile(CurrentProfile);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            OnSwipeLeft();

        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            OnSwipeRight();
        }
    }
    bool IsValidMatch(Profile profile)
    {
        return profile.gender == "Female" && profile.desire == "wants kids NOW";
    }
    public void OnSwipeLeft()
    {
        if (!IsValidMatch(CurrentProfile))
        {
            NextProfile();
        }
        else
        {
            RestartGame();
        }
    }
    public void OnSwipeRight()
    {
        if (IsValidMatch(CurrentProfile))
        {
            NextProfile();
        }
        else
        {
            RestartGame();
        }
    }
    void NextProfile()
    {
        currentIndex++;
        if(currentIndex >= profiles.Count)
        {
            SceneManager.LoadScene("StartFamilyWin");
            return;
        }
        ShowProfile(CurrentProfile);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ShowProfile(Profile profile)
    {
        if(profile == null) return;
        foreach (Transform child in spawnPoint)
        {
            Destroy(child.gameObject);
        }
        GameObject card = Instantiate(profileCardPrefab, spawnPoint);
        ProfileUI profileUI = card.GetComponent<ProfileUI>();
        if (profileUI != null)
        {
            profileUI.SetUp(profile);
        }
        else
        {
            Debug.LogError("Missing");
        }
        Debug.Log($"Profile: {profile.name}, {profile.gender}, {profile.desire}");
    }
}
