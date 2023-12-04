using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBotEnergy
{
    float EnergyCurrentValue { get; }
    float EnergyGrowingCount { get; }
    float EnergyGrowingDelay { get; }
    IEnumerator Energy();
}
