using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossBattle : MonoBehaviour
{
    private Animator _animator;
    
    //Arrays for spawners and enemy types
    public GameObject[] spawners;
    public GameObject[] enemies;
    public GameObject[] bulletSpawners;
    private BossHeart _bossHeart;
    public GameObject bullet;
    public List<GameObject> enemiesList;

    public bool bulletExist;
    
    //Timers
    public float spawnCooldown;
    public float vulnerableCooldown;
    public float vulnerableTime;
    public float bulletCooldown;

    //Fleshwall and heart
    public GameObject fleshWall;
    public GameObject heart;
    
    //Bools to control current phase
    public bool Phase1Active;
    public bool Phase2Active;

    //Max and current amount of enemies
    public float activeEnemies;
    public float maxEnemies;
    
    private FollowEnemy _follow;
    private RangeEnemy  _range;
    private ChargeEnemy  _charge;
    
    //HealItem related
    public GameObject HealItem;
    private float _healCooldown = 10f;
    public Transform healSpawn;
    private PlayerController _player;



    private void Start()
    {
        _animator = GetComponent<Animator>();
        _bossHeart = GetComponentInChildren<BossHeart>();
        _player = FindFirstObjectByType<PlayerController>();

        _follow = FindFirstObjectByType<FollowEnemy>();
        _range = FindFirstObjectByType<RangeEnemy>();
        _charge = FindFirstObjectByType<ChargeEnemy>();

        /*_follow.chaseRange = 50f;
        _follow.enemyHealth = 1f;
        
        _range.chaseRange = 50f;
        _range.enemyHealth = 1f;
        
        _charge.chaseRange = 50f;
        _charge.enemyHealth = 1f;*/

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

        if (Time.time > _healCooldown && _player.playerHealth < 3)
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
        
    }
    
    //Phase 1
    private void Phase1()
    {
        if (Time.time > spawnCooldown)
        {
            int randomSpawner = Random.Range(0, spawners.Length);
            int randomEnemy = Random.Range(0, enemies.Length);
        
        
            Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
        
            spawnCooldown = Time.time + spawnCooldown;
            
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
        
            spawnCooldown = Time.time + spawnCooldown;
            //activeEnemies++;
            //enemiesList.Add(clone);
            
        }
        
        if (Time.time > bulletCooldown)
        {
            StartCoroutine(Shoot());
        }
        
        if (Time.time > vulnerableCooldown)
        {
            StartCoroutine(Vulnerable());
        }
    }
    

    private IEnumerator Vulnerable()
    {
        
        fleshWall.SetActive(false);

        if (_bossHeart.currentBossHealth <= 0)
        {
            yield return new WaitForSeconds(100f);
        }
        
        yield return new WaitForSeconds(vulnerableTime);
        fleshWall.SetActive(true);
        vulnerableCooldown = Time.time + vulnerableTime;
    }


    private IEnumerator Shoot()
    {
        bulletExist = true;
        bulletCooldown = Time.time + bulletCooldown;
        int randomBulletSpawner = Random.Range(0, bulletSpawners.Length);

        var clone = Instantiate(bullet, bulletSpawners[randomBulletSpawner].transform.position, Quaternion.identity);
        
        clone.TryGetComponent(out Rigidbody2D _rigidbody2D);
        _rigidbody2D.linearVelocityY -= 3f;
        
        yield return new WaitForSeconds(2f);
        bulletExist = false;
        
    }

    

}
