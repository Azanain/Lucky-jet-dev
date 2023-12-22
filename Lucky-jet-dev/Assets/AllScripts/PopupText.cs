using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{
    [SerializeField] private Text _textPrefab;
    [SerializeField] private int _maxNumberTexts;
    [SerializeField] private float _durationMovingText;

    private Text[] _texts;
    private void Awake()
    {
        _texts = new Text[_maxNumberTexts];

        for (int i = 0; i < _maxNumberTexts; i++)
        {
            var prefab = Instantiate(_textPrefab, Vector2.zero, Quaternion.identity, transform);
            prefab.gameObject.SetActive(false);
            _texts[i] = prefab;
        }

        EventManager.UpdateScoreEvent += OnSelectRing;
    }
    private void GetFreeText(int value)
    {
        foreach (var text in _texts)
        {
            if (text.gameObject.activeSelf)
            {
                text.gameObject.SetActive(true);
                text.text = "+" + value.ToString();
                StartCoroutine(MoveText(text.gameObject));

                break;
            }
        }
    }
    private void OnSelectRing(int value)
    {
        GetFreeText(value);
    }
    private IEnumerator MoveText(GameObject textObject)
    {
        float time = _durationMovingText;

        while (time > 0)
        {
            time -= Time.deltaTime;
            float size = time / _durationMovingText;

            Vector2 randomVector = new (Random.Range(-5, 5), Random.Range(-5,5));
            textObject.transform.localScale = new Vector2(size, size);

            textObject.transform.Translate(randomVector * Time.deltaTime);

            yield return null;
        }
        if (time <= 0)
        {
            textObject.SetActive(false);
            StopCoroutine(MoveText(textObject));
        }
    }
    private void OnDestroy()
    {
        EventManager.UpdateScoreEvent -= OnSelectRing;
    }
}
