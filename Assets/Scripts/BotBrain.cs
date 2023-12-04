using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BotBrain : MonoBehaviour
{
    NN nN;
    [SerializeField] GameObject food;
    Controller controller;
    [SerializeField] float energy = 10f;
    [SerializeField] float starveSpeed = 0.5f;
    [SerializeField] float energyEnoughToBirthNewBot = 30f;
    [SerializeField] float mutateStrength = 0.1f;
    Food target;
    FoodSpawner foodSpawner;
    [SerializeField] public List<float> allWeights;
    private void Awake()
    {
        nN = new NN(4, 4);
        controller = GetComponent<Controller>();
        foodSpawner = FindObjectOfType<FoodSpawner>();
    }
    void Start()
    {
        SetWeights();
        //nN.SetRandomWeights();
        GetAllWeightsFromThisGen();
        FindFood();
        StartCoroutine(Starve());
    }
    IEnumerator Starve()
    {
        while (energy != 0)
        {
            energy = energy - starveSpeed;
            if (energy <= 0)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    void GetAllWeightsFromThisGen()
    {
        allWeights.Clear();
        for (int i = 0; i <= nN.Layer.Length - 1; i++)
        {
            if (nN.Layer[i].weights == null)
                continue;
            if (nN.Layer[i].weights.Length == 0)
                continue;
            allWeights.AddRange(nN.Layer[i].weights);
        }
    }
    void SetWeights()
    {
        if (allWeights == null)
        {
            nN.SetRandomWeights();
            return;
        }
        if (allWeights.Count == 0)
        {
            nN.SetRandomWeights();
            return;
        }
        int x = allWeights.Count - 1;

        for (int i = nN.Layer.Length - 1; i >= 0; i--)
        {
            if (nN.Layer[i].weights == null)
                continue;
            if (nN.Layer[i].weights.Length == 0)
                continue;
            for (int j = nN.Layer[i].weights.Length - 1; j >= 0; j--)
            {
                nN.Layer[i].weights[j] = allWeights[x];
                x--;
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            FindFood();
        }
        if (target != null)
        {
            nN.SetInputValues(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            nN.NeuronCalculates();
            controller.Move(nN.OutputValues(Vector2.zero));
        }
        if (energy <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nN.SetRandomWeights();
            GetAllWeightsFromThisGen();
        }
    }
    void EatFood(Food food)
    {
        energy += food.Eat();
        if (energy > energyEnoughToBirthNewBot)
        {
            energy = 10;
            CreateNewBot();
        }
        Destroy(food.gameObject);
        FindFood();
    }
    void CreateNewBot()
    {
        GameObject childBot = Instantiate(gameObject, gameObject.transform.position, Quaternion.identity, null);
        BotBrain childBotBrain = childBot.GetComponent<BotBrain>();
        MutateRandomGen(childBotBrain);
    }
    void MutateRandomGen(BotBrain botBrain)
    {
        int randomWeight = Random.Range(0, botBrain.allWeights.Count);
        botBrain.allWeights[randomWeight] = botBrain.allWeights[randomWeight] * Random.Range(-mutateStrength, mutateStrength);
        if (botBrain.allWeights[randomWeight] > 2f)
            botBrain.allWeights[randomWeight] = 2f;
        if (botBrain.allWeights[randomWeight] < -2f)
            botBrain.allWeights[randomWeight] = -2f;
    }
    void FindFood()
    {
        //target = FindObjectOfType<Food>();
        Food closestFood = null;
        foreach (var item in foodSpawner.foods)
        {
            if (closestFood == null)
            {
                closestFood = item;
                continue;
            }
            else
            {
                if (Vector2.Distance(item.transform.position, transform.position) < Vector2.Distance(closestFood.transform.position, transform.position))
                {
                    closestFood = item;
                }

            }
        }
        target = closestFood;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            EatFood(collision.gameObject.GetComponent<Food>());
        }
    }
}
