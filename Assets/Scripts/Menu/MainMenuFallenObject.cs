using UnityEngine;

public class MainMenuFallenObject : MonoBehaviour
{
    public float minFallSpeed = 2f, maxFallSpeed = 5f, minRotateSpeed = -360f, maxRotateSpeed = 360f;

    private float _fallSpeed, _rotateSpeed;

    private float _rotationValue;

    public float destroyHeigh = -10f;

    private void Start()
    {
        _fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        _rotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed);
    }

    private void Update()
    {
        transform.position += Vector3.down * _fallSpeed * Time.deltaTime;

        _rotationValue += _rotateSpeed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, 0f, _rotationValue);

        if (transform.position.y < destroyHeigh)
        {
            Destroy(gameObject);
        }
    }
}
