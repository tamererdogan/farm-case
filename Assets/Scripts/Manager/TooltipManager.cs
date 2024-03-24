using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private GameObject toolTooltipUI;

    [SerializeField]
    private GameObject seedTooltipUI;

    [SerializeField]
    private GameObject cropTooltipUI;

    public void ShowToolTooltip(ToolSO item)
    {
        ShowTooltip(toolTooltipUI, item);
        Transform efficiencyTextTransform = toolTooltipUI.transform.Find("EfficiencyText");
        efficiencyTextTransform.GetComponent<TMP_Text>().text = "Verim: " + item.efficiency;
        Transform growthBoostTimeTextTransform = toolTooltipUI.transform.Find(
            "GrowthBoostTimeText"
        );
        growthBoostTimeTextTransform.GetComponent<TMP_Text>().text =
            "Y. Hızı: " + item.growthBoostTime;

        toolTooltipUI.SetActive(true);
    }

    public void ShowSeedTooltip(SeedSO item)
    {
        ShowTooltip(seedTooltipUI, item);
        Transform harvestCountTextTransform = seedTooltipUI.transform.Find("HarvestCountText");
        harvestCountTextTransform.GetComponent<TMP_Text>().text =
            "Hasat Adedi: " + item.harvestCount;
        Transform growthTimeTextTransform = seedTooltipUI.transform.Find("GrowthTimeText");
        growthTimeTextTransform.GetComponent<TMP_Text>().text = "Y. Süresi: " + item.growthTime;
        seedTooltipUI.SetActive(true);
    }

    public void ShowCropTooltip(CropSO item)
    {
        ShowTooltip(cropTooltipUI, item, false);
        cropTooltipUI.SetActive(true);
    }

    private void ShowTooltip(GameObject container, ItemSO item, bool buyPriceOpen = true)
    {
        Transform itemIconTransform = container.transform.Find("ItemIcon");
        itemIconTransform.GetComponent<Image>().sprite = item.icon;
        Transform itemNameTransform = container.transform.Find("ItemName");
        itemNameTransform.GetComponent<TMP_Text>().text = item.itemName;
        if (buyPriceOpen)
        {
            Transform buyPriceTextTransform = container.transform.Find("BuyPriceText");
            buyPriceTextTransform.GetComponent<TMP_Text>().text = "Alış Fiyatı: " + item.buyPrice;
        }
        Transform sellPriceTextTransform = container.transform.Find("SellPriceText");
        sellPriceTextTransform.GetComponent<TMP_Text>().text = "Satış Fiyatı: " + item.sellPrice;
    }

    public void HideTooltip()
    {
        toolTooltipUI.SetActive(false);
        seedTooltipUI.SetActive(false);
        cropTooltipUI.SetActive(false);
    }
}
