using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;  // Дистанция взаимодействия
    float rayRadius = 0.2f;
    public TextMeshProUGUI promptTextObject;

    Color rayColor = Color.red; // Debug-Debug-Debug  Цвет луча при наведении на объект без функции взаимодействия

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool foundInteractable = false;

         rayColor = Color.red; // Debug-Debug-Debug

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

                if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
                {
                    interactable.Interact();
                }
            }
        }

        if (!foundInteractable)
        {
            if (promptTextObject != null)
            {
                promptTextObject.gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        Vector3 direction = transform.forward * interactionDistance;
        Gizmos.DrawRay(transform.position, direction);
        Gizmos.DrawWireSphere(transform.position + direction, rayRadius);
    }

}
