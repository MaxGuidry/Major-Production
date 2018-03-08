using ScriptableObjects;

public interface IStorageable
{
    void AddToInventory(Item theItem);
    void RemoveFromInventory(Item theItem);
    void RemoveAllFromInventory();
}
