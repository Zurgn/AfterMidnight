using UnityEngine;
using UnityEngine.AI;

public class ScaryMonster : MonoBehaviour
{
    public Transform player;          // Сюда перетащим игрока
    public Camera playerCamera;      // Сюда перетащим камеру игрока
    
    private NavMeshAgent agent;
    private bool hasBeenSeen = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null || playerCamera == null) return;

        // 1. Проверяем, видит ли камера монстра
        if (IsVisibleByCamera())
        {
            hasBeenSeen = true; // Игрок развернулся и увидел монстра!
        }

        // 2. Логика движения
        if (hasBeenSeen)
        {
            // Монстр идет прямо на игрока
            agent.SetDestination(player.position);
        }
        else
        {
            // Монстр идет за игроком, но держится чуть позади (не атакует напрямую)
            // Он пытается прийти в точку «за спиной игрока» на расстоянии 3 метров
            Vector3 pointBehindPlayer = player.position - player.forward * 3f;
            agent.SetDestination(pointBehindPlayer);
        }
    }

    // Метод проверяет, попадает ли монстр в экран (в поле зрения камеры)
    bool IsVisibleByCamera()
    {
        Vector3 screenPoint = playerCamera.WorldToViewportPoint(transform.position);
        
        // Проверяем, находится ли объект внутри границ экрана по X и Y, и находится ли он ПЕРЕД камерой (Z > 0)
        bool inScreenBounds = screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
        bool inFrontOfCamera = screenPoint.z > 0;

        return inScreenBounds && inFrontOfCamera;
    }

    // Метод для вызова «появления за спиной» из другого скрипта или по таймеру
    public void SpawnBehindPlayer()
    {
        hasBeenSeen = false; // Сбрасываем агрессию
        
        // Находим точку строго за спиной игрока на расстоянии 5 метров
        Vector3 spawnPos = player.position - player.forward * 5f;
        
        // Телепортируем NavMeshAgent в эту точку
        agent.Warp(spawnPos);
        
        Debug.Log("Монстр крадется сзади...");
    }
}
