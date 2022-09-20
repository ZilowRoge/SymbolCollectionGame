using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windball : SimpleProjectile
{
    public float damage;
    public float rotation_speed;
    public Transform target;

    protected override void applayMovment()
    {
        if (target != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotation_speed * Time.deltaTime);
            base.applayMovment();
        }
        else
        {
            onDestroy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.onDamage(damage);
        }
        onDestroy();
    }
}
