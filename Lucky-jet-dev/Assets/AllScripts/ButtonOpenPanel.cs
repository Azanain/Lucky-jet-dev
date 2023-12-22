using UnityEngine.EventSystems;

public class ButtonOpenPanel : AbstractButton, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        OnPress();
    }
    public virtual void OnPress()
    { 
        ClickAudio();
        OpenNewPanel();
    }
}
