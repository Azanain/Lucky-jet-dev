using UnityEngine;
public abstract class AbstractButton : MonoBehaviour
{
    [SerializeField] private Options _options;

    [Header("Abstract Data")]
    public GameObject[] closePanels;
    public GameObject[] openPanels;
    public virtual void OpenNewPanel()
    {
        ClickAudio();

        if (closePanels != null)
            for (int i = 0; i < closePanels.Length; i++)
                closePanels[i].SetActive(false);

        if (openPanels != null)
            for (int i = 0; i < openPanels.Length; i++)
                openPanels[i].SetActive(true);
    }
    public void OpenNewPanel(GameObject newPanel)
    {
        ClickAudio();

        if (newPanel != null)
            newPanel.SetActive(true);
    }
    public void CloseOldPanel(GameObject oldPanel)
    {
        ClickAudio();

        if (oldPanel != null)
            oldPanel.SetActive(false);
    }
    public void ClickAudio()
    {
        if(_options != null)
            _options.ButtonPressAudio();
    }
}
