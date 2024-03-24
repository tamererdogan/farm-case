using UnityEngine;

[CreateAssetMenu(menuName = "Item / New Seed")]
public class SeedSO : ItemSO
{
    public float plantTime;

    public float growthTime;

    public float harvestTime;

    public GameObject smallPrefab;

    public GameObject middlePrefab;

    public GameObject largePrefab;
}
