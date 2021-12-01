using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class that handles the interaction on the inventory
/// </summary>
public class OnPointerDownInventory : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private PlayerInventory playerInventory;
    private SlotInfo slotInfo = null;
    private Vector2 onBeginDragPos = Vector2.zero;

    void Start()
    {
        slotInfo = GetComponent<SlotInfo>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //If this slot has an Item, proceed to select it
        if (slotInfo.isFull)
        {
            playerInventory.SelectItem(slotInfo.ID);
        }
    }

    /// <summary>
    /// Stores the initial drag position
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slotInfo.isFull)
        {
            onBeginDragPos = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (slotInfo.isFull)
        {
            playerInventory.DragItem(eventData.position, slotInfo.ID);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (slotInfo.isFull)
        {
            if ((eventData.position -  onBeginDragPos).magnitude >= 50f) //Offset to check if the object can be dropped
            {
                Vector3 worldPos = Vector3.zero;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(eventData.position);

                if (Physics.Raycast(ray, out hit))
                    worldPos = hit.point;

                playerInventory.DropItem(worldPos, slotInfo.ID);
                slotInfo.isFull = false;
            }
            else
            {
                transform.localPosition = Vector3.zero;
            }
        }
    }

}
