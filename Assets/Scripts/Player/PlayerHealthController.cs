using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public PlayerController target;
    public Image[] hearts;
    
    private void Update()
    {
        //Checks if the playerHealth is the same as the indexNumber
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < target.playerHealth)
            {
                hearts[i].color = new Color(1, 0, 0, 1);
            }
            else
            {
                hearts[i].color = new Color(1, 0, 0,0.5f);
            }
        }
    }
}
