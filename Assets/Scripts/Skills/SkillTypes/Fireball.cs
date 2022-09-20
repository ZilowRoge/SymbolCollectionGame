using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : SimpleProjectile
{
    public float projectail_range;

    public float explosion_range;
    public float damage_in_center;

    public LayerMask enemy_layer;

    private void applayDamageInCircle()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, explosion_range, enemy_layer);
        foreach (Collider target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            float damage_multiplayer = 1 - distance / explosion_range;
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.onDamage(damage_in_center * damage_multiplayer);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        applayDamageInCircle();
        onDestroy();
    }
}
