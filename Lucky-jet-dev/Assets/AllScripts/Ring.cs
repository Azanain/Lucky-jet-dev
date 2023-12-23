using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ring : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        EventManager.ReplayGameEvent += ActivateRing;
        EventManager.CirclePassedEvent += ActivateRing;
    }
    private void ActivateRing()
    {
        gameObject.SetActive(true);
    }
    private void Update()
    {
        transform.Rotate(10 * Time.deltaTime, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_particleSystem != null)
            {
                var effect = Instantiate(_particleSystem, transform.position, Quaternion.identity);
                effect.Play();
            }

            if (_audioSource.clip != null)
                _audioSource.Play();

            EventManager.UpdateScore();
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("MissRingTrigger"))
            gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        EventManager.ReplayGameEvent -= ActivateRing;
        EventManager.CirclePassedEvent -= ActivateRing;
    }
}
