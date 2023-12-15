using UnityEngine;

public class SpawnThief : MonoBehaviour
{
    public GameObject thief;
    // Start is called before the first frame update

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SpawnThieff()
    {
        Instantiate(thief, transform.position, transform.rotation);
    }
}