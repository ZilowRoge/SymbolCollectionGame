using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoints
{
    public float left;
    public float right;
    public float up;
    public float down;
    public EndPoints()
    {
        left = Mathf.Infinity;
        right = Mathf.NegativeInfinity;
        up = Mathf.NegativeInfinity;
        down = Mathf.Infinity;
    }

    public string toString()
    {
        return "x = (" + left + ", " + right + ") y = (" + down + ", " + up + ")";
    }
}
