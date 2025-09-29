using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
   private LeverManager  _leverManager;

   
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
}
