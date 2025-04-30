using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;


namespace GameDevTV.RTS.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Worker : MonoBehaviour, ISelectable
    {
        [SerializeField] Transform target;
        [SerializeField] DecalProjector decalProjector;

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


        public void Deselect()
        {
            if (decalProjector != null)
            {
                decalProjector.gameObject.SetActive(false);
            }
        }


        public void Select()
        {
            Debug.Log("HERE");
            if (decalProjector != null)
            {
                decalProjector.gameObject.SetActive(true);
            }
        }
    }
}