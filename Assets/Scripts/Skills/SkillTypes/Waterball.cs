using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterball : SimpleProjectile
{
    public float cone_range;
    [Range(0, 360)]
    public float cone_angle;
    public float damage;
    public LayerMask target_mask;

    void applayDamageInCone()
    {
        Collider[] targets_in_view = Physics.OverlapSphere(transform.position, cone_range, target_mask);
        for (int i = 0; i < targets_in_view.Length; i++)
        {
            Transform target = targets_in_view[i].transform;
            Vector3 direction_to_target = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, direction_to_target) < cone_angle / 2)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                float damage_multiplayer = 1 - distance / cone_range;
                target.GetComponent<Enemy>().onDamage(damage * damage_multiplayer);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Waterball collision");
        applayDamageInCone();
        onDestroy();
    }
}
