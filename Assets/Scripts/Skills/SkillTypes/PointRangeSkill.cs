using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PointRangeSkill : SkillWithIndicator
{
    public float spell_range;

    public override void onCast()
    {
        Collider[] colliders = getColliderInIndicator();
        foreach(Collider col in colliders)
        {
            Debug.Log(col.gameObject.name);
        }
        onDestroy();
    }
}
