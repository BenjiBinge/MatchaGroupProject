using UnityEngine;
using System.Collections;

public class EnemySpawnControl : MonoBehaviour
{
    //Array of spawn points and enemies (total 3)
    public GameObject[] spawners;
    public GameObject[] enemies;
    private float spawnCooldown = 1f;
    public float spawnRadius = 10f;
    private Transform _target;

    private void Start()
    {
        _target = GameObject.Find("Player").transform;
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        
    }
    
    private void Update()
    {
        //Cooldown for spawning
        if (Time.time > spawnCooldown && Vector2.Distance(_target.position, transform.position) < spawnRadius)
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
