using UnityEngine.UI;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject _window;

    [SerializeField]
    private Text _lable;

    [SerializeField]
    private Button _restartButton;

    private GameRule _gameRule;

    private void Awake()
    {
        _restartButton.onClick.AddListener(Restart);
    }

    [Inject]
    public void Construct(GameRule gameRule)
    {
        _gameRule = gameRule;
        _gameRule.OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        _gameRule.OnGameOver -= OnGameOver;
        _restartButton.onClick.RemoveListener(Restart);
    }

    private void OnGameOver(bool status)
    {
        _lable.text = status ? "Победа" : "Поражение";
        _window.SetActive(true);
    }

    //Я реализовал рестарт через SceneManager
    //я понимаю что можно было сделать отдельную сущность для рестарта или переключения между сценами
    //но так как этого не было указано в ТЗ я реализовал это так
    private void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
