using UnityEngine;

[RequireComponent(typeof(RoofVisuals))]
public class Roof : MonoBehaviour
{
    private RoofVisuals roofVisuals = null;
    private void Awake() => roofVisuals = GetComponent<RoofVisuals>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerInventory>() != null)
        {
            roofVisuals.InsideBuilding();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerInventory>() != null)
        {
            roofVisuals.OutsideBuilding();
        }
    }
}
