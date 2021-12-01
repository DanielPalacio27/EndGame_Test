using UnityEngine;

/// <summary>
/// Helper class to detect when the player select an inventory item
/// </summary>
public class SlotInfo : MonoBehaviour
{
    public int ID = 0;
    public bool isFull = false;
    public void SetID(int _ID)
    {
        ID = _ID;
    }
}
