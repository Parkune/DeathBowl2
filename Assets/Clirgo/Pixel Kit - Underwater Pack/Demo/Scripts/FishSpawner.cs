using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class FishSpawner : MonoBehaviour
{
    public List<Transform> fishes = new List<Transform>();

    public float minDelay;
    public float maxDelay;

    private float currentDelay = 0;
    private float selectedDelay = 0;

    public enum spawnDirections
    {
        left,
        right
    }
    public spawnDirections spawnDirection;

    //Get point in box collider

    public void SpawnFish()
    {
        Vector3 rotation;
        if (spawnDirection == spawnDirections.left) rotation = new Vector3(0, 0, 0);
        else rotation = new Vector3(0, 180, 0);

        BoxCollider2D zone = GetComponent<BoxCollider2D>();
        Transform fish = Instantiate(fishes[Random.Range(0, fishes.Count)], GetRandomPoint(zone.bounds), Quaternion.Euler(rotation));
        float scale = Random.Range(0.6f, 1f);
        fish.localScale = new Vector3(scale, scale, 1);
        fish.parent = transform;
        fish.GetComponent<FishMovement>().SetSpeed(Random.Range(0.6f, 2.5f));
    }

    private void Update()
    {
        if (currentDelay < selectedDelay) currentDelay += Time.deltaTime;
        else
        {
            SpawnFish();
            currentDelay = 0;
            selectedDelay = Random.Range(minDelay, maxDelay);
        }
        
    }

    private Vector3 GetRandomPoint(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
   




}
