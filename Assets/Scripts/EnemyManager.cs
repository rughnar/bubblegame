
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;

public class EnemyManager : MonoBehaviour
{
    public List<int> enemiesPriority;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform spawnPointRangeStart;
    [SerializeField] private Transform spawnPointRangeEnd;
    [SerializeField] private float timeBetweenSpawns = 4f;
    public int maxEnemiesQty = 100;
    private GameManager gameManager;
    [SerializeField] private int currentEnemiesQty = 0;
    //public int quantityEnemiesDestroyed = 0;
    private float lastEnemySpawnTime = 0f;

    private float spawnMinX, spawnMaxX, spawnMinY, spawnMaxY;


    System.Random rnd = new System.Random();
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        lastEnemySpawnTime = 0f;
        //currentSpawnedEnemies = new List<int>();

    }

    void FixedUpdate()
    {
        if (Time.fixedTime - lastEnemySpawnTime > timeBetweenSpawns)
        {
            Spawn(1);
            lastEnemySpawnTime = Time.fixedTime;
        }
    }

    void Spawn(int quantity)
    {
        if (currentEnemiesQty < maxEnemiesQty)
        {
            SpawnRandomEnemy();
        }
        else
        {
            Debug.Log("Cantidad de enemigos maxima alcanzada");
        }
    }

    public void EnemyTakenDown()
    {
        currentEnemiesQty -= 1;
    }

    public Vector2 GetRandomSpawnPoint()
    {
        spawnMinX = Math.Min(spawnPointRangeStart.position.x, spawnPointRangeEnd.position.x);
        spawnMaxX = Math.Max(spawnPointRangeStart.position.x, spawnPointRangeEnd.position.x);
        spawnMinY = Math.Min(spawnPointRangeStart.position.y, spawnPointRangeEnd.position.y);
        spawnMaxY = Math.Max(spawnPointRangeStart.position.y, spawnPointRangeEnd.position.y);
        return new Vector2(UnityEngine.Random.Range(spawnMinX, spawnMaxX), UnityEngine.Random.Range(spawnMinY, spawnMaxY));
    }


    private void SpawnRandomEnemy()
    {

        GameObject enemy = Instantiate(GetObjectWithMaxProb(), GetRandomSpawnPoint(), Quaternion.identity);
        enemy.GetComponent<EnemyController>().FaceCenter();
        currentEnemiesQty += 1;
        /*if (enemy.CompareTag("HatWielder"))
        {
            enemy.GetComponent<SpriteRenderer>().sprite = enemySprites[UnityEngine.Random.Range(0, enemySprites.Count)];
            if (UnityEngine.Random.Range(0, 10) > 3)
            {
                GameObject hat = GetHatWithMaxProb();
                if (hat != null)
                {
                    GameObject hat2 = Instantiate(hat, Vector3.zero, Quaternion.identity);
                    //enemy.GetComponentInChildren<HatHolder>().AddHat(hat2);
                }
            }
        }*/

        //int index = Mathf.Abs(Random.Range(0, enemyPrefabs.Count));

    }
    /*
        private void SpawnRandomEnemyRecursively(int minusIndex)
        {
            int index = Mathf.Abs(Random.Range(minusIndex, enemyPrefabs.Count));
            if (currentSpawnedEnemies[index] < enemiesQuantity[index])
            {
                GameObject enemy = Instantiate(enemyPrefabs[index], spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
                enemy.GetComponent<EnemyController>().FaceCenter();
                currentSpawnedEnemies[index] += 1;
            }
            else
            {
                SpawnRandomEnemyRecursively(minusIndex - 1);
            }
        }
    */
    GameObject GetObjectWithMaxProb()
    {
        int totalWeight = enemiesPriority.Sum(); // Using LINQ for suming up all the values
        int randomNumber = rnd.Next(0, totalWeight);

        GameObject myGameObject = null;
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            if (randomNumber < enemiesPriority[i])
            {
                myGameObject = enemyPrefabs[i];
                break;
            }
            randomNumber -= enemiesPriority[i];
        }
        return myGameObject;
    }

    /*GameObject GetHatWithMaxProb()
    {
        int totalWeight = hats.Sum(h => h.spawnPriority); // Using LINQ for suming up all the values
        int randomNumber = rnd.Next(0, totalWeight);

        GameObject myGameObject = null;
        foreach (HatController hat in hats)
        {
            if (randomNumber < hat.spawnPriority)
            {
                myGameObject = hat.gameObject;
                break;
            }
            randomNumber -= hat.spawnPriority;
        }
        return myGameObject;
    }
*/
}
