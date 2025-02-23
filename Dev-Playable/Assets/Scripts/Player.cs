using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    private void Update()
    {
        Vector2 inputVector = new Vector2(0,0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x += 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x -= 1;
        }

        inputVector = inputVector.normalized;
        Vector3 movedir = new Vector3(inputVector.x, 0f, inputVector.y);
        transform.position += movedir * moveSpeed * Time.deltaTime;

        float rotateSpeed = 2f;
        transform.forward = Vector3.Slerp(transform.forward, movedir, Time.deltaTime * rotateSpeed);
    }
}
