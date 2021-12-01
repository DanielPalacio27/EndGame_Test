using UnityEngine;

/// <summary>
/// Component that detects when Player Pick Up and Item
/// </summary>
public class PickUpController : MonoBehaviour
{
    private PlayerInventory playerInventory = null;

    private void Awake()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IPickable inventoryItem = other.GetComponent<IPickable>();
        if (inventoryItem != null)
        {
            playerInventory.AddItem(inventoryItem as Item);
            inventoryItem.OnPickUp(playerInventory);
        }
    }
}
