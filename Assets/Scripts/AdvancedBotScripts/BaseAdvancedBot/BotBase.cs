using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBase : MonoBehaviour,
    IBotMove, IBotRotate, IBotRaycast, 
    IBotReproduction, IBotEnergy, IBotNeuronNetwork
{
    public float EnergyCurrentValue { get; private set; }

    public float EnergyGrowingCount { get; private set; }

    public float EnergyGrowingDelay { get; private set; }

    public float ReproducingEnergyCost { get; private set; }

    public NN NN { get; private set; }

    public float MutateStrength { get; private set; }

    public List<float> Genes { get; private set; }

    public void Breed()
    {
        BotBase newChild = Instantiate(gameObject, transform.position, Quaternion.identity, null).GetComponent<BotBase>();

        newChild.Mutate(MutateStrength);
    }

    public List<float> GetGenes()
    {
        List<float> genes = new List<float>();

        genes.Clear();
        for (int i = 0; i <= NN.Layer.Length - 1; i++)
        {
            if (NN.Layer[i].weights == null)
                continue;
            if (NN.Layer[i].weights.Length == 0)
                continue;
            genes.AddRange(NN.Layer[i].weights);
        }

        return genes;
    }

    public void SetGenes(List<float> genes)
    {
        if (genes == null)
        {
            NN.SetRandomWeights();
            return;
        }
        if (genes.Count == 0)
        {
            NN.SetRandomWeights();
            return;
        }

        int x = genes.Count - 1;

        for (int i = NN.Layer.Length - 1; i >= 0; i--)
        {
            if (NN.Layer[i].weights == null)
                continue;
            if (NN.Layer[i].weights.Length == 0)
                continue;
            for (int j = NN.Layer[i].weights.Length - 1; j >= 0; j--)
            {
                NN.Layer[i].weights[j] = genes[x];
                x--;
            }
        }

        Genes = genes;
    }

    public void Mutate(float mutateStrength)
    {
        int randomGenIndex = Random.Range(0, Genes.Count);

        Genes[randomGenIndex] = Genes[randomGenIndex] + Random.Range(-mutateStrength, mutateStrength);

        if (Genes[randomGenIndex] > 2f)
            Genes[randomGenIndex] = 2f;

        if (Genes[randomGenIndex] < -2f)
            Genes[randomGenIndex] = -2f;
    }

    public void Reproducting()
    {
        if (EnergyCurrentValue >= ReproducingEnergyCost)
        {
            EnergyCurrentValue = EnergyCurrentValue - ReproducingEnergyCost;

            Breed();
        }
    }

    public IEnumerator Energy()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnergyGrowingDelay);
            EnergyCurrentValue = EnergyCurrentValue + EnergyGrowingCount;
        }
    }

    public void ThrowRaycast()
    {
        throw new System.NotImplementedException();
    }

    public void Rotate(Vector3 direction, float speed)
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 direction, float speed)
    {
        throw new System.NotImplementedException();
    }
}
