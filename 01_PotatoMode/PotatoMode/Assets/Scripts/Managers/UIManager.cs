using UnityEngine.UI;
using UnityEngine;
using TMPro;


namespace PotatoMode.Managers
{
    public class UIManager : MonoBehaviour
    {
        #region CONSTANTS
        private static readonly Color EMPTY_NAME_COLOR = new Color(0.65f, 0.152f, 0.152f);
        private static readonly Color FILLED_NAME_COLOR = Color.white;
        #endregion

        [SerializeField] private GameObject _additionalMenu;
        [SerializeField] private SceneLoader _gameScene;
        [Header("Additiona-menu")]
        [SerializeField] private Toggle _termsToggle;
        [SerializeField] private TMP_Dropdown _difficultySelection;
        [SerializeField] private TMP_InputField _nameInput;

        private Image _nameBackgoround;
        private string _userName;

        public static UIManager Instance { get; private set; }


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }


            Instance = this;

            if (_nameInput == null)
                return;

            _nameInput.onValueChanged.AddListener(NameValueChanged);
            _nameBackgoround = _nameInput.GetComponent<Image>();            
        }


        private void NameValueChanged(string newName)
        {
            _nameBackgoround.color = (string.IsNullOrEmpty(newName)) ?
                EMPTY_NAME_COLOR : FILLED_NAME_COLOR;

            _userName = newName;
        }
    

        public void Quit()
        {
            Application.Quit();
        }

        public void LoadGame()
        {
            if (!string.IsNullOrEmpty(_userName))
                _gameScene.LoadScene();
        }

        public void ShowAdditionalMenu()
        {
            _additionalMenu.SetActive(true);
        }

        public void HideAdditionalMenu()
        {
            _additionalMenu.SetActive(false);
            _termsToggle.isOn = false;

            _difficultySelection.value = 0;
            _nameInput.Select();
            _nameInput.text = "";
        }
    }
}