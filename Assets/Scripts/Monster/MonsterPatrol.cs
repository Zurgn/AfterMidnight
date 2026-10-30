using UnityEngine;
using UnityEngine.AI;

public class MonsterPatrol : MonoBehaviour
{
    [Header("Настройки патрулирования")]
    public Transform[] waypoints;
    public float patrolSpeed = 3.5f;
    
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        if (waypoints.Length > 0)
        {
            GotoNextWaypoint();
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance + 0.2f)
        {
            GotoNextWaypoint();
        }
    }

    void GotoNextWaypoint()
    {
        agent.destination = waypoints[currentWaypointIndex].position;

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
