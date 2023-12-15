using UnityEngine;

public class cloudSpawn : MonoBehaviour
{
    public GameObject Cloud1;
    public GameObject Cloud2;
    public GameObject Cloud3;
    [SerializeField] private float spawnRate = 4;
    [SerializeField] private float timer;
    [SerializeField] private float heighOffset = 5;
    [SerializeField] private float depthOffset = 12;
    [SerializeField] private int chosenCloud;

    // Start is called before the first frame update
    private void Start()
    {
        spawnCloud();
    }

    // Update is called once per frame
    private void Update()
    {
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnCloud();
            timer = 0;
        }
    }

    private void spawnCloud()
    {
        float lowastPoint = transform.position.y - heighOffset;
        float highestPoint = transform.position.y + heighOffset;
        float deepestPoint = transform.position.z - depthOffset;
        float flatestPoint = transform.position.z + depthOffset;
        chosenCloud = Random.Range(1, 11);
        if (chosenCloud == 6)
        {
            Instantiate(Cloud1, new Vector3(transform.position.x, Random.Range(lowastPoint, highestPoint), Random.Range(flatestPoint, deepestPoint)), transform.rotation);
        }
        else if (chosenCloud == 5 || chosenCloud == 4)
        {
            Instantiate(Cloud3, new Vector3(transform.position.x, Random.Range(lowastPoint, highestPoint), Random.Range(flatestPoint, deepestPoint)), transform.rotation);
        }
        else
        {
            Instantiate(Cloud2, new Vector3(transform.position.x, Random.Range(lowastPoint, highestPoint), Random.Range(flatestPoint, deepestPoint)), transform.rotation);
        }
    }
}