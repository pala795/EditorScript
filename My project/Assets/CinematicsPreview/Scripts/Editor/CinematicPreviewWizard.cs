using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
public class CinematicPreviewWindow : EditorWindow
{
    
    private static CinematicPreview _cinematicPreview;
    private static SerializedObject _serializedCinematiucObject;
    private float t = 0f; 

    [MenuItem("MyuWindow/CinematicPreviewWindow")]
    public static void OpenWindow(CinematicPreview cinematicPreview)
    {
        if(cinematicPreview != null)
        {
            return;
        }
        _cinematicPreview = cinematicPreview;
        _serializedCinematiucObject = new SerializedObject(_cinematicPreview);
        GetWindow<CinematicPreviewWindow>();
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
        if(_cinematicPreview == null)
        {
            EditorGUILayout.LabelField("No Cinematic Preview Selected");
            return;
        }
        _serializedCinematiucObject.Update();
        EditorGUILayout.PropertyField(_serializedCinematiucObject.FindProperty("posX"));
        EditorGUILayout.PropertyField(_serializedCinematiucObject.FindProperty("posY"));
        EditorGUILayout.PropertyField(_serializedCinematiucObject.FindProperty("posZ"));
        EditorGUILayout.PropertyField(_serializedCinematiucObject.FindProperty("rotX"));
        EditorGUILayout.PropertyField(_serializedCinematiucObject.FindProperty("rotY"));
        EditorGUILayout.PropertyField(_serializedCinematiucObject.FindProperty("rotZ"));
        EditorGUILayout.PropertyField(_serializedCinematiucObject.FindProperty("totalTime"));
        t = EditorGUILayout.Slider("Time", t, 0f, _cinematicPreview.totalTime);
        _serializedCinematiucObject.ApplyModifiedProperties();
    }
}

