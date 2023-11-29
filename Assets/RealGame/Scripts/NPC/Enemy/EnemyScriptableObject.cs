
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[CreateAssetMenu]
public class EnemyScriptableObject : ScriptableObject
{
    //Enemy stats
    public int health = 100;

    //Enemy NavMeshAgent config
    public float AIUpdateInterval = 1f;
    public float acceleration = 8f;
    public float angularSpeed = 120f;
    public int areaMask = -1; // -1 means everything
    public int avoidancePriority = 50;
    public float baseOffset = 0;
    public float height = 2f;
    public ObstacleAvoidanceType obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
    public float radius = 0.5f;
    public float speed = 3f;
    public float stoppingDistance = 0.5f;



    public void SetUpEnemy(Enemy1 enemy)
    {
        enemy.navMeshAgent.acceleration = acceleration;
        enemy.navMeshAgent.angularSpeed = angularSpeed;
        enemy.navMeshAgent.areaMask = areaMask;
        enemy.navMeshAgent.avoidancePriority = avoidancePriority;
        enemy.navMeshAgent.baseOffset = baseOffset;
        enemy.navMeshAgent.height = height;
        enemy.navMeshAgent.speed = speed;
        enemy.navMeshAgent.stoppingDistance = stoppingDistance;
        enemy.health = health;
        
    }
}
