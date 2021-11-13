using UnityEngine;


namespace PotatoMode.Managers
{
    public class CursorManipulator : MonoBehaviour
    {
        [SerializeField] private bool _cursorVisibility;


        private void Awake()
        {
            Cursor.visible = _cursorVisibility;
        }
    }
}