using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapArea))]
public class MapAreaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        int totalChanceInGrass = serializedObject.FindProperty("totalChance").intValue;
        int totalChanceInWater = serializedObject.FindProperty("totalChanceInWater").intValue;

        if (totalChanceInGrass != 100 && totalChanceInGrass != -1) 
            EditorGUILayout.HelpBox($"Tổng tỷ lệ gặp pokemon hoang dã trong bụi cỏ là {totalChanceInGrass} không phải 100", MessageType.Error);
        if (totalChanceInWater != 100 && totalChanceInWater != -1)
            EditorGUILayout.HelpBox($"Tổng tỷ lệ gặp pokemon hoang dã dưới nước là {totalChanceInWater} không phải 100", MessageType.Error);
    }
}
