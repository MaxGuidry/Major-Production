using ScriptableObjects;

public interface IStorageable
{
    void AddToInventory(Item newItem);
    void RemoveFromInventory(Item theItem);
    void RemoveAllFromInventory();
}
