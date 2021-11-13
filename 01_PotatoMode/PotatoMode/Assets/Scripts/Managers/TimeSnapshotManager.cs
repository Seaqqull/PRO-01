using System.Collections.Generic;
using UnityEngine;


namespace PotatoMode.Managers
{
    public class TimeSnapshotManager : MonoBehaviour
    {
        public static TimeSnapshotManager Instance { get; private set; }

        private List<Memento.ILevelMemento> _snapshots
            = new List<Memento.ILevelMemento>();


        // Use this for initialization
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            DontDestroyOnLoad(this);
            Instance = this;
        }

        private void Update()
        {
            if (Input.InputHandler.Instance == null)
                return;

            if (Input.InputHandler.Instance.MakeSnapshot)
            {
                _snapshots.Add(LevelManager.Instance.MakeMemento());
                return;
            }
            if (_snapshots.Count == 0 || !Input.InputHandler.Instance.RestoreFromSnapshot)
                return;


            var lastSnapshot = _snapshots[_snapshots.Count - 1];
            _snapshots.RemoveAt(_snapshots.Count - 1);

            LevelManager.Instance.RecoverFromMemento(lastSnapshot);
        }
    }
}