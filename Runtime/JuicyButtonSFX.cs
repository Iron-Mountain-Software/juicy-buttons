using UnityEngine;

namespace IronMountain.JuicyButtons
{
    [RequireComponent(typeof(JuicyButton))]
    [RequireComponent(typeof(AudioSource))]
    public class JuicyButtonSFX : MonoBehaviour
    {
        [Header("SFX")]
        [SerializeField] private JuicyButton button;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip pointerEnterSound;
        [SerializeField] private AudioClip pointerExitSound;
        [SerializeField] private AudioClip buttonDownSound;
        [SerializeField] private AudioClip buttonUpSound;

        private void Awake() => Initialize();
        private void OnValidate() => Initialize();

        private void Initialize()
        {
            if (!button) button = GetComponent<JuicyButton>();
            if (!audioSource) audioSource = GetComponent<AudioSource>();
            if (!audioSource) audioSource = gameObject.AddComponent<AudioSource>();
            if (!audioSource) return;
            audioSource.clip = null;
            audioSource.playOnAwake = false;
        }

        private void OnEnable()
        {
            if (!button) return;
            button.OnPointerEnterEvent += OnPointerEnter;
            button.OnPointerExitEvent += OnPointerExit;
            button.OnPointerDownEvent += OnPointerDown;
            button.OnPointerClickEvent += OnPointerClick;
        }

        private void OnDisable()
        {
            if (!button) return;
            button.OnPointerEnterEvent -= OnPointerEnter;
            button.OnPointerExitEvent -= OnPointerExit;
            button.OnPointerDownEvent -= OnPointerDown;
            button.OnPointerClickEvent -= OnPointerClick;
        }

        private void OnPointerEnter()
        {
            if (!button || !button.Interactable) return;
            if (!audioSource || !pointerEnterSound) return;
            audioSource.PlayOneShot(pointerEnterSound);
        }
        
        private void OnPointerExit()
        {
            if (!button || !button.Interactable) return;
            if (!audioSource || !pointerExitSound) return;
            audioSource.PlayOneShot(pointerExitSound);
        }
        
        private void OnPointerDown()
        {
            if (!button || !button.Interactable) return;
            if (!audioSource || !buttonDownSound) return;
            audioSource.PlayOneShot(buttonDownSound);
        }

        private void OnPointerClick()
        {
            if (!button || !button.Interactable) return;
            if (!audioSource || !buttonUpSound) return;
            audioSource.PlayOneShot(buttonUpSound);
        }
    }
}