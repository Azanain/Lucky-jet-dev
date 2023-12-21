using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private float _speedFollow;
    [SerializeField] private float _y, _z;
    private void Start()
    {
        if (!_transformPlayer)
            _transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        Vector3 position = new (_transformPlayer.transform.position.x, _transformPlayer.transform.position.y + _y, _transformPlayer.transform.position.z + _z);
        transform.position = Vector3.MoveTowards(transform.position, position, _speedFollow);
    }
}
