using UnityEngine;

public class Bullet : MonoBehaviour
{
   public float moveSpeed;
   private Rigidbody2D _rigidbody;
   private float _despawnTimer = 5f;

   private void Start()
   {
      _rigidbody = GetComponent<Rigidbody2D>();
      
      Destroy(gameObject, _despawnTimer);
   }

   private void Update()
   {
     
   }
}
