using UnityEngine;
using UnityEngine.UI;

namespace IronMountain.JuicyButtons
{
    [RequireComponent(typeof(Text))]
    public class JuicyButtonFontStyleEffect : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private JuicyButton button;
        [SerializeField] private FontStyle normalFontStyle = FontStyle.Normal;
        [SerializeField] private FontStyle highlightedFontStyle = FontStyle.Normal;
        [SerializeField] private FontStyle pressedFontStyle = FontStyle.Normal;
        [SerializeField] private FontStyle selectedFontStyle = FontStyle.Normal;
        [SerializeField] private FontStyle disabledFontStyle = FontStyle.Normal;
        
        private void Awake()
        {
            if (!text) text = GetComponent<Text>();
            if (!button) button = GetComponentInParent<JuicyButton>();
        }

        private void OnValidate()
        {
            if (!text) text = GetComponent<Text>();
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
            if (!text) return;
            switch(selectionState)
            {
                case JuicySelectionState.Normal:
                    text.fontStyle = normalFontStyle;
                    break;
                case JuicySelectionState.Highlighted:
                    text.fontStyle = highlightedFontStyle;
                    break;
                case JuicySelectionState.Pressed:
                    text.fontStyle = pressedFontStyle;
                    break;
                case JuicySelectionState.Selected:
                    text.fontStyle = selectedFontStyle;
                    break;
                case JuicySelectionState.Disabled:
                    text.fontStyle = disabledFontStyle;
                    break;
            }
        }
    }
}