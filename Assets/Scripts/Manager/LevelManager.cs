using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region SINGLETON
    public static LevelManager Instance { get; private set; }

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
    private TMP_Text levelText;

    [SerializeField]
    private int[] expMilestones;

    private int currentExp = 0;

    void Start()
    {
        UpdateDisplay();
    }

    public void AddExp(int exp)
    {
        currentExp += exp;
        UpdateDisplay();
    }

    public int GetLevel()
    {
        int level;
        for (level = 0; level < expMilestones.Length; level++)
        {
            if (currentExp >= expMilestones[level])
                continue;

            return level + 1;
        }

        return level + 1;
    }

    private void UpdateDisplay()
    {
        levelText.text = "Level: " + GetLevel();
    }
}
