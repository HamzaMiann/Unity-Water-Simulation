using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PhysicsScript : MonoBehaviour {

    [Range(0f, 50f)]
    public float resistance = 5f;

    [Range(0f, 5f)]
    public float drag = 0.3f;

    public bool optimizeComparisons = false;
    public bool simplifyPhysics = true;

    private MeshCreator water;
    private Rigidbody body;
    private Vector3[] corners;
    private BoxCollider col;
    private Vector3[] vertices;

	// Use this for initialization
	void Start () {
        water = GameObject.FindGameObjectWithTag("Water").GetComponent<MeshCreator>();
        body = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        corners = GetColliderVertexPositions();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        corners = GetColliderVertexPositions();
        vertices = water.Vertices;
        foreach (Vector3 corner in corners)
        {
            Vector3 closestPoint;
            if (optimizeComparisons)
                closestPoint = threadedFindClosest(corner);
            else
                closestPoint = FindClosest(corner);
            if (closestPoint.y > corner.y)
            {
                Vector3 buoyancyForce;
                if (simplifyPhysics)
                    buoyancyForce = Vector3.up * resistance;
                else
                    buoyancyForce = new Vector3(
                        (closestPoint - corner).x * resistance / Vector3.Distance(closestPoint, corner),    // X
                        Vector3.up.y * resistance,                                                          // Y
                        (closestPoint - corner).z * resistance / Vector3.Distance(closestPoint, corner));   // Z

                body.AddForceAtPosition(buoyancyForce, corner);
                body.AddForceAtPosition(body.GetPointVelocity(corner) * (-drag), corner);
            }
        }
	}

    // Finds the closest point in the water to the given point
    Vector3 FindClosest(Vector3 point)
    {
        Vector3 closest = vertices[0];
        float distance = Vector3.SqrMagnitude(closest - point);
        //float distance = Vector3.Distance(closest, point);
        foreach (Vector3 vertex in vertices)
        {
            if (Vector3.SqrMagnitude(vertex - point) < distance)
            {
                closest = vertex;
                distance = Vector3.SqrMagnitude(vertex - point);
            }
        }
        return closest;
    }

    // Finds the closest point by running 2 searches at the same time
    Vector3 threadedFindClosest(Vector3 point)
    {
        Vector3 closest1 = vertices[0];
        Vector3 closest2 = vertices[0];
        float distance1 = Vector3.SqrMagnitude(closest1 - point);
        //float distance1 = Vector3.Distance(closest1, point);
        float distance2 = distance1;
        Task[] tasks = new Task[2];
        tasks[0] = Task.Run(() =>
        {
            for (int i = 0; i < vertices.Length/2; i++)
            {
                if (Vector3.SqrMagnitude(vertices[i] - point) < distance1)
                {
                    closest1 = vertices[i];
                    distance1 = Vector3.SqrMagnitude(vertices[i] - point);
                }
            }
        });

        tasks[1] = Task.Run(() =>
        {
            for (int i = vertices.Length / 2; i < vertices.Length / 2; i++)
            {
                if (Vector3.SqrMagnitude(vertices[i] - point) < distance2)
                {
                    closest2 = vertices[i];
                    distance2 = Vector3.SqrMagnitude(vertices[i] - point);
                }
            }
        });

        Task.WaitAll(tasks);

        if (distance1 < distance2)
            return closest1;

        return closest2;
    }


    Vector3[] GetColliderVertexPositions() {
        Vector3[] vtx = new Vector3[8];
        
        BoxCollider b = col;

        vtx[0] = transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f);
        vtx[1] = transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f);
        vtx[2] = transform.TransformPoint(b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f);
        vtx[3] = transform.TransformPoint(b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f);

        vtx[4] = transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, b.size.z) * 0.5f);
        vtx[5] = transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, b.size.z) * 0.5f);
        vtx[6] = transform.TransformPoint(b.center + new Vector3(-b.size.x, b.size.y, -b.size.z) * 0.5f);
        vtx[7] = transform.TransformPoint(b.center + new Vector3(b.size.x, b.size.y, -b.size.z) * 0.5f);

        return vtx;
    }
    
}
