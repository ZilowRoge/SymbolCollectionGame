using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeIndicator : SpellIndicator
{
    [SerializeField]
    [Range(0,360)]
    protected float angle;

    private void setConeAngle(float angle)
    {
        setShaderProperty("_Arc1", angle / 2);
        setShaderProperty("_Arc2", angle / 2);
    }

    public override void onValueChange()
    {
        base.onValueChange();
        setConeAngle(angle);
    }
}
