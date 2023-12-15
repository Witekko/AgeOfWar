using UnityEngine;

public class Cameramovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private bool moveLeftButtonPressed = false;
    private bool moveRightButtonPressed = false;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || moveRightButtonPressed)
        {
            MoveCamera(Vector3.right);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || moveLeftButtonPressed)
        {
            MoveCamera(Vector3.left);
        }
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, -11.85f, 11.85f);
        transform.position = currentPosition;
    }

    private void MoveCamera(Vector3 direction)
    {
        // Przesuniêcie kamery na podstawie kierunku i prêdkoœci
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    // Nowe funkcje do obs³ugi przycisków z UI
    public void MoveLeft()
    {
        MoveCamera(Vector3.left);
    }

    public void MoveRight()
    {
        MoveCamera(Vector3.right);
    }

    // Funkcje do obs³ugi przycisków z UI
    public void MoveLeftButtonDown()
    {
        moveLeftButtonPressed = true;
    }

    public void MoveLeftButtonUp()
    {
        moveLeftButtonPressed = false;
    }

    public void MoveRightButtonDown()
    {
        moveRightButtonPressed = true;
    }

    public void MoveRightButtonUp()
    {
        moveRightButtonPressed = false;
    }
}