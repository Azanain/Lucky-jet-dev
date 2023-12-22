using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonReplay : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Plane _plane;

    private void Awake()
    {
        if (_plane == null)
            _plane = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Plane>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _plane.OnReplayGame();
    }
}
