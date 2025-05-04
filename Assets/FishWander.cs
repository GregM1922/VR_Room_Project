using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWander : MonoBehaviour
{
    public Transform tankRoot;               // Assign the parent GameObject containing all tank colliders
    public float moveSpeed = 1.5f;
    public float directionChangeInterval = 1f; // Reduced pause between direction changes
    public Vector3 boundsInset = new Vector3(0.2f, 0.2f, 0.2f); // Padding to keep fish inside tank
    public float stopThreshold = 0.1f;        // Minimum movement distance before rotation stops

    private Bounds tankBounds;
    private Vector3 targetPosition;
    private float directionChangeTimer;

    void Start()
    {
        if (tankRoot == null)
        {
            Debug.LogError("FishWander: tankRoot is not assigned.");
            enabled = false;
            return;
        }

        ComputeTankBounds();
        PickNewTarget();
    }

    void Update()
    {
        directionChangeTimer -= Time.deltaTime;

        if (directionChangeTimer <= 0f || !tankBounds.Contains(targetPosition))
        {
            PickNewTarget();
        }

        // Calculate the direction towards the target
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);

        // Move the fish toward the target position
        if (distanceToTarget > stopThreshold)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Instantly rotate to face 90 degrees from the direction it's moving
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation * Quaternion.Euler(0f, 90f, 0f);
            }
        }
    }

    void ComputeTankBounds()
    {
        Collider[] colliders = tankRoot.GetComponentsInChildren<Collider>();
        if (colliders.Length == 0)
        {
            Debug.LogError("FishWander: No colliders found under tankRoot.");
            enabled = false;
            return;
        }

        tankBounds = colliders[0].bounds;
        foreach (Collider col in colliders)
        {
            tankBounds.Encapsulate(col.bounds);
        }

        tankBounds.Expand(-boundsInset * 2f); // Shrink bounds slightly to avoid edge clipping
    }

    void PickNewTarget()
    {
        targetPosition = new Vector3(
            Random.Range(tankBounds.min.x, tankBounds.max.x),
            Random.Range(tankBounds.min.y, tankBounds.max.y),
            Random.Range(tankBounds.min.z, tankBounds.max.z)
        );

        directionChangeTimer = directionChangeInterval; // Reset timer to shorter interval
    }
}
