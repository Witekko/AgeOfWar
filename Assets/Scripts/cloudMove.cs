using UnityEngine;

public class cloudMove : MonoBehaviour
{
    [SerializeField] private float DeadZone = -30;
    [SerializeField] private float MoveSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        MoveSpeed = Random.Range(0.5f, 2.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = transform.position + (Vector3.left * MoveSpeed) * Time.deltaTime;
        if (transform.position.x < DeadZone)
        {
            Destroy(gameObject);
        }
    }
}