using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using IAC = Utilities.StepGame.StepGameInputActionsCollection;

namespace Utilities.StepGame.Character
{
    public class StepGamePlayerController : MonoBehaviour
    {
        private void OnEnable()
        {
            InputOnEnable();
        }

        private void OnDisable()
        {
            InputOnDisable();
        }

        #region Character Actions

        private Queue<CharacterAction> actionQueue = new Queue<CharacterAction>();


        public void EnqueueAction(CharacterAction action)
        {
            actionQueue.Enqueue(action);
            _ = RunQueue();
        }

        private bool isRunning = false;

        private CharacterAction currentAction = null;

        private async Task RunQueue()
        {
            if (isRunning) return; // Prevent overlapping runners
            isRunning = true;

            while (actionQueue.Count > 0)
            {
                CharacterAction action = actionQueue.Dequeue();
                currentAction = action;
                await action.Run(this);
                currentAction = null;
            }

            isRunning = false;
        }

        public bool HasActionOfType<T>() where T : CharacterAction
        {
            if (currentAction is T) return true;

            foreach (var action in actionQueue)
            {
                if (action is T)
                    return true;
            }
            return false;
        }

        #endregion

        #region UserInput

        #region Setup Input

        //Setup input bindings on enable
        private void InputOnEnable()
        {
            IAC.LeftClick.started += OnLeftClickPressed;
            IAC.RightClick.started += OnRightClickPressed;
        }

        //Setup input bindings on enable
        private void InputOnDisable()
        {
            IAC.LeftClick.started -= OnLeftClickPressed;
            IAC.RightClick.started -= OnRightClickPressed;
        }
        #endregion

        void OnLeftClickPressed(InputAction.CallbackContext obj)
        {
            StepTowardMouse();
        }
        void OnRightClickPressed(InputAction.CallbackContext obj)
        {
            return;
        }

        public Vector3 MousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }

        #endregion

        void StepTowardMouse()
        {
            Vector3 playerPosition = gameObject.transform.position;
            playerPosition.z = 0;
            Vector3 mousePosition = MousePosition();
            mousePosition.z = 0;
            Vector3 mouseDirection = (mousePosition - playerPosition).normalized;
            float distance = Mathf.Min(1.5f, (mousePosition - playerPosition).magnitude);
            //StartStep(mouseDirection, distance);
        }

        //void StartStep(Vector3 direction, float distance)
        //{
        //    if (!HasActionOfType<StepAction>())
        //    {
        //        StepAction stepAction = new StepAction(direction, distance, 5);
        //        EnqueueAction(stepAction);
        //    }
        //}
    }
}
