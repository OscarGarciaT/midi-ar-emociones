using Niantic.Lightship.AR.NavigationMesh;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    [SerializeField] private LightshipNavMeshAgent agent;
    [SerializeField] private Animator animator;

    [SerializeField] private float rotationSpeed = 5.0f;

    void Update()
    {
        if (agent.State == LightshipNavMeshAgent.AgentNavigationState.Idle)
        {
            animator.SetBool("isWalking", false);

            // Get the direction to the main camera
            Vector3 direction = Camera.main.transform.position - transform.position;
            direction.y = 0; // Keep only the horizontal direction

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else if (agent.State == LightshipNavMeshAgent.AgentNavigationState.HasPath)
        {
            animator.SetBool("isWalking", true);
        }
    }
}
