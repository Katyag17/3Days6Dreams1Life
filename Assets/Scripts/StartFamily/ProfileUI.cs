using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI genderText;
    public TextMeshProUGUI desireText;
    public Image profileImage;

    public void SetUp(Profile profile)
    {
        nameText.text = profile.name;
        genderText.text = profile.gender;
        desireText.text = profile.desire;
        profileImage.sprite = profile.image;
    }
}
