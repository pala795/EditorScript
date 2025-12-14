#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterStatsWizard : EditorWindow
{
    private List<CharacterStats> _characterStats;
    
    [MenuItem("MyTools/Character stats Wizard")]
    public static void OpenWindow()
    {
        var window = GetWindow(typeof(CharacterStatsWizard));
        window.minSize = new Vector2(500, 500);
        window.maxSize = new Vector2(502, 502); 
    }

    private void OnEnable()
    {
        Selection.selectionChanged += Repaint;
        
        _characterStats = new List<CharacterStats>();
        var statsGuids = AssetDatabase.FindAssets($"t:{nameof(CharacterStats)}");
        foreach (var guid in statsGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var characterStats = AssetDatabase.LoadAssetAtPath<CharacterStats>(path);
            _characterStats.Add(characterStats);
        }
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= Repaint;
    }

    public void OnGUI()
    {
        GUI.color = Color.gray;
        if (_characterStats == null)
        {
            GUILayout.Label("No character stats found!");
            return;
        }
        foreach (var stats in _characterStats)
        {
            if (GUILayout.Button(stats.Name))
            {
                Selection.SetActiveObjectWithContext(stats, null);
                EditorGUIUtility.PingObject(stats);
            }
        }
    }
}
#endif