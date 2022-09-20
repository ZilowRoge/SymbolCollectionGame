using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this script to Player or Enemy
public class CooldownSystem : MonoBehaviour
{
    private readonly List<CooldownData> cooldowns = new List<CooldownData>();

    private void Update() => processCooldowns();

    public void putOnCooldown(IHasCooldown cooldown)
    {
        cooldowns.Add(new CooldownData(cooldown));
    }

    public bool isOnCooldown(int id)
    {
        foreach (CooldownData cooldown in cooldowns)
        {
            if (cooldown.id == id) { return true; }
        }

        return false;
    }

    public float getRemainingDuration(int id)
    {
        foreach (CooldownData cooldown in cooldowns)
        {
            if (cooldown.id != id) { continue; }

            return cooldown.remining_time;
        }

        return 0f;
    }

    private void processCooldowns()
    {
        float delta_time = Time.deltaTime;

        for (int i = cooldowns.Count - 1; i >= 0; i--)
        {
            if (cooldowns[i].decrementCooldown(delta_time))
            {
                cooldowns.RemoveAt(i);
            }
        }
    }
}
