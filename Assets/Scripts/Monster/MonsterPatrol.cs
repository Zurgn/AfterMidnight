using UnityEngine;
using UnityEngine.AI; // Обязательно для работы с NavMesh

public class MonsterPatrol : MonoBehaviour
{
    [Header("Настройки патрулирования")]
    public Transform[] waypoints; // Массив точек патрулирования
    public float patrolSpeed = 3.5f;
    
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        // Если точки заданы, отправляем монстра к первой точке
        if (waypoints.Length > 0)
        {
            GotoNextWaypoint();
        }
    }

    void Update()
    {
        // Если точек нет или агент еще не дошел до первой — ничего не делаем
        if (waypoints.Length == 0) return;

        // Проверяем, подошел ли монстр близко к текущей точке
        // remainingDistance — оставшееся расстояние, stoppingDistance — радиус остановки агента
        if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance + 0.2f)
        {
            GotoNextWaypoint();
        }
    }

    void GotoNextWaypoint()
    {
        // Устанавливаем монстру следующую цель
        agent.destination = waypoints[currentWaypointIndex].position;

        // Выбираем следующую точку в массиве (по кругу)
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}
