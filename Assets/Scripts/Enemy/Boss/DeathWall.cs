using UnityEngine;


public class DeathWall : MonoBehaviour
{
    
    public Transform point1; 
    public Transform point2; 
    public float moveSpeed = 2f; 
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = point1.position; // Start by moving towards point1
    }

    void Update()
    {
        // Move the wall towards the current target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the wall has reached the current target
        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Switch target to the other point
            if (targetPosition == point1.position)
            {
                targetPosition = point2.position;
            }
            else
            {
                targetPosition = point1.position;
            }
        }
    }
}
