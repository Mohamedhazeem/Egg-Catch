using UnityEngine;

[ExecuteAlways]
public class DebugDirectionLine : MonoBehaviour
{
    public Vector3 direction = Vector3.forward;
    public float length = 2f;
    public Color lineColor = Color.red;

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        Vector3 start = transform.position;
        Vector3 end = start + transform.TransformDirection(direction.normalized) * length;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.05f);
    }
#endif
}
