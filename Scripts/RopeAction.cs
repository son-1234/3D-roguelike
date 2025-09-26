using UnityEngine;

public class RopeAction : MonoBehaviour
{
    public Transform player;
    private Camera cam;
    private RaycastHit hit;
    public LayerMask GrapplingObj;
    private LineRenderer lr;
    private SpringJoint sj;
    private bool OnGrappling = false;
    public Transform gunTip;
    private Vector3 spot;
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

            spot = hit.point;
            
            lr.positionCount = 2;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);

            sj = player.gameObject.AddComponent<SpringJoint>();
            sj.autoConfigureConnectedAnchor = false;// 이게 활성화 되어있으면 자동으로 곗ㄴ해주는데 우리는 직접 계산
            sj.connectedAnchor = spot;

            float dis = Vector3.Distance(this.transform.position, spot);

            sj.maxDistance = dis;
            sj.minDistance = dis * 0.5f;
            sj.spring = 5f;
            sj.damper = 5f;//여기서 프로퍼티를 값을 바꿔서 화면을 바꿀 수 있음
        }
    }

    void EndShoot()
    {
        OnGrappling = false;
        lr.positionCount = 0;
        Destroy(sj);
    }

    void DrawRope()
    {
        if (OnGrappling)
        {
            lr.SetPosition(0, gunTip.position);
            this.transform.LookAt(spot);
        }
    }
}
