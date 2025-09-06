using UnityEngine;
using UnityEngine.UIElements;

public class GameListController : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var gameListPanel = root.Q<VisualElement>("GameListPanel");

        PopulateGameList(gameListPanel);
    }

    private void PopulateGameList(VisualElement panel)
    {
        var gamesContainer = panel.Q<VisualElement>("GamesContainer");
        gamesContainer.Clear();

        for (int i = 0; i < _gameConfig.GameScenes.Length; i++)
        {
            var gameData = _gameConfig.GameScenes[i];
            var gameButton = new Button(() => StartGame(i))
            {
                text = gameData.DisplayName
            };
            gamesContainer.Add(gameButton);
        }
    }

    private void StartGame(int index)
    {
        SceneManager.Instance.StartGame(index);
    }
}