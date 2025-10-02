using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
   private LeverManager  _leverManager;
   public Sprite notOpened;
   public Sprite Opened;
   public SpriteRenderer doorSprite;

   
   private void Start()
   {
      _leverManager = FindFirstObjectByType<LeverManager>();
   }
   
   //When player enters the door after already flipping a lever
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Player") && _leverManager.isFlipped)
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
   }

   private void Update()
   {
      if (!_leverManager.isFlipped)
      {
         doorSprite.sprite = notOpened;
      }
      else if (_leverManager.isFlipped)
      {
         doorSprite.sprite = Opened;
      }
   }
}
