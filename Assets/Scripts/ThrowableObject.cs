using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    [SerializeField] EmpathyObjectSO empathyObject;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        BehaviorManager behaviorManager = collision.gameObject.GetComponent<BehaviorManager>();

        if (behaviorManager != null)
        {
            behaviorManager.CheckEmpathyObject(empathyObject);
        }
    }
}
