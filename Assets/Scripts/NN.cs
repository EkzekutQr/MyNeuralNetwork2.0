using System.Collections.Generic;
using UnityEngine;

public class NN
{
    Layer[] layers;
    public Layer[] Layer { get => layers; }
    public NN(int layersCount, int neuronCount)
    {
        if (layersCount < 3) layersCount = 3;

        layers = new Layer[layersCount];

        layers[0] = new Layer(2);
        //layers[0].neurons[0] = 1f; //нейрон смещения

        for (int i = 1; i <= layers.Length - 2; i++)
        {
            layers[i] = new Layer(neuronCount, layers[i-1].neurons.Length * neuronCount);
        }

        layers[layers.Length - 1] = new Layer(2, /*layers[layers.Length - 1].neurons.Length*/ (2 * layers[layers.Length - 2].neurons.Length));
    }
    public void SetInputValues(float firstNeuron, float secondNeuron/*, Vector2 thirdNeuron*/)
    {
        layers[0].neurons[0] = firstNeuron;
        layers[0].neurons[1] = secondNeuron;
        //layers[0].neurons[2] = thirdNeuron;
    }
    public void SetRandomWeights()
    {
        for(int i = layers.Length - 1; i > 0; i--)
        {
            for(int w = layers[i].weights.Length-1; w >= 0; w--)
            {
                layers[i].weights[w] = Random.Range(-2f, 2f);
            }
        }
    }
    public void NeuronCalculates()
    {
        for (int i = 1; i <= layers.Length - 1; i++)
        {
            //Debug.Log("Layer" + i);
            for(int n = 0; n <= layers[i].neurons.Length - 1; n++)
            {
                int sw = 0;

                List<float> weightsResults = new List<float>();
                for (int w = n * layers[i - 1].neurons.Length; w <= layers[i - 1].neurons.Length * n + layers[i -1].neurons.Length - 1; w++)
                {
                    //Debug.Log("Weight" + w + " - " + layers[i].weights[w]);
                    weightsResults.Add(layers[i - 1].neurons[sw] * layers[i].weights[w]);
                    sw++;
                }
                float currentNueronCalculateResult = 0;
                foreach (var item in weightsResults)
                {
                    currentNueronCalculateResult += item;
                }
                if(n != layers[i].neurons.Length - 1)
                {
                    currentNueronCalculateResult += 1f * Random.Range(-2f, 2f); //нейрон смещения
                }
                layers[i].neurons[n] = currentNueronCalculateResult;
                //Debug.Log("Neuron" + n + " - " + layers[i].neurons[n]);
            }
        }
    }
    public float OutputValues()
    {
        return layers[layers.Length - 1].neurons[0];
    }
    public Vector2 OutputValues(Vector2 v2)
    {
        return new Vector2(layers[layers.Length - 1].neurons[0], layers[layers.Length - 1].neurons[1]);
    }
    public Layer[] GetLayers()
    {
        return layers;
    }
}
