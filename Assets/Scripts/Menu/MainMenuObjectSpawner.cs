using UnityEngine;

public class MainMenuObjectSpawner : MonoBehaviour
{
    public Transform minPos, maxPos;

    public GameObject[] fallingObjects;

    public float timeBetweenSpawns;

    private float _spawnCounter;

    private void Update()
    {
        _spawnCounter -= Time.deltaTime;

        if (_spawnCounter <= 0)
        {
            _spawnCounter = timeBetweenSpawns;

            GameObject newObject = Instantiate(fallingObjects[Random.Range(0, fallingObjects.Length)]);

            newObject.transform.position = new Vector3(Random.Range(minPos.position.x, maxPos.position.x), minPos.position.y, 0f);

            newObject.SetActive(true);
        }
    }
}
