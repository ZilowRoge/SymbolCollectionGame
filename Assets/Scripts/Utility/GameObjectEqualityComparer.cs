using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class GameObjectEqualityComparer : IEqualityComparer<GameObject>
    {
        public bool Equals(GameObject x, GameObject y)
        {
            return x.GetInstanceID() == y.GetInstanceID();
        }

        public int GetHashCode(GameObject obj)
        {
            return obj.GetHashCode();
        }
    }
}
