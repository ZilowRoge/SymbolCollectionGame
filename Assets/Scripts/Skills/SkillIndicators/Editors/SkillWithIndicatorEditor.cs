//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(SpellIndicator))]
//public class SpellIndicatorEditor<T> : Editor where T : SpellIndicator
//{
//    private T instance { get { return (T)target; } }

//    public override void OnInspectorGUI()
//    {
//        if (instance == null)
//            return;

//        EditorGUI.BeginChangeCheck();

//        DrawDefaultInspector();

//        if (EditorGUI.EndChangeCheck())
//        {
//            Debug.Log(typeof(T) + " Works!");
//            instance.onValueChange();
//        }
//    }
//}

//[CustomEditor(typeof(PointIndicator))]
//public class PointIndicatorEditor : SpellIndicatorEditor<PointIndicator>
//{ }

//[CustomEditor(typeof(ConeIndicator))]
//public class ConeIndicatorEditor : SpellIndicatorEditor<ConeIndicator>
//{ }
