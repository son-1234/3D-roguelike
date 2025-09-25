using UnityEngine;

public class RopeAction : MonoBehaviour
{
    public Transform player;
    private Camera cam;
    private RaycastHit hit;
    public LayerMask GrapplingObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RopeShoot();
        }
    }

    void RopeShoot()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, GrapplingObj))
        {
            print("장애물 걸림");
        }
    }
}
