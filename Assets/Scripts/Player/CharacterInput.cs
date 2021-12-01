using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterInput : MonoBehaviour
    {
        private PlayerInput playerInput = null;

        private IRotable iRotable = null;
        private IMoveable iMoveable = null;

        public bool IsAiming { get; private set; } = false;
        public Vector2 AimDirection { get; private set; } = Vector2.zero;

        void Start()
        {
            playerInput = GetComponent<PlayerInput>();

            iRotable = GetComponent<IRotable>();
            iMoveable = GetComponent<IMoveable>();
        }

        private void FixedUpdate()
        {
            AimInput();
            MoveInput();
        }

        /// <summary>
        /// Read the right Joystick value and calls the method in charge of rotate Character
        /// </summary>
        void AimInput()
        {
            AimDirection = playerInput.actions["aim"].ReadValue<Vector2>();
            IsAiming = AimDirection.magnitude > 0.0f;
            iRotable.Rotate(AimDirection);
        }

        /// <summary>
        /// Read the left Joystick value and calls the method in charge of moving Character
        /// </summary>
        void MoveInput()
        {
            Vector2 moveDirection = playerInput.actions["move"].ReadValue<Vector2>();
            iMoveable.Move(moveDirection, IsAiming);
        }
    }
}
