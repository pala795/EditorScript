using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon_", menuName = "Lezione3/Pokemon")]
public class Pokemon : ScriptableObject
{
    public enum Type
    {
        Fuoco,
        Acqua,
        Erba,
        Roccia,
        Psico,
        Buio,
        Drago,
        Folletto,
        Ghiaccio,
        Terra,
        Acciaio,
        Insetto,
        Spettro,
        Normale,
        Lotta,
        Volante,
        Elettro,
        Veleno
    }

    public string Name;
    public List<Type> Types;
    public PokemonStats Stats;
    public PokemonMove[] Moves;
}

[System.Serializable]
public class PokemonStats
{
    public int Hp;
    public int Def;
    public int Atk;
}

[System.Serializable]
public class PokemonMove
{
    public string Name;
}