using UnityEngine;
// Обязательно добавляем эту строку для работы новой системы ввода:
using UnityEngine.InputSystem; 

public class Flashlight : MonoBehaviour
{
    [Header("Настройки фонарика")]
    public GameObject flashlightLight; // Сюда перетаскиваем Spot Light
    
    private bool isOn = true;

    void Start()
    {
        // Проверка на забытый объект в инспекторе
        if (flashlightLight == null)
        {
            Debug.LogError("Ошибка: Перетащите ваш Spot Light в поле Flashlight Light!");
        }
    }

    void Update()
    {
        // Проверяем нажатие клавиши F в новой системе ввода
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log("Новая система ввода: Клавиша F нажата!");
            isOn = !isOn;
            
            if (flashlightLight != null)
            {
                flashlightLight.SetActive(isOn);
            }
        }
    }
}
