using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonActions : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsUI;

    public void PlayButtonAction()
    {
        SceneManager.LoadScene("GameScene");
        SoundManager.Instance.PlayGameClip();
    }

    public void SettingsButtonAction()
    {
        settingsUI.SetActive(true);
    }

    public void ExitButtonAction()
    {
        Application.Quit();
    }
}
