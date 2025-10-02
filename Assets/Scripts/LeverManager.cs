using Unity.VisualScripting;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
   public bool isFlipped;
   public Sprite notFlipped;
   public Sprite flipped;
   public SpriteRenderer leverSprite;
   
   
   //"Flips" the lever
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("AttackHitbox"))
      {
         isFlipped = true;
         leverSprite.sprite = flipped;
         
      }
   }

 
}
