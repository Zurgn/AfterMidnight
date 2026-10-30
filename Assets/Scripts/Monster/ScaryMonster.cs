using UnityEngine;
using UnityEngine.AI;

public class ScaryMonster : MonoBehaviour
{
    public Transform player;
    public Camera playerCamera;
    
    private NavMeshAgent agent;
    private bool hasBeenSeen = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null || playerCamera == null) return;

        if (IsVisibleByCamera())
        {
            hasBeenSeen = true;
        }

        if (hasBeenSeen)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Vector3 pointBehindPlayer = player.position - player.forward * 6f;
            agent.SetDestination(pointBehindPlayer);
        }
    }

    bool IsVisibleByCamera()
    {
        Vector3 screenPoint = playerCamera.WorldToViewportPoint(transform.position);
        
        bool inScreenBounds = screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
        bool inFrontOfCamera = screenPoint.z > 0;

        return inScreenBounds && inFrontOfCamera;
    }

    public void SpawnBehindPlayer()
    {
        hasBeenSeen = false;
        
        Vector3 spawnPos = player.position - player.forward * 5f;
        
        agent.Warp(spawnPos);
    }
}
