using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class SimpleProjectile : Skill
{
    public float speed;
    private bool casted = false;

    protected override void Update()
    {
        base.Update();
        if (casted)
        {
            applayMovment();
        }
    }
    public override void onActivation()
    {
        transform.position = player_transform.position + player_transform.forward + player_transform.right;
    }

    public override void onCast()
    {
        casted = true;
    }

    protected virtual void applayMovment()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

}
