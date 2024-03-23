using UnityEngine;

[CreateAssetMenu(menuName = "Item / New Seed")]
public class SeedSO : ItemSO
{
    public int harvestCount;

    public float growthTime;

    public GameObject smallPrefab;

    public GameObject middlePrefab;

    public GameObject largePrefab;
}
