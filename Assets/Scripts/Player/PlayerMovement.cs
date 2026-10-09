using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{  
    public CharacterController controller;
    private Vector3 velocity;

    public float gravity = -9.81f;

    [Header("Run Settings")]
    public float speed = 5f;              // Скорость
    public float sprint = 1.5f;           // Множитель ускорения
    public float maxStamina = 100f;       // Максимальная стамина
    public float staminaDrain = 20f;      // Расход в секунду при беге
    public float staminaRegen = 15f;      // Восстановление в секунду
    private float currentStamina;          // Текущее значение
    private bool isExhausted = false;      // Флаг полного истощения

    void Start()
    {
        currentStamina = maxStamina;
    }
    
    void Update()
    {
        float x = 0f;
        float z = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) z = 1f;    // Вперёд
            if (Keyboard.current.sKey.isPressed) z = -1f;   // Назад
            if (Keyboard.current.aKey.isPressed) x = -1f;   // Влево
            if (Keyboard.current.dKey.isPressed) x = 1f;    // Вправо
        }

        Vector3 move = transform.right * x + transform.forward * z;

        // Исключение ускорения персонажи при движении по диагонали
        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

       float currentSpeed = speed;

        // Регулирование восстановления сил при полном истощении
        if (currentStamina <= 0)
        {
            isExhausted = true;
        }

        if (isExhausted && currentStamina >= maxStamina)
        {
            isExhausted = false;
        }

        bool isSprinting = Keyboard.current != null && 
                        Keyboard.current.leftShiftKey.isPressed && 
                        move.magnitude > 0.1f && 
                        !isExhausted;

        if (isSprinting)
        {
            currentSpeed *= sprint;
            currentStamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegen * Time.deltaTime;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Обработка гравитации
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
