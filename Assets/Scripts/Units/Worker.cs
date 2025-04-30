using UnityEngine;
using UnityEngine.AI;


namespace GameDevTV.RTS.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Worker : MonoBehaviour
    {
        [SerializeField] Transform target;

        NavMeshAgent agent;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }


        void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }

        }
    }
}