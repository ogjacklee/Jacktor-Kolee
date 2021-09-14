using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MoveToClick : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    public Animator animator;
    public Rigidbody2D rb;
    private Vector3 targetPosition;
    private Camera mainCamera;
    private Vector3 position;
    PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine)
        {
            if (Input.GetMouseButtonDown(1))//right click
            {
                CalculateTargetPosition();
                Debug.Log(targetPosition);
                animator.SetBool("isRunning", true);
            }

            //locating Player position
            GameObject player = GameObject.Find("Player");//defines player object
            Transform playerTransform = player.transform;//player.transform command in variable
            position = playerTransform.position;//defines players position

            if (targetPosition == position)
            {
                animator.SetBool("isRunning", false);
            }
        }
    }


    //methods

    private void CalculateTargetPosition()
    {
        var mousePosition = Input.mousePosition;//gets mouse position
        var transformedPosition = mainCamera.ScreenToWorldPoint(mousePosition);//idk what this does
        targetPosition = new Vector3(transformedPosition.x, transformedPosition.y, 0);//gets position
    }


    private void MoveToTarget()//movement
    {
        var moveVector = position;
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, Time.deltaTime * movementSpeed));
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }
}
