public class Key : Item
{
    protected override void Awake()
    {
        base.Awake();
        InitializeItem();
    }
    public override void OnPickUp(PlayerInventory _playerInventory)
    {
        base.OnPickUp(_playerInventory);
        _playerInventory.OnHasKey(true);
    }

    public override void OnDrop(PlayerInventory _playerInventory)
    {
        base.OnDrop(_playerInventory);
        _playerInventory.OnHasKey(false);
    }
}
