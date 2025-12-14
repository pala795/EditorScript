using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemStats))]
public class ItemStatsDrawer : PropertyDrawer
{
    private float SingleLine => EditorGUIUtility.singleLineHeight;
    private float TotalHeight = 0f;

    private const string Light = "Light";
    private const string Medium = "Medium";
    private const string Heavy = "Heavy";
    private const float LightThreshold = 10f;
    private const float HeavyThreshold = 30f;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var dimensionProperty = property.FindPropertyRelative(nameof(ItemStats.Dimension));
        return SingleLine * (5 + dimensionProperty.vector2IntValue.y);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var valueProperty = property.FindPropertyRelative(nameof(ItemStats.Value));
        var weightProperty = property.FindPropertyRelative(nameof(ItemStats.Weight));
        var dimensionProperty = property.FindPropertyRelative(nameof(ItemStats.Dimension));
        var durabilityProperty = property.FindPropertyRelative(nameof(ItemStats.Durability));

        TotalHeight = position.y;

        DrawValue(position, valueProperty);
        DrawWeight(position, weightProperty);
        DrawDimension(position, dimensionProperty);
        DrawDurability(position, durabilityProperty);
        
        EditorGUI.EndProperty();
    }

    private void DrawValue(Rect position, SerializedProperty property)
    {
        var rect = new Rect(position.x, TotalHeight, position.width * .66f, SingleLine);
        EditorGUI.PropertyField(rect, property);
        TotalHeight += SingleLine;
    }

    private void DrawWeight(Rect position, SerializedProperty property)
    {
        var rect = new Rect(position.x, TotalHeight, position.width * .66f, SingleLine);
        var textRect = new Rect(rect.xMax + 10, TotalHeight, position.width * .33f - 10, SingleLine);
        EditorGUI.PropertyField(rect, property);
        var text = property.floatValue switch
        {
            < LightThreshold => Light,
            > HeavyThreshold => Heavy,
            _ => Medium
        };
        GUI.Label(textRect, text);
        TotalHeight += SingleLine;
    }
    
    private void DrawDimension(Rect position, SerializedProperty property)
    {
        var labelRect = new Rect(position.x, TotalHeight, position.width * .39f, SingleLine);
        var valuesRect = new Rect(labelRect.xMax, TotalHeight, position.width * .27f, SingleLine);
        GUI.Label(labelRect, "Dimension");
        var currValue = property.vector2IntValue;
        var xRect =
            new Rect(valuesRect.x, TotalHeight, valuesRect.width / 2f - 8, SingleLine);
        var xLabelRect = new Rect(xRect.xMax, TotalHeight, 16, SingleLine);
        var yRect = new Rect(xLabelRect.xMax, TotalHeight, valuesRect.width / 2f - 8,
            SingleLine);
        currValue.x = Mathf.Max(1, EditorGUI.IntField(xRect, currValue.x));
        GUI.Label(xLabelRect, "x", new GUIStyle(EditorStyles.label) { alignment = TextAnchor.LowerCenter });
        currValue.y = Mathf.Max(1, EditorGUI.IntField(yRect, currValue.y));
        property.vector2IntValue = currValue;
        TotalHeight += SingleLine;
        
        for (var y = 0; y < currValue.y; y++)
        {
            for (var x = 0; x < currValue.x; x++)
            {
                var cellRect = new Rect(position.x + x * SingleLine, 
                    TotalHeight, SingleLine, SingleLine);
                GUI.Button(cellRect, "");
            }
            TotalHeight += SingleLine;
        }
    }
    
    private void DrawDurability(Rect position, SerializedProperty property)
    {
        var labelRect = new Rect(position.x, TotalHeight, position.width * .39f, SingleLine);
        var valuesRect = new Rect(labelRect.xMax, TotalHeight, position.width * .27f, SingleLine);
        GUI.Label(labelRect, "Durability");
        var currValue = property.vector2Value;
        var xRect =
            new Rect(valuesRect.x, TotalHeight, valuesRect.width / 2f - 8, SingleLine);
        var xLabelRect = new Rect(xRect.xMax, TotalHeight, 16, SingleLine);
        var yRect = new Rect(xLabelRect.xMax, TotalHeight, valuesRect.width / 2f - 8,
            SingleLine);
        currValue.x = Mathf.Min(currValue.y, Mathf.Max(0, EditorGUI.FloatField(xRect, currValue.x)));
        GUI.Label(xLabelRect, "/", new GUIStyle(EditorStyles.label) { alignment = TextAnchor.LowerCenter });
        currValue.y = Mathf.Max(0, EditorGUI.FloatField(yRect, currValue.y));
        property.vector2Value = currValue;
        
        var sliderRect = new Rect(valuesRect.xMax + 16, TotalHeight, position.width * .33f - 16, SingleLine);
        EditorGUI.DrawRect(sliderRect, Color.black);
        sliderRect.position += new Vector2(3, 3);
        sliderRect.width -= 6;
        sliderRect.height -= 6;
        
        EditorGUI.DrawRect(sliderRect, Color.gray);

        sliderRect.width *= Mathf.InverseLerp(0, currValue.y, currValue.x);
        EditorGUI.DrawRect(sliderRect, Color.cyan);
        
        TotalHeight += SingleLine;
    }
}