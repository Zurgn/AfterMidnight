using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public ScaryMonster monsterScript;
    
    private float timer;
    public float spawnInterval = 15f;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            monsterScript.SpawnBehindPlayer();
            timer = spawnInterval;
        }
    }
}
