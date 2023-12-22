using UnityEngine;
using UnityEngine.UI;
public enum TypeGameProgress { Start, Stop}
public class ButtonStartGame : ButtonOpenPanel
{
    [SerializeField] private Text _textTitleSettingsPanel;

    [SerializeField] private bool _isStartButton;

    [SerializeField] private TypeGameProgress _typeGameProgress;
    bool IsStop => Plane.StopMoving;
    public override void OnPress()
    {
        base.OnPress();

        if (_textTitleSettingsPanel != null)
            _textTitleSettingsPanel.text = IsStop ? "Pause" : "Settings";

        switch (_typeGameProgress)
        {
            case TypeGameProgress.Start:
                EventManager.StartGame();
                break;
            case TypeGameProgress.Stop:
                EventManager.PauseGame();
                break;

            default:
                break;
        }
    }
}
