using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
   //Various important components
   private InputManager _input;
   private Rigidbody2D _rigidbody;
   public GameObject AttackHitboxHorizontal;
   public GameObject AttackHitboxVertical;
   
   private LeverManager _leverManager;
   
   //Speed
   public float moveSpeed;
   public float chargeSpeed;
   public float moveDirection;
   
   //Health and Damage related variables
   public float playerHealth;
   public float damageCooldown;
   private float _damageCooldownTimer;
   public bool isKnockbacked;
   
   //Attack related variables
   public bool isAttacking;
   public bool isChargeAttacking;
   private bool _canAttack = true;

   //Charge attack timer variables
   public float chargeWait = 2f;
   private float _chargeTimer = 2f;

   
   private void Start()
   {
      _input = GetComponent<InputManager>();
      _rigidbody = GetComponent<Rigidbody2D>();
   }


   private void FixedUpdate()
   {
      //Vertical + Horizontal movement
      if (!isChargeAttacking && !isKnockbacked)
      {
         _rigidbody.linearVelocityX = _input.Horizontal * moveSpeed;
         _rigidbody.linearVelocityY = _input.Vertical * moveSpeed;
      }
      
   }


   private void Update()
   {
      //Keeps stock of what the last direction you moved in was ^ = -1, v = -3, < = 1, > = 3
      if (_input.Horizontal != 0)
      {
         moveDirection = _input.Horizontal + 2f;
      }
      if (_input.Vertical != 0)
      {
         moveDirection = _input.Vertical - 2f;
      }
      
      //Changes the player direction based on input
      if (_input.Horizontal != 0 && !isChargeAttacking)
      {
         transform.localScale = new Vector2(Mathf.Sign(_input.Horizontal), 1f);
      }
      if (_input.Vertical != 0 && !isChargeAttacking)
      {
         transform.localScale = new Vector2(1f, Mathf.Sign(_input.Vertical));
      }
      
      //Forces four-directional movement. No Diagonal!
      if (_input.Horizontal != 0 && _input.Vertical != 0)
      {
         _input.Horizontal = 0;
         _input.Vertical = 0;
      }
      
      
      Attack();
      
      
      //Counts down the timer when player is holding down attack button
      if (_input.ChargeAttack && _chargeTimer > 0)
      {
         _chargeTimer -= 1f * Time.deltaTime;;
      }
      if (_input.ChargeAttack && _chargeTimer <= 0 && _canAttack)
      {
         //Starts the Charge Attack
         StartCoroutine(ChargeAttacking());
      }
      else if(!_input.ChargeAttack)
      {
         _chargeTimer = chargeWait;
      }
      
      
      
   }

   
   //Restarts the current scene
   private void RestartScene()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }

   private void DeathScreen()
   {
      SceneManager.LoadScene("DeathScreen");
   }
   
   
   //Attack function
   private void Attack()
   {
      if (_input.Attack && _canAttack)
      {
         isAttacking = true;
         StartCoroutine(Attacking());
      }
   }
   
   //For basic attack
   private IEnumerator Attacking()
   {
      if (moveDirection == 1 || moveDirection == 3)
      {
         _canAttack = false;
         AttackHitboxHorizontal.SetActive(true);
         yield return new WaitForSeconds(0.5f);
         AttackHitboxHorizontal.SetActive(false);
         _canAttack = true;
      }

      if (moveDirection == -1 || moveDirection == -3)
      {
         
         _canAttack = false;
         AttackHitboxVertical.SetActive(true);
         yield return new WaitForSeconds(0.5f);
         AttackHitboxVertical.SetActive(false);
         _canAttack = true;
      }
      
   }
   
   
   //Charge attack WiP. DO NOT TOUCH!
   private IEnumerator ChargeAttacking()
   {
      //Horizontal charging
      if (moveDirection == 1 || moveDirection == 3)
      {
         isChargeAttacking = true;
         _rigidbody.linearVelocityX += (moveDirection - 2) * chargeSpeed;
         _canAttack = false;
         AttackHitboxHorizontal.SetActive(true);
         
         yield return new WaitForSeconds(0.5f);
         AttackHitboxHorizontal.SetActive(false);
      }
      
      //Vertical charging
      if (moveDirection == -1 || moveDirection == -3)
      {
         isChargeAttacking = true;
         _rigidbody.linearVelocityY += (moveDirection + 2) * chargeSpeed;
         _canAttack = false;
         AttackHitboxVertical.SetActive(true);
         
         yield return new WaitForSeconds(0.5f);
         AttackHitboxVertical.SetActive(false);
      }
      
      _canAttack = true;
      isChargeAttacking = false;
      isAttacking = false;
      _chargeTimer = chargeWait;
   }

   //Collision
   private void OnCollisionEnter2D(Collision2D other)
   {
      //When player collides with anything with the "Enemy" tag
      if (other.gameObject.CompareTag("Enemy"))
      {
         StartCoroutine(TakeDamage());
      }
   }
   
   
   //For taking damage
   private IEnumerator TakeDamage()
   {
      //You take damage + knockback
      if (Time.time > _damageCooldownTimer && moveDirection == 1 || moveDirection == 3)
      {
         isKnockbacked = true;
         playerHealth -= 1;
         _damageCooldownTimer = Time.time + damageCooldown;
         _rigidbody.linearVelocityX -= moveSpeed * 2f;
         
         yield return new WaitForSeconds(0.2f);
         isKnockbacked = false;
         //Player dies
         if (playerHealth <= 0)
         {
            DeathScreen();
         }
      }
      if (Time.time > _damageCooldownTimer && moveDirection == -1 || moveDirection == -3)
      {
         isKnockbacked = true;
         playerHealth -= 1;
         _damageCooldownTimer = Time.time + damageCooldown;
         _rigidbody.linearVelocityY -= moveSpeed * 2f;
         
         yield return new WaitForSeconds(0.2f);
         isKnockbacked = false;
         //Player dies
         if (playerHealth <= 0)
         {
            DeathScreen();
         }
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      //When player interacts with heal item
      if (other.gameObject.CompareTag("Heal") && playerHealth < 3)
      {
         playerHealth++;
         Destroy(other.gameObject);
      }
   }
   
}
