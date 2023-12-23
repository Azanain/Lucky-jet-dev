using System.Collections;
using UnityEngine;

public class PopupTex : MonoBehaviour
{
    [SerializeField] private float _durationMovingText;
    private void OnEnable()
    {
        StartCoroutine(MoveText());
    }
    private IEnumerator MoveText()
    {
        float time = _durationMovingText;

        while (time > 0)
        {
            time -= Time.deltaTime;
            float size = time / _durationMovingText;

            Vector2 randomVector = new(Random.Range(-5, 5), Random.Range(-5, 5));
            transform.localScale = new Vector2(size, size);

            transform.Translate(randomVector * Time.deltaTime);

            yield return null;
        }
        if (time <= 0)
        {
            gameObject.SetActive(false);
            StopCoroutine(MoveText());
        }
    }
}
