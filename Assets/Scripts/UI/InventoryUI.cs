using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform inventoryContainer = null;
    [SerializeField] private Transform slotTemplate = null;

    [SerializeField] private RectTransform selectedSlot = null;

    public SlotInfo[] slotInfo = null;

    /// <summary>
    /// This function instantiates the UI slots based on inventory size
    /// </summary>
    /// <param name="_size"></param>
    /// <param name="_items"></param>
    public void InitUI_Inventory(int _size, Item[] _items)
    {
        slotInfo = new SlotInfo[_size];
                
        for (int i = 0; i < _size; i++)
        {
            Transform slotTransform = Instantiate(slotTemplate, inventoryContainer);

            //Get the child for display the item
            slotInfo[i] = slotTransform.GetChild(0).GetComponent<SlotInfo>();
            slotInfo[i].SetID(i + 1);
            slotInfo[i].isFull = true;
        }
        
        UpdateUIInventory(_items);        
    }

    public void ResetSelectedSlot()
    {
        selectedSlot.position = new Vector3(-5000, -5000, 0);
    }

    public void UpdateSelectedSlot(int _selectedSlot)
    {
        AudioController.Instance.PlaySingle(AudioController.Instance.data.selectItem, false);
        selectedSlot.position = slotInfo[_selectedSlot].transform.position;
    }

    /// <summary>
    /// When slot is dragged to drop it this function updates its position
    /// </summary>
    /// <param name="_pos"></param>
    /// <param name="_ID"></param>
    public void DragSlot(Vector3 _pos, int _ID)
    {
        slotInfo[_ID - 1].transform.position = _pos;
    }

    public void ResetSlot(int _ID)
    {
        AudioController.Instance.PlaySingle(AudioController.Instance.data.dropItem, false);
        Image mImage = slotInfo[_ID - 1].GetComponent<Image>();
        slotInfo[_ID - 1].transform.localPosition = Vector3.zero;
        mImage.sprite = null;
        mImage.enabled = false;
    }

    /// <summary>
    /// Updates the slot of the inventory with the item image
    /// </summary>
    /// <param name="_itemList"></param>
    public void UpdateUIInventory(Item[] _itemList)
    {
        int length = _itemList.Length;
        for(int i = 0; i < length; i++)
        {
            if (_itemList[i] != null)
            {
                Image itemImage = slotInfo[i].GetComponent<Image>();
                itemImage.enabled = true;
                itemImage.sprite = _itemList[i]._itemIcon;
                slotInfo[i].isFull = true;                
            }
        }
    }
}
