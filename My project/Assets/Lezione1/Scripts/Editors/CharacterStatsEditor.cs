#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterStats))]
public class CharacterStatsEditor : Editor
{
    private Texture2D _icon;

    private void OnEnable()
    {
        _icon = (Texture2D)EditorGUIUtility.Load("Assets/TutorialInfo/Icons/URP.png");
    }

    public override void OnInspectorGUI()
    {
        Undo.RecordObject(target, "Character Stats Changes");
        
        BasicInspectorGui();
        
        EditorGUILayout.Space(100);
        
        PropertyInspectorGui();
    }

    private void BasicInspectorGui()
    {
        var characterStats = target as CharacterStats;
        
        //GUI.enabled = false; questo serve a disabilitare l'inspector
        //GUI.enabled = true;

        //GUI.Label(MyCustomRect, "Text"); Senza "Layout" devo inserire una Rect esplicitamente
        
        //GUILayout.Label("Text"); Con "Layout" unity calcola automaticamente le Rect
        
        //base.OnInspectorGUI(); Questo mostra l'editor di default della classe
        
        characterStats.Name = EditorGUILayout.TextField("Name", characterStats.Name);
        
        GUILayout.BeginHorizontal();
        GUILayout.Label("Max Hp");
        characterStats.MaxHp = EditorGUILayout.FloatField(characterStats.MaxHp);
        characterStats.Hp = Mathf.Min(characterStats.Hp, characterStats.MaxHp);
        
        GUILayout.Space(20);
        
        //var prevHp = characterStats.Hp;
        GUILayout.Label("Hp");
        characterStats.Hp = EditorGUILayout.FloatField(characterStats.Hp);
        GUILayout.EndHorizontal();
        //if (prevHp != characterStats.Hp) 
        //{
        //    EditorUtility.SetDirty(characterStats);
        //} questo serve a rendere l'asset "salvabile", il custom editor
        //      lo fa automaticamente
        characterStats.MaxHp = Mathf.Max(characterStats.Hp, characterStats.MaxHp);

        characterStats.Prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", 
            characterStats.Prefab, typeof(GameObject), false);
         
        _icon = EditorGUILayout.ObjectField("Icon", _icon, typeof(Texture2D), 
                false) as Texture2D;
        GUILayout.Box(_icon, GUILayout.Width(_icon.width), GUILayout.Height(_icon.height));

        if (GUILayout.Button("Open Wizard"))
        {
            CharacterStatsWizard.OpenWindow();
        }
    }

    private void PropertyInspectorGui()
    {
        serializedObject.Update();

        var nameProp = serializedObject.FindProperty(nameof(CharacterStats.Name));
        var maxHpProp = serializedObject.FindProperty(nameof(CharacterStats.MaxHp));
        var hpProp = serializedObject.FindProperty(nameof(CharacterStats.Hp));
        var prefabProp = serializedObject.FindProperty(nameof(CharacterStats.Prefab));
        
        EditorGUILayout.PropertyField(nameProp);
        EditorGUILayout.PropertyField(maxHpProp);
        hpProp.floatValue = Mathf.Min(hpProp.floatValue, maxHpProp.floatValue);

        EditorGUILayout.PropertyField(hpProp);
        maxHpProp.floatValue = Mathf.Max(hpProp.floatValue, maxHpProp.floatValue);

        EditorGUILayout.PropertyField(prefabProp);
        
        serializedObject.ApplyModifiedProperties();
    }
}
#endif