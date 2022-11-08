using System;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Shadow))]
[RequireComponent(typeof(JuicyButton))]
public class JuicyButton3DEffect : MonoBehaviour
{
    private enum HeightType
    {
        ScreenPercent,
        RectPercent
    }

    [Header("Settings")]
    [SerializeField] private HeightType shiftType = HeightType.ScreenPercent;
    [SerializeField] [Range(0, .25f)] private float heightPercent = .01f;
    [SerializeField] private bool downWhenDisabled = true;

    [Header("States")]
    private bool _pressed;

    [Header("Cache")]
    private RectTransform _rectTransform;
    private Image _image;
    private Shadow _shadow;
    private JuicyButton _button;
    private float _slicedMultiplier;
    
    private bool IsSlicedImage => _image != null && _image.type == Image.Type.Sliced;

    private float HeightPixels
    {
        get
        {
            switch (shiftType)
            {
                case HeightType.ScreenPercent:
                    return Screen.height * heightPercent;
                case HeightType.RectPercent:
                    return RectTransform.rect.height * heightPercent;
            }

            return 0f;
        }
    }

    private RectTransform RectTransform
    {
        get
        {
            if (!_rectTransform) _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }
    
    private Shadow Shadow
    {
        get
        {
            if (!_shadow) _shadow = GetComponent<Shadow>();
            return _shadow;
        }
    }

    private void OnEnable()
    {
        _button = GetComponent<JuicyButton>();
        _image = GetComponent<Image>();
        if (IsSlicedImage) _slicedMultiplier = _image.pixelsPerUnitMultiplier;
        _button.OnPointerDownEvent += OnPointerDown;
        _button.OnPointerUpEvent += OnPointerUp;
        _button.OnPointerEnterEvent += OnPointerEnter;
        _button.OnPointerExitEvent += OnPointerExit;
        _button.OnInteractableChanged += OnInteractableChange;
        OnInteractableChange(_button.interactable);
    }

    private void OnDisable()
    {
        _button.OnPointerDownEvent -= OnPointerDown;
        _button.OnPointerUpEvent -= OnPointerUp;
        _button.OnPointerEnterEvent -= OnPointerEnter;
        _button.OnPointerExitEvent -= OnPointerExit;
        _button.OnInteractableChanged -= OnInteractableChange;
    }

    private void OnInteractableChange(bool interactable)
    {
        if (!interactable && downWhenDisabled)
            GoDownImmediate();
        else GoUpImmediate();
    }

    private void OnPointerDown()
    {
        if (!_button.interactable) return;
        if (IsSlicedImage) _image.pixelsPerUnitMultiplier = _slicedMultiplier;
        _pressed = true;
        GoDownImmediate();
    }

    private void OnPointerUp()
    {
        if (!_button.interactable) return;
        if (IsSlicedImage) _image.pixelsPerUnitMultiplier = _slicedMultiplier;
        _pressed = false;
        GoUpImmediate();
    }

    private void OnPointerEnter()
    {
        if (!_button.interactable) return;
        if (IsSlicedImage) _image.pixelsPerUnitMultiplier = _slicedMultiplier;
        if (_pressed) GoDownImmediate();
    }

    private void OnPointerExit()
    {
        if (!_button.interactable) return;
        if (IsSlicedImage) _image.pixelsPerUnitMultiplier = _slicedMultiplier;
        if (!_pressed) GoUpImmediate();
    }

    private void GoDownImmediate()
    {
        RectTransform.offsetMin = new Vector2(0, 0);
        RectTransform.offsetMax = new Vector2(0, -HeightPixels);
        Shadow.effectDistance = new Vector2(0, 0);
    }

    private void GoUpImmediate()
    {
        RectTransform.offsetMin = new Vector2(0, HeightPixels);
        RectTransform.offsetMax = new Vector2(0, 0);
        Shadow.effectDistance = new Vector2(0, -HeightPixels);
    }

    private void OnValidate()
    {
        GoUpImmediate();
    }
}