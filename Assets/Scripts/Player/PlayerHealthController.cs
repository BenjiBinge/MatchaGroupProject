using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public PlayerController target;
    public Image[] hearts;
    
    
    public SpriteRenderer heartSprite1;
    public SpriteRenderer heartSprite2;
    public SpriteRenderer heartSprite3;
    public Sprite empty;
    public Sprite full;
    
    
    private void Update()
    {
        //Checks if the playerHealth is the same as the indexNumber
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < target.playerHealth)
            {
                hearts[i].sprite = full;
            }
            else
            {
                hearts[i].sprite = empty;
            }
        }
    }
}
