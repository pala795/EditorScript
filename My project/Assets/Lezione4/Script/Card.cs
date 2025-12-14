using UnityEngine;

[CreateAssetMenu(menuName = "EditorScripting/Card", fileName = "CARD_name")]
public class Card : ScriptableObject
{
    public string Name;
    public int Cost;
    public int MaxAmount = 2;
}
