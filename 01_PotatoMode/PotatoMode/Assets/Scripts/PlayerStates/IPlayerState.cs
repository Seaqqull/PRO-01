using System;


namespace PotatoMode.Players
{
    public interface IPlayerState
    {
        void OnUpdate(Player player);
    }
}