using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class Extensions
    {
        public static T PickRandomItem<T>(this List<T> list) where T : Object
        {
            if (list == null) return null;
            
            return list.Count switch
            {
                0 => null,
                1 => list[0],
                _ => list[Random.Range(0, list.Count)]
            };
        }
    }
}