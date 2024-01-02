
using System;
using UnityEngine;

public static class TransformExtension
{
    public static Transform RecursiveFindChild(this Transform parent, Func<Transform, bool> func)
    {
        if (parent == null) return null;
        if (func(parent))return parent;
        foreach (Transform child in parent)
        {
            Transform result = child.RecursiveFindChild(func);
            if (result != null) return result;
        }
        return null;
    }
}