using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    ///Object in which the items picked up are stored (Hand_R_Weapon)    
    [SerializeField] private Transform itemsContainer = null;
    [SerializeField] private InventoryUI inventoryUI = null;

    private int size = 4;
    public bool HasKey { get; private set; } = false;
    public bool HasWeaponEquipped { get; private set; } = true;

    private Item[] items = null;

    private bool[] hasItem = null;

    void Start()
    {
        InitInventory();
    }
    public void OnHasKey(bool _hasKey) => HasKey = _hasKey;

    /// <summary>
    /// Initialize Arrays and Inventory if Player Starts with items
    /// </summary>
    public void InitInventory()
    {
        items = new Item [size];
        hasItem = new bool[size];
        int initialItemsLength = itemsContainer.childCount;

        if(initialItemsLength > 0)
        {
            for(int i = 0; i < initialItemsLength; i++)
            {
                items[i] = itemsContainer.GetChild(i).GetComponent<Item>();
                items[i].OnPickUp(this);
                AddAndSetSlotInfoToObject(items[i].gameObject, i + 1);
                hasItem[i] = true;
            }

        }

        inventoryUI.InitUI_Inventory(size, items);
        StartCoroutine(CoroutineUtilities.WaitForEndOfFrame(() => SelectItem(1)));
    }

    /// <summary>
    /// Selects a inventory item by the given ID
    /// </summary>
    /// <param name="_id"></param>
    public void SelectItem(int _id)
    {
        for(int i = 0; i < size; i++)
        {
            if (hasItem[i])
            {
                if (items[i].GetComponent<SlotInfo>().ID == _id)
                {
                    items[i].gameObject.SetActive(true);
                    inventoryUI.UpdateSelectedSlot(i);
                    ///Verify if the selected item is a weapon
                    HasWeaponEquipped = items[i].GetComponent<Weapon>() != null ? true : false;
                }
                else
                {
                    items[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void FindNextItemEquipped()
    {
        bool itemFound = false;
        for(int i = 0; i < size; i++)
        {
            if(hasItem[i])
            {
                SelectItem(items[i].GetComponent<SlotInfo>().ID);
                itemFound = true;
                break;
            }
        }

        if (!itemFound)
            inventoryUI.ResetSelectedSlot();
    }

    public void DragItem(Vector2 _screenPos, int _itemID)
    {
        for(int i = 0; i < size; i++)
        {
            if (hasItem[i] && items[i].GetComponent<SlotInfo>().ID == _itemID)
            {
                inventoryUI.DragSlot(_screenPos, _itemID);
                break;
            }
        }
    }

    /// <summary>
    /// Drop an item to the corresponding world position of drag and delete it from the array of items
    /// </summary>
    /// <param name="_worldPosition"></param>
    /// <param name="_itemID"></param>
    public void DropItem(Vector3 _worldPosition, int _itemID)
    {
        for (int i = 0; i < size; i++)
        {
            if (hasItem[i] && items[i].GetComponent<SlotInfo>().ID == _itemID)
            {
                HasWeaponEquipped = items[i].GetComponent<Weapon>() != null ? false : true;

                items[i].transform.position = new Vector3(_worldPosition.x, .5f, _worldPosition.z);
                items[i].transform.rotation = Quaternion.Euler(0, 90, 0);
                items[i].transform.SetParent(null);
                Destroy(items[i].GetComponent<SlotInfo>());
                items[i].OnDrop(this);
                items[i] = null;
                hasItem[i] = false;

                inventoryUI.ResetSlot(_itemID);
                inventoryUI.UpdateUIInventory(items);

                FindNextItemEquipped();
                break;
            }

        }
      
    }

    /// <summary>
    /// Add and Set SlotInfo Component to Object that was picked up 
    /// </summary>
    /// <param name="_obj"></param>
    /// <param name="_id"></param>
    private void AddAndSetSlotInfoToObject(GameObject _obj, int _id)
    {
        SlotInfo _slotInfo = _obj.AddComponent<SlotInfo>();
        _slotInfo.ID = _id;
        _slotInfo.isFull = true;
    }

    public void AddItem(Item _item)
    {
        for(int i = 0; i < size; i++)
        {
            if(hasItem[i] == false)
            {
                items[i] = _item;
                _item.transform.SetParent(itemsContainer);
                _item.transform.localPosition = Vector3.zero;
                _item.transform.localRotation = Quaternion.identity;
                AddAndSetSlotInfoToObject(_item.gameObject, i + 1);
   
                _item.gameObject.SetActive(false);
                inventoryUI.UpdateUIInventory(items);

                if (CheckIfIsEmpty())
                    StartCoroutine(CoroutineUtilities.WaitForEndOfFrame(() => SelectItem(1)));

                hasItem[i] = true;
                break;
            }
        }
    }
    /// <summary>
    /// Check if inventory has not items
    /// </summary>
    /// <returns></returns>
    public bool CheckIfIsEmpty()
    {
        int index = 0;
        foreach(bool b in hasItem)
        {
            if (b)
                index++;
        }

        if (index == 0)
            return true;
        else
            return false;
    }

}
