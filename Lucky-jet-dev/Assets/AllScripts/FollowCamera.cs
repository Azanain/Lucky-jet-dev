using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _speedFollow;
    [SerializeField] private float _x, _y, _z;
    private void Start()
    {
        if (!_targetTransform)
            _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        Vector3 position = new (_targetTransform.transform.position.x + _x, _targetTransform.transform.position.y + _y, _targetTransform.transform.position.z + _z);
        transform.position = Vector3.MoveTowards(transform.position, position, _speedFollow);
    }
}
