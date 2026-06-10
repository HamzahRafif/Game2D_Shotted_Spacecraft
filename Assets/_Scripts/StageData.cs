using UnityEngine;

[CreateAssetMenu(
    fileName = "New Stage Data",
    menuName = "Game Data/Stage Data"
)]
public class StageData : ScriptableObject
{
    public int stageNumber;

    public float stageDuration;

    public int targetKills;
}