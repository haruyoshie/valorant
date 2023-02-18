using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator anim;

    public Vector3 walkPoint; 
    public bool walkPointSet;
    public float walkPointRange;

    private float _animationBlend;
    private int _animIDSpeed;
    private bool _hasAnimator;
    public float SpeedChangeRate = 10.0f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
    } 
    private void Start()
	{
		_hasAnimator = TryGetComponent(out anim);
        AssignAnimationIDs();
	}

    private void Update()
    {
        _hasAnimator = TryGetComponent(out anim);
       Patroling();
    }
    private void AssignAnimationIDs()
	{
			_animIDSpeed = Animator.StringToHash("Speed");
	}

    private void Patroling()
    {
        if(!walkPointSet)SearchWalkPoint();
        _animationBlend = Mathf.Lerp(_animationBlend, agent.speed, Time.deltaTime * SpeedChangeRate);


        if(walkPointSet)
            agent.SetDestination(walkPoint);
            anim.SetBool("isWalking", true);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 0.5f)
           walkPointSet = false;
        
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;     
    }
    
}