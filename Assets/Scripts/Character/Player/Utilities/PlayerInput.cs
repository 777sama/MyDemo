using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerInput : MonoBehaviour
    {
       public PlayerInputAction InputActions { get; private set; }
       public PlayerInputAction.PlayerActions PlayerActions { get; private set; }

        private void Awake()
        {
            InputActions = new PlayerInputAction();

            PlayerActions = InputActions.Player;
        }

        private void OnEnable()
        {
            InputActions.Enable();
        }

        private void OnDisable()
        {
            InputActions.Disable();
        }

        public void DisableActionFor(InputAction action,float seconds)
        {
            StartCoroutine(DisableAction(action, seconds));
        }

        private IEnumerator DisableAction(InputAction action,float seconds)
        {
            action.Disable();
            yield return new WaitForSeconds(seconds); ;
            action.Enable();
        }
    }
}
