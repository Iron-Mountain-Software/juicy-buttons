using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(Graphic))]
[RequireComponent(typeof(Shadow))]
[RequireComponent(typeof(JuicyButton))]
public class JuicyButton3DEffect : MonoBehaviour
{
    private const string ShaderName = "Custom/3DEffectShader";
    private const string MainColorName = "_MainColor";
    private const string ShadowColorName = "_ShadowColor";
    
    private static readonly int MainColor = Shader.PropertyToID(MainColorName);
    private static readonly int ShadowColor = Shader.PropertyToID(ShadowColorName);

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
    private Material _material;
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
        Shadow.enabled = true;
        if (IsSlicedImage) _slicedMultiplier = _image.pixelsPerUnitMultiplier;
        _button.OnPointerDownEvent += OnPointerDown;
        _button.OnPointerUpEvent += OnPointerUp;
        _button.OnPointerEnterEvent += OnPointerEnter;
        _button.OnPointerExitEvent += OnPointerExit;
        _button.OnInteractableChanged += OnInteractableChange;
        OnInteractableChange(_button.interactable);
        CreateMaterial();
    }

    private void OnDisable()
    {
        Shadow.enabled = false;
        _button.OnPointerDownEvent -= OnPointerDown;
        _button.OnPointerUpEvent -= OnPointerUp;
        _button.OnPointerEnterEvent -= OnPointerEnter;
        _button.OnPointerExitEvent -= OnPointerExit;
        _button.OnInteractableChanged -= OnInteractableChange;
        DestroyMaterial();
    }

    private void CreateMaterial()
    {
        if (_material) return;
        Shader shader = Shader.Find(ShaderName);
        _material = new Material(shader);
        _image.material = _material;
    }

    private void DestroyMaterial()
    {
        if (!_material) return;
        _image.material = null;
        if (Application.isPlaying) Destroy(_material);
        else DestroyImmediate(_material);
        _material = null;
    }

    private void RefreshColors()
    {
        if (!_material) return;
        _material.SetColor(MainColor, _image.color);
        _material.SetColor(ShadowColor, _shadow.effectColor);
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
        if (enabled)
        {
            RectTransform.offsetMin = Vector2.zero;
            RectTransform.offsetMax = new Vector2(0, -HeightPixels);
            Shadow.effectDistance = Vector2.zero;
        }
        else
        {
            RectTransform.offsetMin = Vector2.zero;
            RectTransform.offsetMax = Vector2.zero;
            Shadow.effectDistance = Vector2.zero;
        }
    }

    private void GoUpImmediate()
    {
        if (enabled)
        {
            RectTransform.offsetMin = new Vector2(0, HeightPixels);
            RectTransform.offsetMax = Vector2.zero;
            Shadow.effectDistance = new Vector2(0, -HeightPixels);
        }
        else
        {
            RectTransform.offsetMin = Vector2.zero;
            RectTransform.offsetMax = Vector2.zero;
            Shadow.effectDistance = Vector2.zero;
        }
    }

    private void OnValidate()
    {
        RefreshColors();
        GoUpImmediate();
    }
}