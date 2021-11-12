using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject, ICastable
{
    public int shield;
    public int damage;
    [TextArea(3, 5)]
    public string description;

    public virtual void Cast() { }
}
