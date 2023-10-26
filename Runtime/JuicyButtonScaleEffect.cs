using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace IronMountain.JuicyButtons
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(JuicyButton))]
    public class JuicyButtonScaleEffect : MonoBehaviour
    {
        [SerializeField] private JuicyButton button;
        [SerializeField] private float seconds = 0.15f;
        [SerializeField] [FormerlySerializedAs("scaleWhenUp")] private Vector3 normalScale = Vector3.one;
        [SerializeField] private Vector3 highlightedScale = Vector3.one;
        [SerializeField] [FormerlySerializedAs("scaleWhenDown")] private Vector3 pressedScale = Vector3.one * .95f;
        [SerializeField] private Vector3 selectedScale = Vector3.one;
        [SerializeField] private Vector3 disabledScale = Vector3.one;

        private void Awake()
        {
            if (!button) button = GetComponent<JuicyButton>();
        }

        private void OnValidate()
        {
            if (!button) button = GetComponent<JuicyButton>();
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
            if (!isActiveAndEnabled) return;
            switch(selectionState)
            {
                case JuicySelectionState.Normal:
                    StartCoroutine(AnimateScale(transform.localScale, normalScale));
                    break;
                case JuicySelectionState.Highlighted:
                    StartCoroutine(AnimateScale(transform.localScale, highlightedScale));
                    break;
                case JuicySelectionState.Pressed:
                    StartCoroutine(AnimateScale(transform.localScale, pressedScale));
                    break;
                case JuicySelectionState.Selected:
                    StartCoroutine(AnimateScale(transform.localScale, selectedScale));
                    break;
                case JuicySelectionState.Disabled:
                    StartCoroutine(AnimateScale(transform.localScale, disabledScale));
                    break;
            }
        }
        
        private IEnumerator AnimateScale(Vector3 startScale, Vector3 endScale)
        {
            for (float timer = 0f; timer < seconds; timer += Time.unscaledDeltaTime)
            {
                float progress = timer / seconds;
                transform.localScale = Vector3.Lerp(startScale, endScale, Mathf.SmoothStep(0f, 1f, progress));
                yield return null;
            }
        }
    }
}