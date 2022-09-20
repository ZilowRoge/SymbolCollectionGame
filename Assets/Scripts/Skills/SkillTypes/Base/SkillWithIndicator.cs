using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SkillWithIndicator : Skill
{
    public GameObject indicator_prefab;
    public float indicator_range;
    public LayerMask target_mask;

    protected override void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            onCast();
        }
    }

    public override void onActivation()
    {
        GameObject indicator = (GameObject)Instantiate(indicator_prefab, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)), transform);
    }

    protected Collider[] getColliderInIndicator()
    {
        return Physics.OverlapSphere(transform.GetChild(0).position, indicator_range, target_mask);
    }
}
