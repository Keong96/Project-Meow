public enum ModifierType
{
    Flat,
    Multiply
}

public class Modifier
{
    public ModifierType type;
    public float value;
    public int order;
}