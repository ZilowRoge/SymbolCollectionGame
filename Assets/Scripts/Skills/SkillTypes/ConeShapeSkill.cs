using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConeShapeSkill : SkillWithIndicator
{
    public float angle;

    protected override void Start()
    {
        base.Start();
        onActivation();
    }

    protected override void Update()
    {
        base.Update();
        transform.position = player_transform.position;
        setConeRotation(player_transform.rotation.eulerAngles.y);
    }
    private void setConeRotation(float rotation)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotation, transform.rotation.eulerAngles.z);
    }

    public override void onCast()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, indicator_range, target_mask);
        for(int i = 0; i < colliders.Length; i++)
        {
            if (isInCone(colliders[i]))
            {
                Debug.Log(colliders[i].gameObject.name);//damage calculation here
            }
        }
    }

    private bool isInCone(Collider col)
    {
        Transform target = col.transform;
        Vector3 direction_to_target = (target.position - transform.position).normalized;
        return Vector3.Angle(transform.forward, direction_to_target) < angle / 2;
    }
}
