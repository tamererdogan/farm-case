using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    #region SINGLETON
    public static VibrationManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion SINGLETON

    private bool isVibrationOn;

    public void VibrationOn()
    {
        isVibrationOn = true;
    }

    public void VibrationOff()
    {
        isVibrationOn = false;
    }

    public bool IsVibrationOn()
    {
        return isVibrationOn;
    }

    public void Vibrate()
    {
        if (!isVibrationOn)
            return;

#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
    }
}
