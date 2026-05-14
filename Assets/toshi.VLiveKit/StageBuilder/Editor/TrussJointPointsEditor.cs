using UnityEngine;
using UnityEditor;

/// <summary>
/// Draws clickable joint cones for every TrussJointPoints component in the scene.
/// The first click selects a source joint; the second click rotates and moves the source object
/// so the selected joint faces and meets the target joint.
/// </summary>
[CustomEditor(typeof(TrussJointPoints))]
public class TrussJointPointsEditor : Editor
{
    private const float ConeSize = 0.2f;
    private Vector3? firstClickedConeDirection = null;
    private Transform firstClickedConeParent = null;
    private int? firstClickedJointIndex = null;
    private float colorIntensity = 0.8f;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    private void OnSceneGUI()
    {
        // Draw handles for every truss joint set so modules can be aligned across objects.
        foreach (TrussJointPoints tjp in FindObjectsOfType<TrussJointPoints>())
        {
            DrawConeForJoint(tjp);
        }
    }

    private void DrawConeForJoint(TrussJointPoints tjp)
    {
        // Cycle colors to make adjacent joints distinguishable in dense truss prefabs.
        Color[] colors = { Color.cyan, Color.magenta, Color.yellow, Color.red, Color.green, Color.blue };

        for (int i = 0; i < tjp.Joints.Count; i++)
        {
            TrussJointData joint = tjp.Joints[i];

            // Convert local joint data into world-space handle position and orientation.
            Vector3 worldJointPosition = tjp.transform.TransformPoint(joint.LocalJointPosition);
            Quaternion localJointRotation = Quaternion.Euler(joint.LocalJointRotationEuler);
            Quaternion worldJointRotation = tjp.transform.rotation * localJointRotation;

            Vector3 coneDirection = worldJointRotation * Vector3.forward;
            Vector3 normalizedConeDirection = Vector3.Normalize(coneDirection);
            // Debug.DrawRay(worldJointPosition, coneDirection, Color.red);

            Color coneColor = colors[i % colors.Length] * colorIntensity;
            Handles.color = (firstClickedConeParent == tjp.transform && firstClickedJointIndex == i) ? coneColor * 2 : coneColor;
            float coneSize = (firstClickedConeParent == tjp.transform && firstClickedJointIndex == i) ? ConeSize * 1.5f : ConeSize;

            if (Handles.Button(worldJointPosition, worldJointRotation, coneSize, coneSize, Handles.ConeHandleCap))
            {
                if (firstClickedConeDirection == null)
                {
                    // Remember the source joint direction and parent transform until a target joint is clicked.
                    firstClickedConeDirection = coneDirection;
                    firstClickedConeParent = tjp.transform;
                    firstClickedJointIndex = i;

                    Debug.Log("First clicked cone world position: " + worldJointPosition);
                }
                else
                {
                    Debug.Log("Second clicked cone world position: " + worldJointPosition);

                    // The target joint should face back toward the selected source joint.
                    Vector3 targetDirection = -coneDirection;
                    Quaternion rotation = Quaternion.FromToRotation(firstClickedConeDirection.Value, targetDirection);

                    // Rotate the source object, then compensate its position so the selected source joint lands on the target joint.
                    firstClickedConeParent.rotation = rotation * firstClickedConeParent.rotation;

                    if (firstClickedJointIndex.HasValue)
                    {
                        Debug.Log("firstClickedJointIndex: " + firstClickedJointIndex.Value);
                        Debug.Log("tjp.Joints.Count: " + tjp.Joints.Count);
                        Debug.Log("firstClickedConeParent.Joints.Count: " + firstClickedConeParent.GetComponent<TrussJointPoints>().Joints.Count);
                        Vector3 firstConePositionAfterRotation = firstClickedConeParent.GetComponent<TrussJointPoints>().Joints[firstClickedJointIndex.Value].LocalJointPosition;
                        Debug.Log("First cone position after rotation: " + firstConePositionAfterRotation);
                        Vector3 worldFirstConePositionAfterRotation = firstClickedConeParent.TransformPoint(firstConePositionAfterRotation);
                        Debug.Log("worldFirstConePositionAfterRotation: " + worldFirstConePositionAfterRotation);

                        Debug.Log("Parent position: " + firstClickedConeParent.position);

                        Vector3 difference = worldFirstConePositionAfterRotation - worldJointPosition;
                        Debug.Log("Difference: " + difference);
                        firstClickedConeParent.position -= difference;
                    }

                    firstClickedConeDirection = null;
                    firstClickedConeParent = null;
                    firstClickedJointIndex = null;
                }
            }
        }
    }
}
