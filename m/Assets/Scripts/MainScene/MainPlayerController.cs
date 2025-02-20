using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] protected SpriteRenderer CharacterRenderer;

    protected Vector2 movementDirection = Vector2.zero;
    private Vector2 lookDirection = Vector2.zero;
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float horiaontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horiaontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }



        movementDirection = movementDirection * 3;


        _rigidbody.velocity = movementDirection;

        Rotate(lookDirection);
    }



    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        CharacterRenderer.flipX = isLeft;
    }




}



