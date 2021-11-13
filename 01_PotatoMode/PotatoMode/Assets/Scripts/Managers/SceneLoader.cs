using UnityEngine.SceneManagement;
using UnityEngine;


namespace PotatoMode.Managers
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private int _sceneIndex;



        public void LoadScene()
        {
            SceneManager.LoadScene(_sceneIndex);
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
