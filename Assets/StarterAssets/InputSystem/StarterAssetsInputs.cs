using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool scroll;
        public bool use;
        public bool drop;
        public bool attack;

        [Header("Movement Settings")]
        public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnScroll(InputValue value)
        {
            ScrollInput(value.isPressed);
        }

        public void OnUse(InputValue value)
        {
            UseInput(value.isPressed);
        }

        public void OnDrop(InputValue value)
        {
            DropInput(value.isPressed);
        }

        public void OnAttack(InputValue value)
        {
            AttackInput(value.isPressed);
        }
#else
 // old input sys if we do decide to have it (most likely wont)... 
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void AttackInput(bool attackState)
        {
            attack = attackState;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void ScrollInput(bool newScrollState)
        {
            scroll = newScrollState;
        }

        public void UseInput(bool newState)
        {
            use = newState;
        }

        public void DropInput(bool newState)
        {
            drop = newState;
        }

        public void AttackInput(bool newState)
        {
            attack = newState;
        }

#if !UNITY_IOS || !UNITY_ANDROID

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

#endif

    }

}