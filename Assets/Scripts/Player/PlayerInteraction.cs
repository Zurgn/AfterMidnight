using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;  // Дистанция взаимодействия
    public TextMeshProUGUI promptTextObject;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Color rayColor = Color.red; // Debug-Debug-Debug  Цвет луча при наведении на объект без функции взаимодействия
        bool foundInteractable = false;
        float rayRadius = 0.2f;

        if (Physics.SphereCast(ray.origin, rayRadius, ray.direction, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                rayColor = Color.green; // Debug-Debug-Debug Цвет луча при наведении на объект с функцией взаимодействия
                foundInteractable = true;
            
                if (promptTextObject != null)
                {
                    promptTextObject.gameObject.SetActive(true);
                    promptTextObject.text = interactable.GetInteractionPrompt();
                }

                if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
                {
                    interactable.Interact();
                }
            }
        }

        if (!foundInteractable)
            {
                if (promptTextObject != null)
                {
                    promptTextObject.gameObject.SetActive(false); // Выключаем текст
                }
            }

        Debug.DrawRay(ray.origin, ray.direction * interactionDistance, rayColor); // Debug-Debug-Debug Отрисовка луча
    }
}
