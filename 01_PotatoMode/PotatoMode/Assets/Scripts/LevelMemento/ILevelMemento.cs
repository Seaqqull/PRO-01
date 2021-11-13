using System;


namespace PotatoMode.Managers.Memento
{
    public interface ILevelMemento
    {
        DateTime SnapshotDate { get; }
        float Time { get; }
    }
}