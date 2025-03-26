using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClickCharactercontroller : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray;
            RaycastHit hit;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                _agent.SetDestination(hit.point);
            }

        }
        _animator.SetFloat("_speed", _agent.velocity.magnitude/_agent.speed);
    }
}
