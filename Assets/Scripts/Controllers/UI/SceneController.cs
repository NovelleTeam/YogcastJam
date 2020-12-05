using UnityEngine;

namespace Controllers.UI
{
    public class SceneController : MonoBehaviour
    {
        public static void SwitchScene(int sceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }

        public static void SwitchScene(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        public static void Quit()
        {
            Application.Quit();
        }
    }
}