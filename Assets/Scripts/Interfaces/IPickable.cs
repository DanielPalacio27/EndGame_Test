public interface IPickable
{
    bool wasPickedUp { get; set; }
    void OnPickUp(PlayerInventory _playerInventory);
    void OnDrop(PlayerInventory _playerInventory);
}
