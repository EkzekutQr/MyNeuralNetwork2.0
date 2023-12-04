using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBotReproduction
{
    public float ReproducingEnergyCost { get; }
    void Reproducting();
    void Breed();
}
