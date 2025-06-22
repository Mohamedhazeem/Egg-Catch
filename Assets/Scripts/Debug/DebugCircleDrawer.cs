using UnityEngine;
using UnityEditor;
[ExecuteAlways]
public class DebugCircleDrawer : MonoBehaviour
{
    public float radius = 1f;
    public Color fillColor;

    private void OnDrawGizmos()
    {
        if (SceneView.lastActiveSceneView == null)
            return;

        Camera sceneCam = SceneView.lastActiveSceneView.camera;
        if (sceneCam == null)
            return;
        Vector3 camForward = sceneCam.transform.forward;

        Handles.color = fillColor;
        Handles.DrawSolidDisc(transform.position, camForward, radius);
    }
}
