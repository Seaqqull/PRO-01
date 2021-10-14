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

        public static InputHandler Instance
        {
            get; private set;
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
            if(Instance != null)
                Destroy(gameObject);
            Instance = this;
        }
        
        private void Update()
        {
            _horizontal = EngineInput.GetAxis("Horizontal");

            _shift = EngineInput.GetKey(KeyCode.LeftShift) || EngineInput.GetKey(KeyCode.RightShift);
            _up = EngineInput.GetKey(KeyCode.W) || EngineInput.GetKey(KeyCode.UpArrow);
            _space = EngineInput.GetKey(KeyCode.Space);
        }
    }
}