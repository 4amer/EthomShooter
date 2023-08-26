using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody playerRB;
    public Animator playerAnim;
    [SerializeField] private float SPEED;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameObject obj;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>(); 
    }

    private void Update()
    {
        //Movement 

        float vertecalX = Input.GetAxisRaw("Vertical");
        float horizontalZ = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(horizontalZ, 0, vertecalX);
        playerRB.MovePosition(playerRB.position + SPEED * movement * Time.deltaTime);

        mainCamera.transform.position = playerRB.transform.position + new Vector3(0,10,-10);

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out RaycastHit rayCastHit, float.MaxValue, mask)) 
        {
            Vector3 direction = rayCastHit.point - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        //Animatinos 
        if(movement != Vector3.zero)
        {
            playerAnim.SetBool("isRun", true);
        }
        else
        {
            playerAnim.SetBool("isRun", false);
        }
    }
}
