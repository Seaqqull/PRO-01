using PotatoMode.Input;
using UnityEngine;


namespace PotatoMode.Players
{
    public class GroundedPlayerState : IPlayerState
    {
        public void OnUpdate(Player player)
        {
            player.MovementDirection = Mathf.Sign(InputHandler.Instance.Horizontal);
            player.View.localScale = (InputHandler.Instance.Horizontal == 0.0f)
                ? player.View.localScale : new Vector3(player.MovementDirection, 1, 1);


            if (InputHandler.Instance.Up)
                player.Jump();
            else if (InputHandler.Instance.Space)
                player.Dash();
        }
    }
}