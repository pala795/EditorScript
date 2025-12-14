using UnityEngine;
using UnityEditor;

public class MenuItemExercise : MonoBehaviour
{
    [MenuItem("Mio Menu/SetParent %#F1")]
    static void SetParent()
    {
        Debug.Log("SetParent menu item clicked");

    }

    [MenuItem("Mio Menu/SetParent %#F1")]
    static void GetDistance()
    {
        Transform obj1 = Selection.transforms[0];
        Transform obj2 = Selection.transforms[1];

        Vector3 pos1 = obj1.position;
        Vector3 pos2 = obj2.position;
        float distance = Vector3.Distance(pos1, pos2);
        Debug.Log("Distance between " + obj1.name + " and " + obj2.name + " is: " + distance);
    }
    [MenuItem("Mio Menu/SetParent %#F1",true)]
    static bool ValidateGetDistance()
    {
        return Selection.transforms.Length == 1;
    }
}

