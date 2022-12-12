using System.Collections;
using UnityEngine;

namespace SpellBoundAR.JuicyButtons
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(JuicyButton))]
    public class JuicyButtonScaleEffect : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Vector3 scaleWhenUp = Vector3.one;
        [SerializeField] private Vector3 scaleWhenDown = Vector3.one * .9f;
        [SerializeField] private float seconds = 0.2f;
        [SerializeField] private bool downWhenDisabled = true;

        [Header("Cache")]
        private JuicyButton _button;
        private bool _down;
        private bool _pressed;
        
        private void Awake()
        {
            _button = GetComponent<JuicyButton>();
        }

        private void OnEnable()
        {
            if (!_button) return;
            _button.OnPointerDownEvent += OnPointerDown;
            _button.OnPointerUpEvent += OnPointerUp;
            _button.OnPointerEnterEvent += OnPointerEnter;
            _button.OnPointerExitEvent += OnPointerExit;
            _button.OnInteractableChanged += OnInteractableChange;

            OnInteractableChange(_button.interactable);
        }

        private void OnDisable()
        {
            if (!_button) return;
            _button.OnPointerDownEvent -= OnPointerDown;
            _button.OnPointerUpEvent -= OnPointerUp;
            _button.OnPointerEnterEvent -= OnPointerEnter;
            _button.OnPointerExitEvent -= OnPointerExit;
            _button.OnInteractableChanged -= OnInteractableChange;
        }

        private void OnInteractableChange(bool interactable)
        {
            if (!interactable && downWhenDisabled)
                GoDown();
            else GoUp();
        }

        private void OnPointerDown()
        {
            if (!_button.interactable) return;
            _pressed = true;
            GoDown();
        }

        private void OnPointerUp()
        {
            if (!_button.interactable) return;
            _pressed = false;
            GoUp();
        }

        private void OnPointerEnter()
        {
            if (!_button.interactable) return;
            if (_pressed) GoDown();
        }

        private void OnPointerExit()
        {
            if (!_button.interactable) return;
            if (!_pressed) GoUp();
        }

        private void GoDown()
        {
            if (_down) return;
            _down = true;
            StopAllCoroutines();
            StartCoroutine(SmoothRescale(transform.localScale, scaleWhenDown));
        }

        private void GoUp()
        {
            if (!_down) return;
            _down = false;
            StopAllCoroutines();
            StartCoroutine(SmoothRescale(transform.localScale, scaleWhenUp));
        }

        private IEnumerator SmoothRescale(Vector3 startScale, Vector3 endScale)
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