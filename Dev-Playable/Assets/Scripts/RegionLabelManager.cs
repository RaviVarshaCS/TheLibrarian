using UnityEngine;
using TMPro;  // Required for TextMeshPro

public class RegionLabelManager : MonoBehaviour
{
    public Transform publicRegion;      // Reference to the public region's transform
    public Transform restrictedRegion;  // Reference to the restricted region's transform
    public TextMeshProUGUI publicLabel;  // Reference to the public label UI (TextMeshPro)
    public TextMeshProUGUI restrictedLabel;  // Reference to the restricted label UI (TextMeshPro)

    public float labelHeight = 1.0f; // Height offset for the labels above the regions

    void Update()
    {
        // Update the position of the labels above their respective regions
        if (publicRegion != null)
        {
            publicLabel.transform.position = publicRegion.position + new Vector3(0, labelHeight, 0);
        }
        
        if (restrictedRegion != null)
        {
            restrictedLabel.transform.position = restrictedRegion.position + new Vector3(0, labelHeight, 0);
        }
    }
}
