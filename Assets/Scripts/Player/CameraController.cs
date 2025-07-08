using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;

    public Transform clampMin, clampMax;

    private Camera _camera;
    private float halfWidth, halfHeight;

    void Start()
    {
        _target = FindAnyObjectByType<PlayerController>().transform;

        clampMin.SetParent(null);
        clampMax.SetParent(null);

        _camera = GetComponent<Camera>();
        halfHeight = _camera.orthographicSize;
        halfWidth = _camera.orthographicSize * _camera.aspect;
    }

    void Update()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, -10);

        Vector3 _clampedPosition = transform.position;

        _clampedPosition.x = Mathf.Clamp(_clampedPosition.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth);
        _clampedPosition.y = Mathf.Clamp(_clampedPosition.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight);

        transform.position = _clampedPosition;
    }
}
