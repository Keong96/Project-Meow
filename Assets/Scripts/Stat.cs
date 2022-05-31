using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Stat
{
    public float baseValue;
    public float currentValue;

    public List<Modifier> modifiers;

    public void Init()
    {
        currentValue = baseValue;
        modifiers = new List<Modifier>();
    }

    public void AddModifier(Modifier mod)
    {
        modifiers.Add(mod);
    }

    public void RemoveModifier(Modifier mod)
    {
        for(int i = 0; i < modifiers.Count; i++)
        {
            if(modifiers[i] == mod)
            {
                modifiers.RemoveAt(i);
            }
        }
    }
    
    public void SortModifier()
    {
        //modifiers.Sort((m1, m2) => m1.order.CompareTo(m2.order));
        modifiers.OrderBy(m => m.order).ToList();
    }

    public void Calculate()
    {
        SortModifier();

        currentValue = baseValue;

        foreach (Modifier mod in modifiers)
        {
            switch(mod.type)
            {
                case ModifierType.Flat:
                    currentValue += mod.value;
                    break;

                case ModifierType.Multiply:
                    currentValue *= mod.value;
                    break;
            }
        }
    }
}
