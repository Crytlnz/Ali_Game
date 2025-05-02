using UnityEngine;

public class MoveVertikal : MonoBehaviour
{
    public float speed = 2f;              // Kecepatan gerak
    public float height = 2f;             // Jarak vertikal maksimum dari posisi awal

    private Vector3 startPos;
    private bool movingUp = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float moveAmount = speed * Time.deltaTime;
        if (movingUp)
        {
            transform.position += Vector3.up * moveAmount;
            if (transform.position.y >= startPos.y + height)
                movingUp = false;
        }
        else
        {
            transform.position -= Vector3.up * moveAmount;
            if (transform.position.y <= startPos.y)
                movingUp = true;
        }
    }
}
