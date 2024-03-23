using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    #region SINGLETON
    public static TooltipManager Instance { get; private set; }

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
}
