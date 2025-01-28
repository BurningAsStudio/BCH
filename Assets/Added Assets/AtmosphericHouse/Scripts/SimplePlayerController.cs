using UnityEngine;

namespace BCH
{
    [RequireComponent(typeof(CharacterController))]
    public class SimplePlayerController : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;

        public Camera playerCamera;
        public float walkSpeed = 1.15f;
        public float runSpeed = 4.0f;
        public float lookSpeed = 2.0f;
        public float lookXLimit = 60.0f;
        public float gravity = 150.0f;

        private CharacterController _characterController;
        private Vector3 _moveDirection = Vector3.zero;
        private float _rotationX = 0;
        private bool _canMove = true;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            // Получаем направление движения
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            // Проверяем, используется ли джойстик
            bool isJoystickActive = _joystick.Direction != Vector2.zero;

            // Скорость движения (пешком или бегом)
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float moveSpeed = isRunning ? runSpeed : walkSpeed;

            // Вычисляем ввод для движения
            float inputVertical = isJoystickActive ? _joystick.Vertical : Input.GetAxis("Vertical");
            float inputHorizontal = isJoystickActive ? _joystick.Horizontal : Input.GetAxis("Horizontal");

            // Рассчитываем движение
            float movementDirectionY = _moveDirection.y; // сохраняем вертикальное направление
            _moveDirection = (forward * inputVertical + right * inputHorizontal) * moveSpeed;
            _moveDirection.y = movementDirectionY;

            // Добавляем гравитацию
            if (!_characterController.isGrounded)
            {
                _moveDirection.y -= gravity * Time.deltaTime;
            }

            // Перемещение игрока
            _characterController.Move(_moveDirection * Time.deltaTime);

            // Обработка вращения
            if (_canMove)
            {
                if (isJoystickActive)
                {
                    // Управление вращением с джойстика
                    transform.rotation *= Quaternion.Euler(0, _joystick.Horizontal * lookSpeed, 0);
                }
                else
                {
                    // Управление мышью
                    _rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                    _rotationX = Mathf.Clamp(_rotationX, -lookXLimit, lookXLimit);
                    playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
                    transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
                }
            }
        }
    }
}
