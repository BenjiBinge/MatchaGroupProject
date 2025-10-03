using UnityEngine;

public class Bullet : MonoBehaviour
{
   public float moveSpeed;
   private Rigidbody2D _rigidbody;
   private float _despawnTimer = 5f;
   

   private void Start()
   {
      _rigidbody = GetComponent<Rigidbody2D>();
      
      
      //Destroys the bullet after a said amount of time
      Destroy(gameObject, _despawnTimer);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("AttackHitbox"))
      {
         Destroy(gameObject);
      }
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Death"))
      {
         Destroy(gameObject);
      }

      if (other.gameObject.CompareTag("Enemy"))
      {
         Destroy(gameObject);
      }
   }

  
}
