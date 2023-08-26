using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AK47 : MonoBehaviour
{
    private Transform shotPoint;

    [SerializeField] private GameObject Particles;
    [SerializeField] private GameObject Bullet;
    private float Shot;
    [SerializeField] private float timeToShotConst;
    [SerializeField] private float reloadConst;

    private float timeToShot;
    private float reload;

    private Movement movementScript;

    private Camera mainCamera;
    [SerializeField] private LayerMask mask;
    private void Start()
    {
        shotPoint = GameObject.Find("shotPoint").transform;

        reload = reloadConst;
        timeToShot = timeToShotConst;

        movementScript = FindObjectOfType<Movement>();

        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (timeToShot < 0)
            {
                Instantiate(Particles, shotPoint.position, shotPoint.rotation);
                Instantiate(Bullet, shotPoint.position, shotPoint.rotation);
                timeToShot = timeToShotConst;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            movementScript.playerAnim.SetBool("isShooting", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            movementScript.playerAnim.SetBool("isShooting", false);
        }
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out RaycastHit rayCastHit, float.MaxValue, mask))
        {
            Vector3 direction = rayCastHit.point - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            angle -= 180;
            transform.rotation = Quaternion.Euler(0,angle,0);
        }
        timeToShot -= Time.deltaTime;
    }
}
