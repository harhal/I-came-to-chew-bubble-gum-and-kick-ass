using System.Collections.Generic;
using BubbleGumGuy;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class CharacterUiControls : MonoBehaviour
    {
        private BggActionInput _cachedBggActionInput;
        
        [SerializeField]
        List<Button> buttons = new List<Button>();
        
        BggActionInput GetInputComponent()
        {
            if (!_cachedBggActionInput)
            {
                _cachedBggActionInput = The.Me.GetComponent<BggActionInput>();
            }

            return _cachedBggActionInput;
        }

        void Start()
        {
            GetInputComponent().OnInputStageEntered += SetInputEnabled;
        }

        void SetInputEnabled()
        {
            foreach (var button in buttons)
            {
                button.interactable = true;
            }
        }

        void SetInputDisabled()
        {
            foreach (var button in buttons)
            {
                button.interactable = false;
            }
        }

        public void OnUpPressed()
        {
            SetInputDisabled();
            GetInputComponent().OnMove(Vector2.up);
        }

        public void OnDownPressed()
        {
            SetInputDisabled();
            GetInputComponent().OnMove(Vector2.down);
        }

        public void OnRightPressed()
        {
            SetInputDisabled();
            GetInputComponent().OnMove(Vector2.right);
        }

        public void OnLeftPressed()
        {
            SetInputDisabled();
            GetInputComponent().OnMove(Vector2.left);
        }

        public void OnAPressed()
        {
            SetInputDisabled();
            GetInputComponent().OnPop();
        }

        public void OnBPressed()
        {
            SetInputDisabled();
            GetInputComponent().OnAttack();
        }
    }
}
