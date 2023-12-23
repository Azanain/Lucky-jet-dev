public class FailMenu : ButtonOpenPanel
{
    private void Awake()
    {
        EventManager.FailGameEvent += OpneFailPanel;
    }
    private void OpneFailPanel()
    {
        foreach (var panel in closePanels)
            panel.SetActive(true);
    }
    public override void OnPress()
    {
        base.OnPress();

        EventManager.ReplayGame();
    }
    private void OnDestroy()
    {
        EventManager.FailGameEvent -= OpneFailPanel;
    }
}
