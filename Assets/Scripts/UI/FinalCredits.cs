using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class FinalCredits : MonoBehaviour
    {
        public void RestartGame()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}