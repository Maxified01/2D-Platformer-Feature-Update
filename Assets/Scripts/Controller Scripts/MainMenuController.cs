using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene-ALU");  // Changed from GameScene to GameScene-ALU
    }

}