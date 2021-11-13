using EngineInput = UnityEngine.Input;
using UnityEngine;


namespace PotatoMode.Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private float _horizontal;
        [SerializeField] private bool _space;
        [SerializeField] private bool _shift;
        [SerializeField] private bool _up;
        [Header("Snapshots")]
        [SerializeField] private bool _makeSnapshot;
        [SerializeField] private bool _restoreFromSnapthot;

        public static InputHandler Instance
        {
            get; private set;
        }

        public bool RestoreFromSnapshot
        {
            get => _restoreFromSnapthot;
        }
        public bool MakeSnapshot
        {
            get => _makeSnapshot;
        }
        public float Horizontal
        {
            get => _horizontal;
        }
        public bool Space
        {
            get => _space;
        }
        public bool Shift
        {
            get => _shift;
        }
        public bool Up
        {
            get => _up;
        }


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
                
            Instance = this;
        }
        
        private void Update()
        {
            _horizontal = EngineInput.GetAxis("Horizontal");

            _restoreFromSnapthot = EngineInput.GetKeyDown(KeyCode.LeftArrow);
            _makeSnapshot = EngineInput.GetKey(KeyCode.RightArrow);

            _shift = EngineInput.GetKey(KeyCode.LeftShift) || EngineInput.GetKey(KeyCode.RightShift);
            _up = EngineInput.GetKey(KeyCode.W) || EngineInput.GetKey(KeyCode.UpArrow);
            _space = EngineInput.GetKey(KeyCode.Space);
        }
    }
}