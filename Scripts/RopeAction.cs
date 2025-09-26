using UnityEngine;

public class RopeAction : MonoBehaviour
{
    public Transform player;
    private Camera cam;
    private RaycastHit hit;
    public LayerMask GrapplingObj;
    private LineRenderer lr;

    private bool OnGrappling = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RopeShoot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndShoot();
        }
        
        DrawRope();
    }

    void RopeShoot()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, GrapplingObj))
        {
            OnGrappling = true;
            
            lr.positionCount = 2;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);
        }
    }

    void EndShoot()
    {
        OnGrappling = false;
        lr.positionCount = 0;

        OnGrappling = false;
    }

    void DrawRope()
    {
        if (OnGrappling)
        {
            lr.SetPosition(0, this.transform.position);
        }
    }
}
