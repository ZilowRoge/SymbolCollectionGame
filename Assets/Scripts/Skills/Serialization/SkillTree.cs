using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Serialization
{
    [System.Serializable]
    public enum SkillActivityState
    {
        INACTIVE,
        ACTIVE,
        UNLOCKED
    }

    [System.Serializable]
    public class SkillTree
    {
        public SkillTreeStats[] skills;
    }

    [System.Serializable]
    public class SkillTreeStats
    {
        public int id;
        public int[] dependencies;
        public int[] ancestors;
        public int state;
        public int level;
    }
}
