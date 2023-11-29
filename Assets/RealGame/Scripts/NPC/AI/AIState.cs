using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStateID
{
    Idle,
    Attacking,
    ChasePlayer
}
public interface AIState 
{
    AIStateID GetID();
    void Enter(AIAgent agennt);
    void Update(AIAgent agent);  
    void Exit(AIAgent agennt);


    
}
