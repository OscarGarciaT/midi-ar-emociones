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

            Vector3 direction = Camera.main.transform.position - transform.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else if (agent.State == LightshipNavMeshAgent.AgentNavigationState.HasPath)
        {
            animator.SetBool("isWalking", true);
        }
    }
}
