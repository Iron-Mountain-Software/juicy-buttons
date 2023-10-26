using UnityEngine;

namespace IronMountain.JuicyButtons
{
    [RequireComponent(typeof(JuicyButton))]
    [RequireComponent(typeof(AudioSource))]
    public class JuicyButtonSFX : MonoBehaviour
    {
        [Header("SFX")]
        [SerializeField] private AudioClip buttonDownSound;
        [SerializeField] private AudioClip buttonUpSound;

        [Header("Cache")]
        private JuicyButton _button;
        private AudioSource _audioSource;

        private void Awake()
        {
            _button = GetComponent<JuicyButton>();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = null;
            _audioSource.playOnAwake = false;
        }

        private void OnEnable()
        {
            _button.OnPointerDownEvent += OnPointerDown;
            _button.OnPointerClickEvent += OnPointerClick;
        }

        private void OnDisable()
        {
            _button.OnPointerDownEvent -= OnPointerDown;
            _button.OnPointerClickEvent -= OnPointerClick;
        }

        private void OnPointerDown()
        {
            if (!_button.Interactable) return;
            _audioSource.PlayOneShot(buttonDownSound);
        }

        private void OnPointerClick()
        {
            if (!_button.Interactable) return;
            _audioSource.PlayOneShot(buttonUpSound);
        }
    }
}