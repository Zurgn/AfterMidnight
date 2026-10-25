using UnityEngine;
using UnityEngine.SceneManagement; // Нужно для перезагрузки сцены

public class MonsterKill : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            KillPlayer(collision.gameObject);
        }
    }

    private void KillPlayer(GameObject player)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
