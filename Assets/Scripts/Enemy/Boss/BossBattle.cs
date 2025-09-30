using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossBattle : MonoBehaviour
{
    private Animator _animator;
    
    //Arrays for spawners and enemy types
    public GameObject[] spawners;
    public GameObject[] enemies;
    private BossHeart _bossHeart;
    public List<GameObject> enemiesList;
    
    
    //Timers
    public float spawnCooldown;
    public float vulnerableCooldown;
    public float vulnerableTime;

    //Fleshwall and heart
    public GameObject fleshWall;
    public GameObject heart;
    public GameObject deathWall1;
    public GameObject deathWall2;
    
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
        _bossHeart = GetComponentInChildren<BossHeart>();
        //Phase1Active = true;
    }

    private void Update()
    {
        //Checks if the boss' health is more or less than its halfway point
        if (_bossHeart.currentBossHealth > (_bossHeart.maxBossHealth / 2))
        {
            Phase1Active = true;
            Phase2Active = false;
        }
        if (_bossHeart.currentBossHealth < (_bossHeart.maxBossHealth / 2))
        {
            Phase2Active = true;
            Phase1Active = false;
        }
        
        //Activates either phase 1 or 2
        if (Phase1Active)
        {
            Phase1();
        }

        if (Phase2Active)
        {
            Phase2();
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
            //enemiesList.Add(clone);
            
        }

        if (Time.time > vulnerableCooldown)
        {
            StartCoroutine(Vulnerable());
        }
        
        
    }

    private void Phase2()
    {
        deathWall1.SetActive(true);
        deathWall2.SetActive(true);
        
        
    }
    

    private IEnumerator Vulnerable()
    {
        
        fleshWall.SetActive(false);
        
        
        
        yield return new WaitForSeconds(vulnerableTime);
        fleshWall.SetActive(true);
        vulnerableCooldown = Time.time + vulnerableTime;
    }
    
    




}
