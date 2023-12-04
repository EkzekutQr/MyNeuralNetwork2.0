using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] Food foodPrefab;
    public List<Food> foods;
    private void Start()
    {
        foods = new List<Food>();
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            Food newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity, null);
            foods.Add(newFood);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
