using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(10 * Time.deltaTime, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("add ring");
        }
    }
}
