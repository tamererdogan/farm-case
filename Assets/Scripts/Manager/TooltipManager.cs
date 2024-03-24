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

        Transform plantTimeBoostTextTransform = toolTooltipUI.transform.Find(
            "Grid/PlantTimeBoostText"
        );
        plantTimeBoostTextTransform.GetComponent<TMP_Text>().text =
            "Ekim Süresi (Boost): -" + item.plantTimeBoost + " sn";

        Transform harvestTimeBoostTextTransform = toolTooltipUI.transform.Find(
            "Grid/HarvestTimeBoostText"
        );
        harvestTimeBoostTextTransform.GetComponent<TMP_Text>().text =
            "Hasat Süresi (Boost): -" + item.harvestTimeBoost + " sn";

        toolTooltipUI.SetActive(true);
    }

    public void ShowSeedTooltip(SeedSO item)
    {
        ShowTooltip(seedTooltipUI, item);

        Transform plantTimeTextTransform = seedTooltipUI.transform.Find("Grid/PlantTimeText");
        plantTimeTextTransform.GetComponent<TMP_Text>().text =
            "Ekim Süresi: " + item.plantTime + " sn";

        Transform growthTimeTextTransform = seedTooltipUI.transform.Find("Grid/GrowthTimeText");
        growthTimeTextTransform.GetComponent<TMP_Text>().text =
            "Yetişme Süresi: " + item.growthTime + " sn";

        Transform harvestTimeTextTransform = seedTooltipUI.transform.Find("Grid/HarvestTimeText");
        harvestTimeTextTransform.GetComponent<TMP_Text>().text =
            "Hasat Süresi: " + item.harvestTime + " sn";

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
            Transform buyPriceTextTransform = container.transform.Find("Grid/BuyPriceText");
            buyPriceTextTransform.GetComponent<TMP_Text>().text = "Alış Fiyatı: " + item.buyPrice;
        }

        Transform sellPriceTextTransform = container.transform.Find("Grid/SellPriceText");
        sellPriceTextTransform.GetComponent<TMP_Text>().text = "Satış Fiyatı: " + item.sellPrice;

        Transform levelTextTransform = container.transform.Find("Grid/LevelText");
        levelTextTransform.GetComponent<TMP_Text>().text = "Level: " + item.level;
    }

    public void HideTooltip()
    {
        toolTooltipUI.SetActive(false);
        seedTooltipUI.SetActive(false);
        cropTooltipUI.SetActive(false);
    }
}
