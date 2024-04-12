using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    public GameObject Obstacle1, Obstacle2, Obstacle3;
    public float obstacleSpawnInterval = 2.5f;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnObstacles");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Run>().isGameOver)
        {
            StopCoroutine("SpawnObstacles");
        }
    }

    private void SpanwObstacle()
    {
        int random = Random.Range(1, 4);
        if (random == 1)
        {
            Instantiate(Obstacle2, new Vector3(transform.position.x, -3.5f, 0), Quaternion.identity);
        }
        else if (random == 2)
        {
            Instantiate(Obstacle1, new Vector3(transform.position.x, -3.5f, 0), Quaternion.identity);
        }
        else if (random == 3)
        {
            GameObject obstacle = Instantiate(Obstacle3, new Vector3(transform.position.x, -3.80f, 0), Quaternion.identity);
            obstacle.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed; // Đặt tốc độ di chuyển về phía bên trái
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpanwObstacle();
            yield return new WaitForSeconds(obstacleSpawnInterval);
        }
    }
}
