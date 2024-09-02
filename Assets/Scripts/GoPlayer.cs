using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPlayer : MonoBehaviour
{
    private Rigidbody rb;
    private int speed = 3;
    public bool isGround = true;

    public float sensitivity = 5f; // чувствительность мыши
    public float headMinY = -40f; // ограничение угла для головы
    public float headMaxY = 40f;
    public Transform eye;
    private float rotationY;

    public GameObject mainCamera;
    public GameObject playerCamera;

    void Start()
    {
        Screen.lockCursor = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        mainCamera.SetActive(false);
        playerCamera.SetActive(true);
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.S))
            transform.position -= transform.forward * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.W))
            transform.position += transform.forward * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.A))
            transform.position -= transform.right * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.D))
            transform.position += transform.right * Time.deltaTime * speed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed = 6;
        else
            speed = 3;

        //if (Input.GetKey(KeyCode.LeftArrow))
        //    transform.rotation *= Quaternion.Euler(0f, -2.5f, 0f);
        //if (Input.GetKey(KeyCode.RightArrow))
        //    transform.rotation *= Quaternion.Euler(0f, +2.5f, 0f);

        //поворот камеры за курсором
        float rotationX = gameObject.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        rotationY = Mathf.Clamp(rotationY, headMinY, headMaxY);
        transform.rotation = Quaternion.Euler(0, rotationX ,0);
        eye.transform.rotation = Quaternion.Euler(-rotationY, rotationX ,0);

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            rb.AddForce(new Vector3(0, 200f, 0f));
            isGround = false;  
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if (mainCamera.active == true)
            {
                mainCamera.SetActive(false);
                playerCamera.SetActive(true);
            }
            else
            {
                playerCamera.SetActive(false);
                mainCamera.SetActive(true);
            }
        }
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Ground" && other.contacts[0].normal == Vector3.up)
        {
            isGround = true;
        }
    }
}
