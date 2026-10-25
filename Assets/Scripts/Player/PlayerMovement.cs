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
    private float currentStamina;         // Текущее значение
    private bool isExhausted = false;     // Флаг полного истощения

    [Header("Настройки прыжка и приседания")]
    public float jumpHeight = 2f;         // Высота прыжка
    public float crouchHeight = 1f;       // Высота контроллера при приседании
    public float crouchSpeed = 2f;        // Скорость при приседании
    private float normalSpeed;            // Исходная скорость
    private float normalHeight;           // Исходный рост
    private float currentSpeed;           // Переменная для текущей скорости
    private bool isCrouching = false;     // флаг приседания

    void Start()
    {
        currentStamina = maxStamina;
        normalSpeed = speed;
        normalHeight = controller.height;
    }
    
    void Update()
    {
        float x = 0f;
        float z = 0f;

        // Прыжок
        if (controller.isGrounded)
        {
            if (velocity.y < 0) 
            {
                velocity.y = -2f; 
            }

            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !isCrouching)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        // Приседание
        if (Keyboard.current != null)
        {
            if (Keyboard.current.leftCtrlKey.isPressed)
            {
                controller.height = crouchHeight;
                speed = crouchSpeed;
                isCrouching = true; // Игрок присел
            }
            else
            {
                controller.height = normalHeight;
                speed = normalSpeed;
                isCrouching = false; // Игрок встал
            }
        }


        
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
