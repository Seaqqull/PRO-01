using UnityEngine;


namespace PotatoMode.Platforms.Data
{
    public interface IInteractable
    {
        void Visit(IConsumer participant);
    }

    public interface IConsumer
    {
        Rigidbody2D Body { get; }

        Transform Transform { get; }
        // public void Interact(Coin coin);
        // public void Interact(Obstacle obstacle);
    }
}