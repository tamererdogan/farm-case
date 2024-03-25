using TMPro;
using UnityEngine;

public class SettingsButtonActions : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsUI;

    [SerializeField]
    private GameObject controlsUI;

    [SerializeField]
    private TMP_Text volumeValueInfo;

    [SerializeField]
    private TMP_Text vibrationValueInfo;

    void Start()
    {
        UpdateVolumeDisplay();
        UpdateVibrationDisplay();
    }

    public void CloseSettingsAction()
    {
        settingsUI.SetActive(false);
    }

    public void CloseControlsAction()
    {
        controlsUI.SetActive(false);
    }

    public void VolumeUpAction()
    {
        SoundManager.Instance.VolumeUp();
        UpdateVolumeDisplay();
    }

    public void VolumeDownAction()
    {
        SoundManager.Instance.VolumeDown();
        UpdateVolumeDisplay();
    }

    public void UpdateVolumeDisplay()
    {
        float volume = SoundManager.Instance.GetVolume();
        volumeValueInfo.text = "%" + Mathf.Round(volume * 100);
    }

    public void VibrationOnAction()
    {
        VibrationManager.Instance.VibrationOn();
        UpdateVibrationDisplay();
    }

    public void VibrationOffAction()
    {
        VibrationManager.Instance.VibrationOff();
        UpdateVibrationDisplay();
    }

    public void UpdateVibrationDisplay()
    {
        bool isVibrationOn = VibrationManager.Instance.IsVibrationOn();
        vibrationValueInfo.text = isVibrationOn ? "Açık" : "Kapalı";
    }
}
