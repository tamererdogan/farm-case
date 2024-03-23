using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bagUI;

    protected List<int?> itemIds;

    protected virtual void Start()
    {
        itemIds = new List<int?>();
        for (int i = 0; i < 16; i++)
            itemIds.Add(null);
    }

    public void ToggleUI()
    {
        bagUI.SetActive(!bagUI.activeSelf);
    }

    public bool IsOpen()
    {
        return bagUI.activeSelf;
    }

    public void UpdateDisplay()
    {
        Slot[] slots = bagUI.GetComponentsInChildren<Slot>();
        for (int i = 0; i < itemIds.Count; i++)
        {
            Image iconImage = slots[i].GetComponentsInChildren<Image>()[1];
            if (iconImage == null)
                continue;

            if (itemIds[i] == null)
            {
                iconImage.enabled = false;
                continue;
            }

            ItemSO item = DataManager.Instance.GetItem((int)itemIds[i]);
            iconImage.sprite = item.icon;
            iconImage.enabled = true;
        }
    }
}
