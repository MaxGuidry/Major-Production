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

    public static Modifier CreateModifier(int value, Stat targetStat, ModType type)
    {
        var Mod = CreateInstance<Modifier>();
        Mod.Value = value;
        Mod.Target = targetStat;
        Mod.Type = type;
        return Mod;
    }
}
