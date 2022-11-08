using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JuicyButton : Button
{
    public event Action OnPointerDownEvent;
    public event Action OnPointerUpEvent;
    public event Action OnPointerEnterEvent;
    public event Action OnPointerExitEvent;
    public event Action OnPointerClickEvent;
    public event Action<bool> OnInteractableChanged;

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
