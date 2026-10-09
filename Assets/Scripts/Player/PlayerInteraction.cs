using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;  // Дистанция взаимодействия

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Color rayColor = Color.red; // Debug-Debug-Debug  Цвет луча при наведении на объект без функции взаимодействия

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                rayColor = Color.green; // Debug-Debug-Debug Цвет луча при наведении на объект с функцией взаимодействия
                if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
                {
                    interactable.Interact();
                }
            }
            else{}
        }
        Debug.DrawRay(ray.origin, ray.direction * interactionDistance, rayColor); // Debug-Debug-Debug Отрисовка луча
    }
}
