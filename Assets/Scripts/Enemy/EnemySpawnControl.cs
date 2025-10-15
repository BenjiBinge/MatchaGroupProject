using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class EnemySpawnControl : MonoBehaviour
{
    //Array of spawn points and enemies (total 3)
    public GameObject[] spawners;
    public GameObject[] enemies;
    public float maxSpawnCount;
    private float spawnCooldown = 5f;
    public float spawnRadius = 10f;
    private Transform _target;
    
    public List <GameObject> enemiesList;
    

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
            if (enemiesList == null || enemiesList.Count < maxSpawnCount)
            {
                StartCoroutine(SpawnEnemy());
            }
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            print(enemiesList);
        }
    }
    
    
    //Spawns a random enemy from the arrays
    private IEnumerator SpawnEnemy()
    {
        int randomSpawner = Random.Range(0, spawners.Length);
        int randomEnemy = Random.Range(0, enemies.Length);
        
        GameObject clone = Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
        if (clone.TryGetComponent(out ChargeEnemy charge))
        {
            charge._spawn = this;
        }
        else if (clone.TryGetComponent(out FollowEnemy follow))
        {
            follow._spawn = this;
        }
        else if (clone.TryGetComponent(out RangeEnemy ranged))
        {
            ranged._spawn = this;
        }
        enemiesList.Add(clone);
        
        spawnCooldown = Time.time + 5;
        yield return new WaitForSeconds(1f);
    }
}
