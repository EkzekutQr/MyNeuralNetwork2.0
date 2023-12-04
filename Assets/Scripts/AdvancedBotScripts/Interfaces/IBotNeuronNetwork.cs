using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBotNeuronNetwork
{
    NN NN { get; }
    float MutateStrength { get; }
    List<float> Genes { get; }
    void Mutate(float mutateStrength);
    List<float> GetGenes();
    void SetGenes(List<float> genes);
}
