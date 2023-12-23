using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MissRingTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ring"))
        {
            if (_audioSource.clip != null)
                _audioSource.Play();

            EventManager.MissRingle();
        }
    }
}
