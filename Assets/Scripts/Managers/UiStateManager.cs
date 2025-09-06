using UnityEngine;
using UnityEngine.UIElements;

public enum UIState
{
    Login,
    GroupSelection,
    GameList,
    InGame,
    Settings
}

public class UIStateManager : MonoBehaviour
{
    public static UIStateManager Instance { get; private set; }

    [SerializeField] private UIDocument _uiDocument;

    private VisualElement _loginPanel;
    private VisualElement _groupSelectionPanel;
    private VisualElement _gameListPanel;
    private VisualElement _inGamePanel;
    private VisualElement _settingsPanel;

    private UIState _currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializePanels();
        SetState(UIState.Login);
    }

    private void InitializePanels()
    {
        var root = _uiDocument.rootVisualElement;

        _loginPanel = root.Q<VisualElement>("LoginPanel");
        _groupSelectionPanel = root.Q<VisualElement>("GroupSelectionPanel");
        _gameListPanel = root.Q<VisualElement>("GameListPanel");
        _inGamePanel = root.Q<VisualElement>("InGamePanel");
        _settingsPanel = root.Q<VisualElement>("SettingsPanel");
    }

    public void SetState(UIState newState)
    {
        if (_currentState == newState) return;

        HideAllPanels();
        ShowPanel(newState);
        _currentState = newState;
    }

    private void HideAllPanels()
    {
        _loginPanel?.AddToClassList("hidden");
        _groupSelectionPanel?.AddToClassList("hidden");
        _gameListPanel?.AddToClassList("hidden");
        _inGamePanel?.AddToClassList("hidden");
        _settingsPanel?.AddToClassList("hidden");
    }

    private void ShowPanel(UIState state)
    {
        VisualElement panelToShow = state switch
        {
            UIState.Login => _loginPanel,
            UIState.GroupSelection => _groupSelectionPanel,
            UIState.GameList => _gameListPanel,
            UIState.InGame => _inGamePanel,
            UIState.Settings => _settingsPanel,
            _ => _loginPanel
        };

        panelToShow?.RemoveFromClassList("hidden");
    }

    public UIState GetCurrentState()
    {
        return _currentState;
    }
}