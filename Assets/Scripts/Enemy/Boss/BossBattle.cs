using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BossBattle : MonoBehaviour
{
    private Animator _animator;
    
    //Arrays for spawners and enemy types
    public GameObject[] spawners;
    public GameObject[] enemies;
    //public GameObject[] bulletSpawners;
    private BossHeart _bossHeart;
    //public GameObject bullet;
    public List<GameObject> enemiesList;
    
    
    //Timers
    public float spawnCooldown;
    public float vulnerableCooldown;
    public float vulnerableTime;

    //Fleshwall and heart
    public GameObject fleshWall;
    public GameObject heart;
    
    //Bools to control current phase
    public bool Phase1Active;
    public bool Phase2Active;
    
    
    //HealItem related
    public GameObject HealItem;
    private float _healCooldown = 10f;
    public Transform healSpawn;
    private PlayerController _player;

    public Animator fleshAnimator;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _bossHeart = FindFirstObjectByType<BossHeart>();
        _player = FindFirstObjectByType<PlayerController>();
        
        
        Phase1Active = true;
        
    }

    private void Update()
    {
        
        //Activates either phase 1 or 2
        if (_bossHeart.currentBossHealth > _bossHeart.maxBossHealth / 2)
        {
            Phase1Active = true;
            Phase2Active = false;
        }

        if (_bossHeart.currentBossHealth < _bossHeart.maxBossHealth / 2)
        {
            Phase1Active = false;
            Phase2Active = true;
        }
        
        
        if (Phase1Active)
        {
            Phase1();
        }
        if (Phase2Active)
        {
            Phase2();
        }

        
        if (Time.time > _healCooldown && _player.playerHealth < 2)
        {
            Instantiate(HealItem, healSpawn.position, Quaternion.identity);
            _healCooldown = Time.time + 10f;
        }

        if (_bossHeart.currentBossHealth <= 0)
        {
            Destroy(fleshWall);
            Phase1Active =  false;
            Phase2Active = false;
        }

        if (_bossHeart.isDead)
        {
            SceneManager.LoadScene("ChooseEnding");
        }
        
    }
    
    //Phase 1
    private void Phase1()
    {
        if (Time.time > spawnCooldown)
        {
            int randomSpawner = Random.Range(0, spawners.Length);
            int randomEnemy = Random.Range(0, enemies.Length);
        
        
            Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
        
            spawnCooldown = Time.time + 6f;
            
        }

        if (Time.time > vulnerableCooldown)
        {
            StartCoroutine(Vulnerable());
        }
        
    }

    //Phase 2
    private void Phase2()
    {

        if (Time.time > spawnCooldown)
        {
            int randomSpawner = Random.Range(0, spawners.Length);
            int randomEnemy = Random.Range(0, enemies.Length);
        
        
            Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
        
            spawnCooldown = Time.time + 6f;
            
        }
        
        
        
        if (Time.time > vulnerableCooldown && !_bossHeart.isDead)
        {
            StartCoroutine(Vulnerable());
        }
    }
    

    private IEnumerator Vulnerable()
    {
        fleshAnimator.Play("FleshWallOpen");
        
        yield return new WaitForSeconds(1f);
        fleshWall.SetActive(false);
        
        
        yield return new WaitForSeconds(vulnerableTime);
        
        fleshAnimator.Play("FleshWallClose");
        fleshWall.SetActive(true);
        vulnerableCooldown = Time.time + 20f;
        
    }

    
    
   
    

}
