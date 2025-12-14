using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[SelectionBase]
public class SelectionBase : MonoBehaviour
{
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.DrawDottedLine(Vector3.zero, new Vector3(3,3,3), 5f);
#endif
    }
}
