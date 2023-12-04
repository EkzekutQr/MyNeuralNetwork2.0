using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float foodEnergy = 10f;
    public float Eat()
    {
        FindObjectOfType<FoodSpawner>().foods.Remove(this);
        return foodEnergy;
    }
}
