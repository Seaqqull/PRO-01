using UnityEngine;


namespace PotatoMode.Platforms.Data
{
    public interface IInteractable
    {
        void Visit(IConsumer participant);
    }

    public interface IConsumer
    {
        Transform Transform { get; }
        Rigidbody2D Body { get; }
    }
}