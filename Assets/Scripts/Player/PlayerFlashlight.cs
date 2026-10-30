using UnityEngine;
using UnityEngine.InputSystem; 

public class Flashlight : MonoBehaviour
{
    [Header("Настройки фонарика")]
    public GameObject flashlightLight;
    
    private bool isOn = true;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            isOn = !isOn;
            
            if (flashlightLight != null)
            {
                flashlightLight.SetActive(isOn);
            }
        }
    }
}
