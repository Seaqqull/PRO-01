using UnityEngine;


namespace UPotatoMode.tilities
{
    public class ScenePoint : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] protected float _size = 0.3f;
        [SerializeField] protected Color _color = Color.black;
        
#pragma warning restore 0649

        protected Color _gizmoDefault;

        public Color Color
        {
            get => _color;
        }


        protected void OnDrawGizmos()
        {
            _gizmoDefault = Gizmos.color;

            Gizmos.color = Color;
            Gizmos.DrawWireSphere(transform.position, _size);

            Gizmos.color = _gizmoDefault;
        }
    }
}