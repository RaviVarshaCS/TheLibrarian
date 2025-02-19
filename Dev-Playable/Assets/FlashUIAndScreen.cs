// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections;

// public class FlashUIAndScreen : MonoBehaviour
// {
//     public Image uiElement;      // UI element to flash first
//     public Image screenOverlay;  // Full-screen UI Image (set to cover entire screen)
//     public Color flashColor = Color.red;
//     public float flashDuration = 2f;
//     public float flashSpeed = 5f;
//     public float screenFlashDuration = 2f;
//     public GameObject UIPanelRel;
//     public GameObject UIPanelRep;

//     private Color originalUIColor;
//     private Color originalScreenColor;
//     private PlayerRelationship rel;
//     private PlayerReputation rep;
//     private int relation;
//     private int reput;
//     bool flashed;

//     void Start()
//     {
//         if (uiElement != null && screenOverlay != null)
//         {
//             originalUIColor = uiElement.color;
//             originalScreenColor = screenOverlay.color;
//             Debug.Log("Starting Flash Loop");
            
//         }
//         rel = PlayerRelationship.Instance;
//         rep = PlayerReputation.Instance;
//         flashed = false;
//         UIPanelRel.SetActive(false);
//         UIPanelRep.SetActive(false);
//     }

//     void Update() {
//         relation = rel.Relationship;
        
//         Debug.Log("Relation is +" + relation);
//         reput = rep.Reputation;
//         Debug.Log("Reputation is" + reput);

//         if(!flashed && (reput <= 20)) {
//             Debug.Log("STATS HAVE DROPPED!!!!!");
//             StartCoroutine(FlashSequenceREP());
            
//         } else if (!flashed && relation <= 20) {
//             Debug.Log("STATS HAVE DROPPED!!!!!");
//             StartCoroutine(FlashSequenceREL());
//         }

//     }

//     IEnumerator FlashSequenceREP()
//     {
//         // isFlashing = true; // Set flag to prevent multiple flashes

//         // Step 1: Flash the UI element
//         yield return StartCoroutine(FlashEffect(uiElement, originalUIColor, flashColor, flashDuration));

//         // // Step 2: Flash the whole screen
//         // yield return StartCoroutine(FlashEffect(screenOverlay, originalScreenColor, flashColor, screenFlashDuration));

//         // Reset the screen overlay color (hide it)
//         screenOverlay.color = new Color(originalScreenColor.r, originalScreenColor.g, originalScreenColor.b, 0);

//         UIPanelRep.SetActive(true);
//         flashed = true; // Reset flag to allow future flashes
//     }

//     IEnumerator FlashSequenceREL()
//     {
//         // isFlashing = true; // Set flag to prevent multiple flashes

//         // Step 1: Flash the UI element
//         yield return StartCoroutine(FlashEffect(uiElement, originalUIColor, flashColor, flashDuration));

//         // // Step 2: Flash the whole screen
//         // yield return StartCoroutine(FlashEffect(screenOverlay, originalScreenColor, flashColor, screenFlashDuration));

//         // Reset the screen overlay color (hide it)
//         screenOverlay.color = new Color(originalScreenColor.r, originalScreenColor.g, originalScreenColor.b, 0);

//         UIPanelRep.SetActive(true);
//         flashed = true; // Reset flag to allow future flashes
//     }

//     IEnumerator FlashEffect(Image target, Color original, Color flash, float duration)
//     {
//         float elapsed = 0f;
//         while (elapsed < duration)
//         {
//             // Use a custom timer to interpolate the color instead of PingPong with Time
//             float t = Mathf.PingPong(elapsed * flashSpeed, 1);  
//             target.color = Color.Lerp(original, flash, t);
            
//             elapsed += Time.deltaTime; // Increment elapsed time
//             yield return null;
//         }
//         target.color = original; // Reset color after flashing
//     }

// }
