
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChaseState : AIState
{
    public Transform player;
    
    public float timer;
    public float maxDistance = 1f;
    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;

    }
    public void Enter(AIAgent agent)
    {
       if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
    }
    public void Update(AIAgent agent)
    {
        if (!agent.enabled) return;
        timer = -Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
            agent.navMeshAgent.destination = player.position;
        if (timer < 0.0f)
        {
            Vector3 direction = (player.transform.position - agent.navMeshAgent.destination);
            direction.y = 0;
            if(direction.sqrMagnitude > maxDistance* maxDistance)
            {
                //if(agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                //{
                    agent.navMeshAgent.destination = player.position;
                //}
            }
            timer = agent.config.AIUpdateInterval;


        }
        
    }

    public void Exit(AIAgent agennt)
    {
        
    }

    

    

   
   
}
