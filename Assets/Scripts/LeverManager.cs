using Unity.VisualScripting;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
   public bool isFlipped;
   
   //"Flips" the lever
  private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("AttackHitbox"))
      {
         isFlipped = true;
      }
   }

 
}
