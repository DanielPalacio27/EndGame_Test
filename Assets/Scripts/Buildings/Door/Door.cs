using UnityEngine;

[RequireComponent(typeof(DoorVisuals))]
public class Door : MonoBehaviour
{
    private BoxCollider mCollider = null;
    private DoorVisuals doorVisuals = null;

    void Awake()
    {
        mCollider = GetComponent<BoxCollider>();
        doorVisuals = GetComponent<DoorVisuals>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerInventory playerInventory = collision.gameObject.GetComponent<PlayerInventory>();
        if (playerInventory != null && playerInventory.HasKey)
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInventory playerInventory = other.gameObject.GetComponent<PlayerInventory>();
        if (playerInventory != null && playerInventory.HasKey)
        {
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        mCollider.isTrigger = true;
        doorVisuals.OpenAnimation();
    }

    void CloseDoor()
    {
        mCollider.isTrigger = false;
        doorVisuals.CloseAnimation();
    }

}
