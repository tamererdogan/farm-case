using UnityEngine;

public class ItemSO : ScriptableObject
{
    public int id;

    public int level;

    public string itemName;

    public Sprite icon;

    public GameObject prefab;

    public float buyPrice;

    public float sellPrice;
}
