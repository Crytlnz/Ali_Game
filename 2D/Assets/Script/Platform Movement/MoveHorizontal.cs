using UnityEngine;

public class MoveHorizontal : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPos;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float movement = speed * Time.deltaTime;
        if (movingRight)
        {
            transform.Translate(Vector2.right * movement);
            if (transform.position.x >= startPos.x + distance)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * movement);
            if (transform.position.x <= startPos.x - distance)
                movingRight = true;
        }
    }
}
