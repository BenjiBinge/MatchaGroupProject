using UnityEngine;
using System.Collections;

public class EnemySpawnControl : MonoBehaviour
{
    //Array of spawn points and enemies (total 3)
    public GameObject[] spawners;
    public GameObject[] enemies;
    private float spawnCooldown = 1f;


    private void Update()
    {
        //Cooldown for spawning
        if (Time.time > spawnCooldown)
        {
            StartCoroutine(SpawnEnemy());
        }
    }
    
    
    //Spawns a random enemy from the arrays
    private IEnumerator SpawnEnemy()
    {
        int randomSpawner = Random.Range(0, spawners.Length);
        int randomEnemy = Random.Range(0, enemies.Length);
        
        
        Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
        
        spawnCooldown = Time.time + spawnCooldown;
        yield return new WaitForSeconds(1f);
    }
}
