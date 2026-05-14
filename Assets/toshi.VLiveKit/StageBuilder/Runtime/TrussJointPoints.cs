using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores local-space attachment points for modular stage truss pieces.
/// Editor tooling reads these points to draw orientation cones and align two truss modules.
/// </summary>
public class TrussJointPoints : MonoBehaviour
{
    [SerializeField]
    public List<TrussJointData> Joints = new List<TrussJointData>();
}

/// <summary>
/// Serializable local transform data for a single truss connection point.
/// </summary>
[System.Serializable]
public class TrussJointData
{
    [SerializeField]
    public Vector3 LocalJointPosition;
    [SerializeField]
    public Vector3 LocalJointRotationEuler = Vector3.zero;

    public TrussJointData(Vector3 localJointPosition, Vector3 localJointRotationEuler)
    {
        LocalJointPosition = localJointPosition;
        LocalJointRotationEuler = localJointRotationEuler;
    }
}
