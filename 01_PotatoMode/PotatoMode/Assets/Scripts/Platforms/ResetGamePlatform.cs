using UnityEngine.SceneManagement;
using PotatoMode.Platforms.Data;
using UnityEngine;


namespace PotatoMode.Platforms
{
    public class ResetGamePlatform : Platform
    {
        [SerializeField] private int _sceneIndex;
        
        
        protected override void OnExit(IConsumer consumer) { }
        
        protected override void OnEnter(IConsumer consumer)
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}