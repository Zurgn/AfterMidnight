using UnityEngine;

public interface IInteractable
{
    string GetInteractionPrompt(); // Текст-подсказка
    void Interact();               // Действие
}

