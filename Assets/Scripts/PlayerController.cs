using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerSpeed = 6f;
    float xSpeed = 0f;
    float maxXValue = 4.28f;
    bool isPlayerMoving;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerMoving = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerMoving == false)
        {
            return;
        }
        float touchX = 0f;
        float newXValue;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            xSpeed = 250;
            touchX = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }
        else if (Input.GetMouseButton(0))
        {
            xSpeed = 500f;
            touchX = Input.GetAxis("Mouse X");
        }
        else
        {
            xSpeed = 0f;
        }

        newXValue = transform.position.x + xSpeed * touchX * Time.deltaTime;
        newXValue = Mathf.Clamp(newXValue, -maxXValue, maxXValue);
        Vector3 playerNewPosition = new Vector3(newXValue, transform.position.y, transform.position.z + playerSpeed * Time.deltaTime);
        transform.position = playerNewPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            isPlayerMoving = false;
        }
    }
}