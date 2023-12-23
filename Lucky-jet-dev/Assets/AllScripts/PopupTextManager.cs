using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PoolText))]
public class PopupTextManager : MonoBehaviour
{
    [SerializeField] private Text _textPrefab;
    [SerializeField] private int _maxNumberTexts;
    [SerializeField] private float _durationMovingText;
    [SerializeField] private PoolText _pool;

    private readonly float[] _scoreMultiplier = new float[4] { 2.90f, 1.42f, 4.37f, 3.29f };
    private void Awake()
    {
        if (_pool == null)
            _pool = GetComponent<PoolText>();

        _pool.Initialize(_textPrefab, 5, 5, true, transform);
        EventManager.UpdateScoreEvent += GetFreeText;
    }
    private float GetRandomValue()
    {
        var random = Random.Range(0, _scoreMultiplier.Length);
        return _scoreMultiplier[random];
    }
    private void GetFreeText()
    {
        var prefab = _pool.GetFreeElement();
        prefab.transform.localPosition = Vector3.zero;
        prefab.text = "+" + GetRandomValue().ToString();
    }
    private void OnDestroy()
    {
        EventManager.UpdateScoreEvent -= GetFreeText;
    }
}
