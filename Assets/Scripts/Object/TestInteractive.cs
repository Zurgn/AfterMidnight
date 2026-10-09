using UnityEngine;

public class TestCube : MonoBehaviour, IInteractable
{
    public string GetInteractionPrompt() => "Открыть сундук [F]";

    public void Interact()
    {
        Debug.Log("Дожили, кубики лапают...");
    }
}
