using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public enum PhysicsType
    {
        Raycast,
        OverlapSphere,
        OverlapCapsule,
        OverlapBox
    }

    [Header("Raycast Settings")]
    public PhysicsType debugPhysicsType;
    public float raycastDistance = 10f;
    public Color rayColor = Color.red;

    

    [Header("OverlapSphere Settings")]
    public float overlapSphereRadius = 1f;
    public Color overlapSphereColor = Color.yellow;

    [Header("OverlapCapsule Settings")]
    public float overlapCapsuleRadius = 1f;
    public float overlapCapsuleHeight = 2f;
    public Color overlapCapsuleColor = Color.magenta;

    [Header("OverlapBox Settings")]
    public Vector3 boxSize;
    public Vector3 overlapBoxSize = Vector3.one;
    public Color overlapBoxColor = Color.gray;

    public void DamageCast()
    {
        switch (debugPhysicsType)
        {
            case PhysicsType.OverlapSphere:
                DrawOverlapSphere();
                break;
            case PhysicsType.OverlapCapsule:
                DrawOverlapCapsule();
                break;
            case PhysicsType.OverlapBox:
                DrawOverlapBox();
                break;
        }
    }

    void OnDrawGizmos()
    {
        switch (debugPhysicsType)
        {

            case PhysicsType.OverlapSphere:
                DrawOverlapSphere();
                break;
            case PhysicsType.OverlapCapsule:
                DrawOverlapCapsule();
                break;
            case PhysicsType.OverlapBox:
                DrawOverlapBox();
                break;
        }
    }

   

    void DrawOverlapSphere()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, overlapSphereRadius);
        foreach (var collider in colliders)
        {
            Debug.Log("Overlap detected: " + collider.gameObject.name, collider.gameObject);
        }

        Gizmos.color = overlapSphereColor;
        Gizmos.DrawWireSphere(transform.position, overlapSphereRadius);
    }

    void DrawOverlapCapsule()
    {
        Collider[] colliders = Physics.OverlapCapsule(transform.position - Vector3.up * overlapCapsuleHeight * 0.5f,
            transform.position + Vector3.up * overlapCapsuleHeight * 0.5f, overlapCapsuleRadius);
        foreach (var collider in colliders)
        {
            Debug.Log("Overlap detected: " + collider.gameObject.name, collider.gameObject);
        }

        Gizmos.color = overlapCapsuleColor;
        Gizmos.DrawWireSphere(transform.position - Vector3.up * overlapCapsuleHeight * 0.5f, overlapCapsuleRadius);
        Gizmos.DrawWireSphere(transform.position + Vector3.up * overlapCapsuleHeight * 0.5f, overlapCapsuleRadius);
    }

    void DrawOverlapBox()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize * 0.5f);
        foreach (var collider in colliders)
        {
            Debug.Log("Overlap detected: " + collider.gameObject.name, collider.gameObject);
        }

        Gizmos.color = overlapBoxColor;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
