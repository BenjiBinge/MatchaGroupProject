using UnityEngine;

public class Bullet : MonoBehaviour
{
   public float moveSpeed;
   private Rigidbody2D _rigidbody;

   private void Start()
   {
      _rigidbody = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
     
   }
}
