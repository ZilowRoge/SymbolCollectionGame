using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasCooldown
{
    int id { get; }
    float cooldown_duration { get; }
}

public class CooldownData
{
    public CooldownData(IHasCooldown cooldown)
    {
        id = cooldown.id;
        remining_time = cooldown.cooldown_duration;
    }

    public int id { get; }
    public float remining_time { get; private set; }

    public bool decrementCooldown(float delta_time)
    {
        remining_time = -delta_time;

        return remining_time < 0f;
    }
}
