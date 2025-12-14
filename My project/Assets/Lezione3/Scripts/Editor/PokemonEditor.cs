using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Pokemon))]
public class PokemonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Edit", GUILayout.Height(50)))
        {
            PokemonWizard.Open(target as Pokemon);
        }
        GUILayout.Space(20); 

        GUI.enabled = false;
        base.OnInspectorGUI();
        GUI.enabled = true;
    }
}
