using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    private int maxFingers = 1;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Debug.Log("I'm being touched " + i + " times.");

                Touch touch = Input.GetTouch(i);

                Debug.Log(touch.phase);

                if (touch.phase == TouchPhase.Began)
                {
                    int numFingers = Input.touchCount;

                    if (numFingers > maxFingers)
                        numFingers = maxFingers;

                    Vector3 spawnPosition = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 40));

                    SpawnObject(numFingers, spawnPosition);
                }
            }
        }
    }

    void SpawnObject(int numFingers, Vector3 spawnPosition)
    {
        int spawnIndex = Mathf.Clamp(numFingers - 1, 0, objectsToSpawn.Length - 1);

        GameObject spawnedObject = Instantiate(objectsToSpawn[spawnIndex], spawnPosition, Quaternion.identity);

        Color randomColor = new Color(Random.value, Random.value, Random.value);

        Renderer renderer = spawnedObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = randomColor;
        }

        float randomScale = Random.Range(0.25f, 1.75f);

        spawnedObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
    }
}