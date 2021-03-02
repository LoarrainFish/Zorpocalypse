using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fov : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask obstacleMask;
    public LayerMask targetMask;

    public float meshRes;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    public MeshRenderer alert;
    public Material red;
    public Material norm;
    public bool found =  false;

   

    

    public AudioSource Alert;


    public AI ai;
    

    public List<Transform> visableTargets = new List<Transform>();
    
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine(FindTarget());
        

        

        ai = this.gameObject.GetComponent<AI>();
        
        
    }

     
    void Update()
    {
        DrawFov();

        if (found == true)
        {
            
            ai.shot = true;
            
        }
        
        if(found == false)
        {
            
            ai.shot = false;
        }

        

    }

    IEnumerator FindTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            FindVisableTargets();
        }
    }
    void FindVisableTargets()
    {
        visableTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++){
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    //AI player spotted                    
                    found = true;
                    visableTargets.Add(target);
                    var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f);

                }
                
                

            }
            
        }
        
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    

    void DrawFov()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshRes);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoint = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoint.Add(newViewCast.point);
        }

        int vertexCount = viewPoint.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i=0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoint[i]);

            if(i < vertexCount - 2)
            {
                
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
            
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}
