using UnityEngine;

public class PlantField : MonoBehaviour
{
    private int state;

    private float elapsedTime = 0f;

    private SeedSO seedSO;

    private float plantTimeBoost;

    private float harvestTimeBoost;

    private GameObject currentObject;

    private GameObject fxObject;

    void Start()
    {
        fxObject = transform.Find("DustFX").gameObject;
        ResetPlantField();
    }

    void Update()
    {
        if (seedSO == null)
            return;

        CheckPlantingState();
        CheckGrowthingState();
        CheckHarvestingState();
    }

    public int GetState()
    {
        return state;
    }

    public void Plant(SeedSO seedSO, float plantTimeBoost)
    {
        this.seedSO = seedSO;
        this.plantTimeBoost = plantTimeBoost;
        fxObject.SetActive(true);
        NextState();
    }

    public void Harvest(float harvestTimeBoost)
    {
        this.harvestTimeBoost = harvestTimeBoost;
        fxObject.SetActive(true);
        NextState();
    }

    private void CheckPlantingState()
    {
        if (state != 1)
            return;

        float plantTime = seedSO.plantTime - plantTimeBoost;
        elapsedTime += Time.deltaTime;
        if (plantTime - elapsedTime > 0)
            return;

        fxObject.SetActive(false);
        //Small Object Instantiate
        CreatePlantObject(seedSO.smallPrefab);

        NextState();
    }

    private void CheckGrowthingState()
    {
        if (!(state == 2 || state == 3))
            return;

        float growthTime = seedSO.growthTime / 2;
        elapsedTime += Time.deltaTime;
        if (growthTime - elapsedTime > 0)
            return;

        if (state == 2)
        {
            //Medium Object Instantiate
            CreatePlantObject(seedSO.middlePrefab);
        }

        if (state == 3)
        {
            //Large Object Instantiate
            CreatePlantObject(seedSO.largePrefab);
        }

        NextState();
    }

    private void CheckHarvestingState()
    {
        if (state != 5)
            return;

        float harvestTime = seedSO.harvestTime - harvestTimeBoost;
        elapsedTime += Time.deltaTime;
        if (harvestTime - elapsedTime > 0)
            return;

        InventoryManager.Instance.AddItem(seedSO.cropId);
        fxObject.SetActive(false);
        ResetPlantField();
    }

    private void CreatePlantObject(GameObject prefab)
    {
        Destroy(currentObject);
        GameObject plantObject = Instantiate(prefab, transform);
        plantObject.transform.localScale = new Vector3(1, 1, 1);
        plantObject.transform.localPosition = new Vector3(0, 0.55f, 0);
        currentObject = plantObject;
    }

    public void NextState()
    {
        elapsedTime = 0f;
        state++;
    }

    public void ResetPlantField()
    {
        state = 0;
        elapsedTime = 0f;
        seedSO = null;
        plantTimeBoost = 0f;
        harvestTimeBoost = 0f;
        Destroy(currentObject);
        fxObject.SetActive(false);
    }
}
