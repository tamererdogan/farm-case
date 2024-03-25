using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleManager : MonoBehaviour
{
    #region SINGLETON
    public static ConsoleManager Instance { get; private set; }

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

    [SerializeField]
    private GameObject consoleUI;

    [SerializeField]
    private GameObject contentUI;

    [SerializeField]
    private Scrollbar scrollbar;

    [SerializeField]
    private GameObject messagePrefab;

    public void AddMessage(string message, Color color = default)
    {
        if (color == default)
            color = Color.white;

        GameObject messageObject = Instantiate(messagePrefab, contentUI.transform);
        TMP_Text textObject = messageObject.GetComponent<TMP_Text>();
        textObject.text = message;
        textObject.color = color;

        scrollbar.value = 0f;
    }

    public void ToggleUI()
    {
        consoleUI.SetActive(!consoleUI.activeSelf);
    }
}
