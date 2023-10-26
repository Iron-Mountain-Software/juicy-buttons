using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace IronMountain.JuicyButtons
{
    public class JuicyButton : Button
    {
        public event Action OnPointerDownEvent;
        public event Action OnPointerUpEvent;
        public event Action OnPointerEnterEvent;
        public event Action OnPointerExitEvent;
        public event Action OnPointerClickEvent;
        public event Action<bool> OnInteractableChanged;

        public event Action<JuicySelectionState> OnSelectionStateChanged;
        
        public bool Interactable
        {
            get => interactable;
            set
            {
                if (value == Interactable) return;
                interactable = value;
                OnInteractableChanged?.Invoke(Interactable);
            }
        }

        public JuicySelectionState CurrentSelectionState
        {
            get
            {
                switch(currentSelectionState)
                {
                    case SelectionState.Normal:
                        return JuicySelectionState.Normal;
                    case SelectionState.Highlighted:
                        return JuicySelectionState.Highlighted;
                    case SelectionState.Pressed:
                        return JuicySelectionState.Pressed;
                    case SelectionState.Selected:
                        return JuicySelectionState.Selected;
                    case SelectionState.Disabled:
                        return JuicySelectionState.Disabled;
                    default:
                        return JuicySelectionState.Normal;
                }
            }
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);
            switch (state)
            {
                case SelectionState.Normal:
                    OnSelectionStateChanged?.Invoke(JuicySelectionState.Normal);
                    break;
                case SelectionState.Highlighted:
                    OnSelectionStateChanged?.Invoke(JuicySelectionState.Highlighted);
                    break;
                case SelectionState.Pressed:
                    OnSelectionStateChanged?.Invoke(JuicySelectionState.Pressed);
                    break;
                case SelectionState.Selected:
                    OnSelectionStateChanged?.Invoke(JuicySelectionState.Selected);
                    break;
                case SelectionState.Disabled:
                    OnSelectionStateChanged?.Invoke(JuicySelectionState.Disabled);
                    break;
            }
        }

        public override void OnPointerDown(PointerEventData eventData) {
            base.OnPointerDown(eventData);
            OnPointerDownEvent?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData) {
            base.OnPointerUp(eventData);
            OnPointerUpEvent?.Invoke();
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            base.OnPointerEnter(eventData);
            OnPointerEnterEvent?.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData) {
            base.OnPointerExit(eventData);
            OnPointerExitEvent?.Invoke();
        }

        public override void OnPointerClick(PointerEventData eventData) {
            base.OnPointerClick(eventData);
            OnPointerClickEvent?.Invoke();
        }
    }
}
