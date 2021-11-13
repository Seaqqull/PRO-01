using System;


namespace PotatoMode.Managers.Memento
{
    public class LevelSnapshot : ILevelMemento
    {
        public DateTime SnapshotDate { get; }
        public float Time { get; }


        public LevelSnapshot(float time)
        {
            SnapshotDate = DateTime.UtcNow;
            Time = time;
        }
    }
}