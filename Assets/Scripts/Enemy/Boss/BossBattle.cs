using UnityEngine;
using System.Collections;

public class BossBattle : MonoBehaviour
{
    private Animator _animator;
    
    //Arrays for spawners and enemy types
    public GameObject[] spawners;
    public GameObject[] enemies;
    
    
    //Timers
    public float spawnCooldown;
    public float vulnerableCooldown;
    public float vulnerableTime;

    //Fleshwall and heart
    public GameObject fleshWall;
    public GameObject heart;
    
    //Bools to control current phase
    public bool battleStarted;
    public bool Phase1Active;
    public bool Phase2Active;

    //Max and current amount of enemies
    public float activeEnemies;
    public float maxEnemies;



    private void Start()
    {
        _animator = GetComponent<Animator>();
        Phase1Active = true;
    }

    private void Update()
    {
        if (Phase1Active)
        {
            Phase1();
        }
    }
    
    //Phase 1
    private void Phase1()
    {
        if (Time.time > spawnCooldown && activeEnemies < maxEnemies)
        {
            int randomSpawner = Random.Range(0, spawners.Length);
            int randomEnemy = Random.Range(0, enemies.Length);
        
        
            Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
        
            spawnCooldown = Time.time + spawnCooldown;
            activeEnemies++;
        }

        if (Time.time > vulnerableCooldown)
        {
            StartCoroutine(Vulnerable());
        }
        
        
    }


    private IEnumerator Vulnerable()
    {
        
        fleshWall.SetActive(false);
        
        yield return new WaitForSeconds(vulnerableTime);
        fleshWall.SetActive(true);
        vulnerableCooldown = Time.time + vulnerableTime;
    }
    
    




}
