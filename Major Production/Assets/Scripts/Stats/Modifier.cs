using UnityEngine;

public enum ModType
{
    Add,
    Mult
}
[CreateAssetMenu]
public class Modifier : ScriptableObject
{
    public int ID;
    public int Value;
    public ModType Type;
    public Stat Target;
}
