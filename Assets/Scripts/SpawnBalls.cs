using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public Transform ballSpawnPoint;
    public GameObject ballPrefab;
    public List<Material> ballMaterials;
    public float spawnRadius = 0.6f;

    int ballMaterialIndex = 0;

    void Start()
    {
        Shuffle(ballMaterials);

        for (int i = 0; i < 20; i++)
        {
            SpawnBall();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }
    }

    void SpawnBall()
    {
        float xOffset = Random.Range(-spawnRadius, spawnRadius);
        float yOffset = Random.Range(-spawnRadius, spawnRadius);
        float zOffset = Random.Range(-spawnRadius, spawnRadius);
        Vector3 spawnPoint = new Vector3(ballSpawnPoint.position.x + xOffset, ballSpawnPoint.position.y + yOffset,
                                         ballSpawnPoint.position.z + zOffset);

        GameObject ball = Instantiate(ballPrefab, spawnPoint, ballSpawnPoint.rotation);
        MeshRenderer ballMeshRenderer = ball.GetComponentInChildren<MeshRenderer>();
        ballMeshRenderer.material = pickRandomMaterial();
    }

    Material pickRandomMaterial()
    {
        var material = ballMaterials[ballMaterialIndex];
        ballMaterialIndex++;
        if (ballMaterialIndex >= ballMaterials.Count)
        {
            ballMaterialIndex = 0;
            Shuffle(ballMaterials);
        }
        return material;
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}