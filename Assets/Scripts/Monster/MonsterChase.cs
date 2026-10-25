using UnityEngine;
using UnityEngine.AI;

public class MonsterChase : MonoBehaviour
{
    [Header("Vision settings")]
    public float viewRadius = 8f;        // Дистанция зрения
    [Range(0, 360)]
    public float viewAngle = 90f;        // Угол обзора в градусах
    [Header("layer settings")]
    public LayerMask obstacleMask;       // Слой стен
    private NavMeshAgent agent;
    private Transform player;
    private bool isChasing = false;
    public float chaseLoseTimeout = 3.0f; // Сколько секунд монстр помнит игрока после потери из виду
    private float loseTimer = 0f;         // Внутренний счетчик времени


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
{
    if (CanSeePlayer())
    {
        isChasing = true;
        loseTimer = chaseLoseTimeout; // Сбрасываем таймер на максимум, пока видим игрока

        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
    else
    {
        // Если игрок скрылся, но монстр еще находится в режиме погони
        if (isChasing)
        {
            loseTimer -= Time.deltaTime; // Уменьшаем таймер каждую секунду

            if (player != null)
            {
                // Монстр продолжает бежать за игроком, пока тикает таймер
                agent.SetDestination(player.position); 
            }

            // Когда время вышло — прекращаем погоню
            if (loseTimer <= 0)
            {
                isChasing = false;
                agent.ResetPath();
            }
        }
    }
}


    bool CanSeePlayer()
    {
        if (player == null) return false;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= viewRadius)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
            {
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);

        if (player != null)
        {
            Gizmos.color = isChasing ? Color.green : Color.gray;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }

    Vector3 DirFromAngle(float angleInDegrees)
    {
        angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
