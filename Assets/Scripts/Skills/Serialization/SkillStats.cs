using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Serialization
{
    [System.Serializable]
    public class SkillDatabase
    {
        public SkillStats[] skills;
    }
    [System.Serializable]
    public class SkillStats
    {
        public int id;
        public string name;
        public string description;
        public float cooldown;
        public float duration;
        public float speed;
        public float damage;
        public float range;
        public float aoe_range;
        public int modificator_id;
        public float modificator_value;
        public float modificator_duration;

        public SkillStats(
            int id, string name, string description, 
            float cooldown, float duration, float speed, float damage, float range, float aoe_range,
            int modificator_id, float modificator_value, float modificator_duration)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.cooldown = cooldown;
            this.duration = duration;
            this.speed = speed;
            this.damage = damage;
            this.range = range;
            this.aoe_range = aoe_range;
            this.modificator_id = modificator_id;
            this.modificator_value = modificator_value;
            this.modificator_duration = modificator_duration;
        }

        public string getToolTip()
        {
            string stats = string.Empty;  //Resets the stats info
            string color = string.Empty;  //Resets the color info
            string newLine = string.Empty; //Resets the new line

            if (description != string.Empty) //Creates a newline if the item has a description, this is done to makes sure that the headline and the describion isn't on the same line
            {
                newLine = "\n";
            }

            //Returns the item info so that we can use it in the tooltip
            return string.Format(
                "<color=white><size=16>{0}</size></color>\n" +
                "<size=10><i><color=gray>" + newLine + "{1}</color></i></size>", name, description);

        }
    }

    //[System.Serializable]
    //public class ProjectileSkillStats : SkillStats
    //{
    //    public float speed;  
    //}

    //[System.Serializable]
    //public class AttackProjectileStats : ProjectileSkillStats
    //{
    //    public float damage;
    //    public float range;
    //}
}
