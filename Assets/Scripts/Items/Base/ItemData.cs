using UnityEngine;


public class Item : ScriptableObject
{
    public string itemName;
    public int price;
    public Sprite itemIcon;
    public GameObject itemPrefab;

    public bool isStackable;
    public int amount;
}
