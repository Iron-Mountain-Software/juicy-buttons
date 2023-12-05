# Juicy Buttons
*Version: 1.0.2*
## Description: 
Adds all of the missing events to buttons, allowing deeper customization options.
## Use Cases: 
* Button sound effects
* Button scale animations
* Button 3D effects
* Button alpha effects
* Button text font style effects
## Package Mirrors: 
[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODg3LnBuZw==/original/npRUfq.png'>](https://github.com/Iron-Mountain-Software/juicy-buttons.git)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODk4LnBuZw==/original/Rv4m96.png'>](https://iron-mountain.itch.io/juicy-buttons)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODkyLnBuZw==/original/Fq0ORM.png'>](https://www.npmjs.com/package/com.iron-mountain.juicy-buttons)
---
## Key Scripts & Components: 
1. public class **JuicyButton** : Button
   * Actions: 
      * public event Action ***OnPointerDownEvent*** 
      * public event Action ***OnPointerUpEvent*** 
      * public event Action ***OnPointerEnterEvent*** 
      * public event Action ***OnPointerExitEvent*** 
      * public event Action ***OnPointerClickEvent*** 
      * public event Action ***OnInteractableChanged*** 
      * public event Action ***OnSelectionStateChanged*** 
   * Properties: 
      * public Boolean ***Interactable***  { get; set; }
      * public JuicySelectionState ***CurrentSelectionState***  { get; }
   * Methods: 
      * public override void ***OnPointerDown***(PointerEventData eventData)
      * public override void ***OnPointerUp***(PointerEventData eventData)
      * public override void ***OnPointerEnter***(PointerEventData eventData)
      * public override void ***OnPointerExit***(PointerEventData eventData)
      * public override void ***OnPointerClick***(PointerEventData eventData)
1. public class **JuicyButton3DEffect** : MonoBehaviour
1. public class **JuicyButtonAlphaEffect** : MonoBehaviour
1. public class **JuicyButtonFontStyleEffect** : MonoBehaviour
1. public class **JuicyButtonGraphicColorEffect** : MonoBehaviour
1. public class **JuicyButtonSFX** : MonoBehaviour
1. public class **JuicyButtonScaleEffect** : MonoBehaviour
1. public enum **JuicySelectionState** : Enum
