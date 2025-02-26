using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using GGG.Tool.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace zzz
{
    public class PlayerInputSystem : GGG.Tool.Singleton.Singleton<PlayerInputSystem>
    {
        public PlayerInputAction inputActions;

        protected override void Awake()
        {
            base.Awake();
            if (inputActions == null)
            {
                inputActions = new PlayerInputAction();
            }
        }

        private void OnEnable()
        {
            inputActions?.Enable();
        }

        private void OnDisable()
        {
            inputActions?.Disable();
        }

        public Vector2 PlayerMove
        {
            get => inputActions.Player.Movement.ReadValue<Vector2>();
        }

        public Vector2 Look
        {
            get => inputActions.Player.Look.ReadValue<Vector2>();
        }

        public bool Walk
        {
            get => inputActions.Player.Walk.triggered;
        }

        public bool LATK
        {
            get => inputActions.Player.LATK.triggered;
        }
    }
}
