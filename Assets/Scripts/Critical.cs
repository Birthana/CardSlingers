using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Critical", menuName = "Trigger/Critical")]
public class Critical : Trigger
{
    public float critModifer;

    public override int PerformTrigger(int damage)
    {
        return Mathf.CeilToInt(damage * critModifer);
    }
}
