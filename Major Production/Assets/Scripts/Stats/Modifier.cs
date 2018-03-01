using UnityEngine;

public enum ModType
{
    Add,
    Mult
}
public class Modifier : ScriptableObject
{
    public int ID;
    public int Value;
    public ModType Type;
    public Stat Target;
}
