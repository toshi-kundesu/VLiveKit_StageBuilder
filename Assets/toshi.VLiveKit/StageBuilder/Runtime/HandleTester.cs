using UnityEngine;

/// <summary>
/// SceneView-only helper component for testing snapped move and rotate handles on stage assets.
/// </summary>
public class HandleTester : MonoBehaviour
{
    [Tooltip("Rotation snap in degrees for each axis")]
    public Vector3 rotationSnap = new Vector3(30f, 30f, 30f);

    [Tooltip("Movement snap in units for each axis")]
    public Vector3 movementSnap = new Vector3(1f, 1f, 1f);
}
