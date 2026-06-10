using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "Game Data/ShipData")]
public class ShipData : ScriptableObject
{
    public string shipName;
    public int maxHealth;
    public float moveSpeed;
    public int damage;
    public int unlockCost;
}