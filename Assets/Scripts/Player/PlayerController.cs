using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
   //Various important components
   private InputManager _input;
   private Rigidbody2D _rigidbody;
   public GameObject AttackHitboxHorizontal;
   public GameObject AttackHitboxVertical;
   
   public float moveSpeed;

   public float playerHealth;
   
   //Attack related variables
   public bool isAttacking;
   public bool isChargeAttacking;
   private float _attackCooldown = 5f;
   private bool _canAttack = true;

   
   
   
   private void Start()
   {
      _input = GetComponent<InputManager>();
      _rigidbody = GetComponent<Rigidbody2D>();
   }


   private void FixedUpdate()
   {
      //Vertical + Horizontal movement
      _rigidbody.linearVelocityX = _input.Horizontal * moveSpeed;
      _rigidbody.linearVelocityY = _input.Vertical * moveSpeed;
   }


   private void Update()
   {
      
      //Changes the player direction based on input
      if (_input.Horizontal != 0)
      {
         transform.localScale = new Vector2(Mathf.Sign(_input.Horizontal), 1f);
      }

      //Vertical direction is WiP
      if (_input.Vertical != 0)
      {
         transform.localScale = new Vector2(1f, Mathf.Sign(_input.Vertical));
      }
      
      Attack();

      
      //Dies if Hp <= 0
      if (playerHealth <= 0)
      {
         RestartScene();
      }
   }

   
   //Restarts the current scene
   private void RestartScene()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
      if (_input.Horizontal != 0)
      {
         _canAttack = false;
         AttackHitboxHorizontal.SetActive(true);
         yield return new WaitForSeconds(0.5f);
         AttackHitboxHorizontal.SetActive(false);
         _canAttack = true;
      }

      if (_input.Vertical != 0)
      {
         _canAttack = false;
         AttackHitboxVertical.SetActive(true);
         yield return new WaitForSeconds(0.5f);
         AttackHitboxVertical.SetActive(false);
         _canAttack = true;
      }
   }
   
   
}
