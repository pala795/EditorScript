using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PokemonWizard : EditorWindow
{
    private static Pokemon _pokemon;
    private static SerializedObject _serializedPokemonObject;

    private Texture2D _yellowTexture;
    private Texture2D YellowTexture
    {
        get
        {
            if (_yellowTexture != null)
            {
                return _yellowTexture;
            }
            _yellowTexture = new Texture2D(1, 1);
            _yellowTexture.SetPixel(0, 0, Color.yellow);
            _yellowTexture.Apply();
            return _yellowTexture;
        }
    }
    
    private bool _statsFoldout;
    
    private const int TYPES_PER_ROW = 5;

    public static void Open(Pokemon pokemon)
    {
        if (pokemon == null)
        {
            return;
        }

        _pokemon = pokemon;
        _serializedPokemonObject = new SerializedObject(_pokemon);
        GetWindow<PokemonWizard>();
    }

    private void OnFocus()
    {
        AssetDatabase.SaveAssets();
    }

    private void OnLostFocus()
    {
        AssetDatabase.SaveAssets();
    }

    private void OnGUI()
    {
        if (_pokemon == null)
        {
            var window = GetWindow<PokemonWizard>();
            window.Close();
            return;
        }
        
        DrawPokemonData();
    }

    private void DrawPokemonData()
    {
        GUILayout.Space(EditorGUIUtility.singleLineHeight);
        DrawPokemonName();
        GUILayout.Space(EditorGUIUtility.singleLineHeight);
        DrawPokemonTypes();
        GUILayout.Space(EditorGUIUtility.singleLineHeight);
        DrawPokemonStats();
        GUILayout.Space(EditorGUIUtility.singleLineHeight);
        DrawPokemonMoves();
    }

    private void DrawPokemonName()
    {
        var name = EditorGUILayout.TextField(nameof(Pokemon.Name), _pokemon.Name);
        if (name != _pokemon.Name)
        {
            _pokemon.Name = name;
            EditorUtility.SetDirty(_pokemon);
        }
    }

    private void DrawPokemonTypes()
    {
        GUILayout.Label(nameof(Pokemon.Types));
        var index = 0;
        foreach (var type in Enum.GetNames(typeof(Pokemon.Type)))
        {
            if (index == 0)
            {
                GUILayout.BeginHorizontal();
            }

            DrawPokemonTypeToggle(Enum.Parse<Pokemon.Type>(type));
            index++;

            if (index == TYPES_PER_ROW)
            {
                index = 0;
                GUILayout.EndHorizontal();
            }
        }

        if (index != 0)
        {
            GUILayout.EndHorizontal();
        }
    }

    private void DrawPokemonTypeToggle(Pokemon.Type type)
    {
        var hasType = _pokemon.Types != null && _pokemon.Types.Contains(type);
        
        var prevBackgroundColor = GUI.backgroundColor;
        if (hasType)
        {
            GUI.backgroundColor = Color.yellow;
        }

        if (GUILayout.Button(type.ToString(), GUILayout.Height(24), 
                GUILayout.ExpandWidth(false)))
        {
            EditorUtility.SetDirty(_pokemon);
            if (hasType)
            {
                _pokemon.Types.Remove(type);
            }
            else
            {
                _pokemon.Types ??= new List<Pokemon.Type>();
                _pokemon.Types.Add(type);
            }
        }

        GUI.backgroundColor = prevBackgroundColor;
    }
    
    private void DrawPokemonStats()
    {
        _serializedPokemonObject.Update();
        
        var statsProperty = _serializedPokemonObject.FindProperty(nameof(Pokemon.Stats));
        EditorGUILayout.PropertyField(statsProperty);
        
        _serializedPokemonObject.ApplyModifiedProperties();
    }
    
    private void DrawPokemonMoves()
    {
        if (_pokemon.Moves == null || _pokemon.Moves.Length != 4)
        {
            _pokemon.Moves = new PokemonMove[4];
            EditorUtility.SetDirty(_pokemon);
        }
        for (var i = 0; i < 4; i++)
        {
            if (i % 2 == 0)
            {
                GUILayout.BeginHorizontal();
            }

            DrawPokemonMove(i);
            
            if (i % 2 == 1)
            {
                GUILayout.EndHorizontal();
            }
        }
    }

    private void DrawPokemonMove(int index)
    {
        var currentMoveName = _pokemon.Moves[index] == null ? "" : _pokemon.Moves[index].Name;
        var moveName = EditorGUILayout.TextField(currentMoveName);

        if (moveName == currentMoveName)
        {
            return;
        }
        
        if (string.IsNullOrEmpty(moveName))
        {
            _pokemon.Moves[index] = null;
        }
        else
        {
            if (_pokemon.Moves[index] == null)
            {
                _pokemon.Moves[index] = new PokemonMove { Name = moveName };
            }
            else
            {
                _pokemon.Moves[index].Name = moveName;
            }
        }
        EditorUtility.SetDirty(_pokemon);
    }
}