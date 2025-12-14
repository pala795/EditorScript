using UnityEngine;

[CreateAssetMenu(menuName = "Proj/CharacterStats", fileName = "CH_Stats_")]
public class CharacterStats : ScriptableObject
{
    public float Hp;
    public string Name;
    public float MaxHp;
    public GameObject Prefab;
}
