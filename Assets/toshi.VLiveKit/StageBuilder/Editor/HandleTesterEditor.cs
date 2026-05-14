using UnityEngine;
using UnityEditor;

/// <summary>
/// Draws custom SceneView transform handles for HandleTester and applies movement/rotation snapping.
/// </summary>
[CustomEditor(typeof(HandleTester))]
public class HandleTesterEditor : Editor
{
    private Vector3 lastRotation;

    private float handleSize = 1.2f;
    private float colorIntensity = 0.9f;

    void OnSceneGUI()
    {
        Tools.hidden = true;

        HandleTester t = (HandleTester)target;
        EditorGUI.BeginChangeCheck();
        Vector3 axisX = t.transform.right;
        Vector3 axisY = t.transform.up;
        Vector3 axisZ = t.transform.forward;

        Handles.color = Color.red * colorIntensity;
        Quaternion newRotationX = Handles.Disc(t.transform.rotation, t.transform.position, axisX, handleSize, false, 1.0f);

        Handles.color = Color.green * colorIntensity;
        Quaternion newRotationY = Handles.Disc(t.transform.rotation, t.transform.position, axisY, handleSize, false, 1.0f);

        Handles.color = Color.blue * colorIntensity;
        Quaternion newRotationZ = Handles.Disc(t.transform.rotation, t.transform.position, axisZ, handleSize, false, 1.0f);

        Handles.color = Color.cyan * colorIntensity;
        Vector3 newPositionX = Handles.Slider(t.transform.position, Vector3.right, handleSize, Handles.ArrowHandleCap, t.movementSnap.x);
        Vector3 newPositionY = Handles.Slider(t.transform.position, Vector3.up, handleSize, Handles.ArrowHandleCap, t.movementSnap.y);
        Vector3 newPositionZ = Handles.Slider(t.transform.position, Vector3.forward, handleSize, Handles.ArrowHandleCap, t.movementSnap.z);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(t.transform, "Transform Change");
            // Each axis uses the matching disc's Euler delta so the tester can validate independent snap values.
            Vector3 rotationChange = new Vector3(
                Mathf.Round((newRotationX.eulerAngles.x - lastRotation.x) / t.rotationSnap.x) * t.rotationSnap.x,
                Mathf.Round((newRotationY.eulerAngles.y - lastRotation.y) / t.rotationSnap.y) * t.rotationSnap.y,
                Mathf.Round((newRotationZ.eulerAngles.z - lastRotation.z) / t.rotationSnap.z) * t.rotationSnap.z
            );
            t.transform.rotation = Quaternion.Euler(t.transform.rotation.eulerAngles + rotationChange);
            Vector3 positionInUnits = new Vector3(
                Mathf.Round((newPositionX - t.transform.position).x / t.movementSnap.x) * t.movementSnap.x,
                Mathf.Round((newPositionY - t.transform.position).y / t.movementSnap.y) * t.movementSnap.y,
                Mathf.Round((newPositionZ - t.transform.position).z / t.movementSnap.z) * t.movementSnap.z
            );
            t.transform.position = t.transform.position + positionInUnits;
        }
        lastRotation = t.transform.rotation.eulerAngles;

    }
}
