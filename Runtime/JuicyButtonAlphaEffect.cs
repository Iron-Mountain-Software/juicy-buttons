using System.Collections;
using UnityEngine;

namespace IronMountain.JuicyButtons
{
    [RequireComponent(typeof(CanvasGroup))]
    public class JuicyButtonAlphaEffect : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private JuicyButton button;
        [SerializeField] private float seconds = 0.15f;
        [SerializeField] private float normalAlpha = 1;
        [SerializeField] private float highlightedAlpha = 1;
        [SerializeField] private float pressedAlpha = 1;
        [SerializeField] private float selectedAlpha = 1;
        [SerializeField] private float disabledAlpha = 1;
        
        private void Awake()
        {
            if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
            if (!button) button = GetComponentInParent<JuicyButton>();
        }

        private void OnValidate()
        {
            if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
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
            if (!canvasGroup || !isActiveAndEnabled) return;
            switch(selectionState)
            {
                case JuicySelectionState.Normal:
                    StartCoroutine(AnimateAlpha(canvasGroup.alpha, normalAlpha));
                    break;
                case JuicySelectionState.Highlighted:
                    StartCoroutine(AnimateAlpha(canvasGroup.alpha, highlightedAlpha));
                    break;
                case JuicySelectionState.Pressed:
                    StartCoroutine(AnimateAlpha(canvasGroup.alpha, pressedAlpha));
                    break;
                case JuicySelectionState.Selected:
                    StartCoroutine(AnimateAlpha(canvasGroup.alpha, selectedAlpha));
                    break;
                case JuicySelectionState.Disabled:
                    StartCoroutine(AnimateAlpha(canvasGroup.alpha, disabledAlpha));
                    break;
            }
        }
        
        private IEnumerator AnimateAlpha(float startAlpha, float endAlpha)
        {
            for (float timer = 0f; timer < seconds; timer += Time.unscaledDeltaTime)
            {
                float progress = timer / seconds;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, Mathf.SmoothStep(0f, 1f, progress));
                yield return null;
            }
            canvasGroup.alpha = endAlpha;
        }
    }
}