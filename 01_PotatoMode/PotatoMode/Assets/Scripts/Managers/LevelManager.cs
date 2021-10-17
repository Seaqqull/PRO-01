using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using TMPro;


namespace PotatoMode.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countdownText;
        [SerializeField] private Animator _levelAnimator;
        [Header("End-game")]
        [SerializeField] private TMP_Text _endTimeText;
        [SerializeField] private float _endGameTime;
        [SerializeField] private int _gameIndex;

        private Coroutine _levelCoroutine;
        private float _gameTime;
        
        public static LevelManager Instance { get; private set; }

        public bool LevelEnded { get; private set; }


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            _levelCoroutine = StartCoroutine(LevelRoutine());
        }

        private void OnDestroy()
        {
            if (_levelCoroutine == null)
                return;
            StopCoroutine(_levelCoroutine);
            _levelCoroutine = null;
        }
        
        
        private IEnumerator LevelRoutine()
        {
            // During game
            yield return InGameRoutine();
            // When finished the level
            yield return EndGameRoutine();

            _levelCoroutine = null;
        }

        private IEnumerator InGameRoutine()
        {
            do
            {
                _gameTime += Time.deltaTime;
                SetCountdownTime(_gameTime);
                
                yield return null;
            } while (!LevelEnded);    
        }
        
        private IEnumerator EndGameRoutine()
        {
            var endTime = _endGameTime;
            _levelAnimator.SetTrigger(Utilities.Constants.Animation.GAME_END);
            
            do
            {
                endTime -= Time.deltaTime;
                _endTimeText.text = ((int) endTime).ToString();
                
                yield return null;
            } while (endTime > 0.0f);
            
            SceneManager.LoadScene(_gameIndex);
        }
        
        public void SetCountdownTime(float amount)
        {
            _countdownText.text = GetFormattedTime(amount);
        }
        
        public string GetFormattedTime(float time)
        {
            var milliseconds = (time * 1000) % 1000;

            var seconds = (int)time;
            var minutes = seconds / 60;
            seconds = (minutes == 0)? seconds : (seconds % (minutes * 60));

            return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        }


        public void EndLevel()
        {
            LevelEnded = true;
        }
    }
}