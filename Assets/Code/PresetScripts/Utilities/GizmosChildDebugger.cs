using UnityEditor;
using UnityEngine;

public class GizmosChildDebugger : MonoBehaviour
{
    [SerializeField] Color _color = Color.red;
    void OnDrawGizmos()
    {
        Gizmos.color = _color;
        foreach(Transform child in transform)
        {
            Gizmos.DrawLine(transform.position, child.position);
        }
    }
}
