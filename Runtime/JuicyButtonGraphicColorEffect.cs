using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.JuicyButtons
{
    [RequireComponent(typeof(Graphic))]
    public class JuicyButtonGraphicColorEffect : MonoBehaviour
    {
        [SerializeField] private Graphic graphic;
        [SerializeField] private JuicyButton button;
        [SerializeField] private float seconds = 0.15f;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color highlightedColor = Color.white;
        [SerializeField] private Color pressedColor = Color.white;
        [SerializeField] private Color selectedColor = Color.white;
        [SerializeField] private Color disabledColor = Color.white;
        
        private void Awake()
        {
            if (!graphic) graphic = GetComponent<Graphic>();
            if (!button) button = GetComponentInParent<JuicyButton>();
        }

        private void OnValidate()
        {
            if (!graphic) graphic = GetComponent<Graphic>();
            if (!button) button = GetComponentInParent<JuicyButton>();
        }
        
        private void OnEnable()
        {
            if (button) button.OnSelectionStateChanged += OnSelectionStateChanged;
            OnSelectionStateChanged(button
                ? button.CurrentSelectionState
                : JuicySelectionState.Normal);
        }

        private void OnDisable()
        {
            if (button) button.OnSelectionStateChanged -= OnSelectionStateChanged;
        }
        
        private void OnSelectionStateChanged(JuicySelectionState selectionState)
        {
            StopAllCoroutines();
            if (!graphic || !isActiveAndEnabled) return;
            switch(selectionState)
            {
                case JuicySelectionState.Normal:
                    StartCoroutine(AnimateColor(graphic.color, normalColor));
                    break;
                case JuicySelectionState.Highlighted:
                    StartCoroutine(AnimateColor(graphic.color, highlightedColor));
                    break;
                case JuicySelectionState.Pressed:
                    StartCoroutine(AnimateColor(graphic.color, pressedColor));
                    break;
                case JuicySelectionState.Selected:
                    StartCoroutine(AnimateColor(graphic.color, selectedColor));
                    break;
                case JuicySelectionState.Disabled:
                    StartCoroutine(AnimateColor(graphic.color, disabledColor));
                    break;
            }
        }
        
        private IEnumerator AnimateColor(Color startColor, Color endColor)
        {
            for (float timer = 0f; timer < seconds; timer += Time.unscaledDeltaTime)
            {
                float progress = timer / seconds;
                graphic.color = Color.Lerp(startColor, endColor, Mathf.SmoothStep(0f, 1f, progress));
                yield return null;
            }
            graphic.color = endColor;
        }
    }
}