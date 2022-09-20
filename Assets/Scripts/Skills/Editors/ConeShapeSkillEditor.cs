//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//public class SkillWithIndicatorEditor<T> : Editor where T : SkillWithIndicator
//{
//    protected virtual void OnSceneGUI()
//    {
//        T skill = (T)target;
//        Handles.color = Color.white;
//        Handles.DrawWireArc(skill.transform.position, Vector3.up, Vector3.forward, 360, skill.indicator_range);

//    }

//    public Vector3 getDirectionFromAngle(Transform center, float angle, bool angle_is_global)
//    {
//        if (!angle_is_global)
//        {
//            angle += center.eulerAngles.y;
//        }
//        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
//    }
//}

//[CustomEditor(typeof(PointRangeSkill))]
//public class PointRangeSkillEditor : SkillWithIndicatorEditor<PointRangeSkill>
//{ }

//[CustomEditor(typeof(ConeShapeSkill))]
//public class ConeShapeSkillEditor : SkillWithIndicatorEditor<ConeShapeSkill>
//{
//    protected override void OnSceneGUI()
//    {
//        base.OnSceneGUI();
//        ConeShapeSkill skill = (ConeShapeSkill)target;
//        Vector3 viewAngleA = getDirectionFromAngle(skill.transform, -skill.angle / 2, false);
//        Vector3 viewAngleB = getDirectionFromAngle(skill.transform, skill.angle / 2, false);

//        Handles.DrawLine(skill.transform.position, skill.transform.position + viewAngleA * skill.indicator_range);
//        Handles.DrawLine(skill.transform.position, skill.transform.position + viewAngleB * skill.indicator_range);
//    }
//}

