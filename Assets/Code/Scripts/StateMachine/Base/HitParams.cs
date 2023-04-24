using UnityEngine;
public enum Element
{
    None, Fire, Ice, Lightning, Earth, Wind, Water, Light, Dark
}
public class HitParams
{
    public float Damage;
    public float Force = 0;
    public Vector3 Direction = Vector3.zero;
    public float Duration = 0;
    public Element Element = Element.None;

    public Vector3 ForceDirection = Vector3.zero;

    // Normal
    public HitParams(float damage, float force, Vector3 direction)
    {
        Damage = damage;
        Force = force;
        Direction = direction; ForceDirection = force*direction;
    }
    public HitParams(float damage, Vector3 forceDirection)
    {
        Damage = damage;
        ForceDirection = forceDirection;
    }



    // No duration
    public HitParams(float damage, float force, Vector3 direction, Element element)
    {
        Damage = damage;
        Force = force;
        Direction = direction; ForceDirection = force*direction;
        Element = element;
    }
    public HitParams(float damage, Vector3 forceDirection, Element element)
    {
        Damage = damage;
        ForceDirection = forceDirection;
        Element = element;
    }


    // No Element
    public HitParams(float damage, float force, Vector3 direction, float duration)
    {
        Damage = damage;
        Force = force;
        Direction = direction; ForceDirection = force*direction;
        Duration = duration;
    }
    public HitParams(float damage, Vector3 forceDirection, float duration)
    {
        Damage = damage;
        ForceDirection = forceDirection;
        Duration = duration;
    }



    // Everything
    public HitParams(float damage, float force, Vector3 direction, Element element, float duration)
    {
        Damage = damage;
        Force = force;
        Direction = direction; ForceDirection = force*direction;
        Element = element;
        Duration = duration;
    }
    public HitParams(float damage, Vector3 forceDirection, Element element, float duration)
    {
        Damage = damage;
        ForceDirection = forceDirection;
        Element = element;
        Duration = duration;
    }
}
