using UnityEngine;

public class Item : MonoBehaviour, IPickable
{
    [SerializeField] protected ItemData itemData;
    public string _itemName { get; protected set; }
    public Sprite _itemIcon { get; protected set; }
    public bool wasPickedUp { get; set; }

    private Collider mCollider = null;

    protected virtual void Awake()
    {
        mCollider = GetComponent<Collider>();
    }

    public void InitializeItem()
    {
        _itemName = itemData.name;
        _itemIcon = itemData.icon;
    }

    public virtual void OnDrop(PlayerInventory _playerInventory)
    {
        AudioController.Instance.PlaySingle(AudioController.Instance.data.dropItem, false);
        wasPickedUp = false;
        mCollider.enabled = true;
    }

    public virtual void OnPickUp(PlayerInventory _playerInventory)
    {
        AudioController.Instance.PlaySingle(AudioController.Instance.data.itemPickedUp, false);
        wasPickedUp = true;
        mCollider.enabled = false;
    }
}
