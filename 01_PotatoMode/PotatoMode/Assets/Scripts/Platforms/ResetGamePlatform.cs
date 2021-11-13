using PotatoMode.Platforms.Data;
using PotatoMode.Managers;


namespace PotatoMode.Platforms
{
    public class ResetGamePlatform : Platform
    {
        protected override void OnExit(IConsumer consumer) { }
        
        protected override void OnEnter(IConsumer consumer)
        {
            LevelManager.Instance.EndLevel();
        }
    }
}