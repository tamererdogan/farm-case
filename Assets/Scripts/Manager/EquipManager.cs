using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    #region SINGLETON
    public static EquipManager Instance { get; private set; }

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

    private int? toolItemId;

    private int? seedItemId;

    [SerializeField]
    private Slot toolSlot;

    [SerializeField]
    private Slot seedSlot;

    [SerializeField]
    private Sprite slotPlaceHolder;

    void Start()
    {
        UpdateDisplay();
    }

    public int? GetToolItemId()
    {
        return toolItemId;
    }

    public int? GetSeedItemId()
    {
        return seedItemId;
    }

    public void EquipTool(int? itemId)
    {
        toolItemId = itemId;
        UpdateDisplay();
    }

    public void EquipSeed(int? itemId)
    {
        seedItemId = itemId;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        Image toolIconImage = toolSlot.GetComponent<Image>();
        if (toolIconImage != null)
        {
            if (toolItemId != null)
            {
                ItemSO item = DataManager.Instance.GetItem((int)toolItemId);
                toolIconImage.sprite = item.icon;
            }
            else
            {
                toolIconImage.sprite = slotPlaceHolder;
            }
        }

        Image seedIconImage = seedSlot.GetComponent<Image>();
        if (seedIconImage != null)
        {
            if (seedItemId != null)
            {
                ItemSO item = DataManager.Instance.GetItem((int)seedItemId);
                seedIconImage.sprite = item.icon;
            }
            else
            {
                seedIconImage.sprite = slotPlaceHolder;
            }
        }
    }
}
