using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Utility
{
    class CooldownDataComparer : IEqualityComparer<CooldownData>
    {
        public bool Equals(CooldownData x, CooldownData y)
        {
            return x.id == y.id;
        }

        public int GetHashCode(CooldownData obj)
        {
            return obj.id.GetHashCode();
        }
    }
}
