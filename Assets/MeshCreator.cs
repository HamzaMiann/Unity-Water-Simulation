using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MeshCreator : MonoBehaviour {

    [Range(2,256)]
    public int resolution = 16;

    [Range(1f, 50f)]
    public float size = 15f;

    [Range(1f,50f)]
    public float rippleNoiseReductionFactor = 5f;

    [Range(0.01f, 50f)]
    public float waveHeight = 1f;

    [Range(1f, 50f)]
    public float waveReductionFactor = 4f;

    [Range(1f, 20f)]
    public float waveSpeed = 1f;
    public bool staticWater = false;

    private Vector3 position;
    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;

    public float sineOffset = 0.1f;

    public Vector3[] Vertices
    {
        get => vertices;
        private set => vertices = value;
    }

    private void Awake()
    {
        initializeMesh();
        position = transform.position;
    }

    private void OnValidate()
    {
        initializeMesh();
        position = transform.position;
        UpdateMesh(size);
    }


    // Use this for initialization
    void Start () {
        initializeMesh();
        position = transform.position;
        UpdateMesh(size);
    }

    // Run the logic on fixed update as the physics are updated per-frame
    void FixedUpdate()
    {
        if (!staticWater)
            sineOffset += 0.01f * waveSpeed;
        position = transform.position;
        UpdateMesh(size);
    }

    // Initializes the mesh
    void initializeMesh()
    {
        vertices = new Vector3[resolution * resolution];
        triangles = new int[(resolution - 1) * (resolution - 1) * 6];
        mesh = GetComponent<MeshFilter>().sharedMesh;
        if (mesh == null)
        {
            GetComponent<MeshFilter>().sharedMesh = new Mesh();
            mesh = GetComponent<MeshFilter>().sharedMesh;
        }
    }

    // Updates the mesh vertices
    void UpdateMesh(float s)
    {

        s = s / (float)resolution;

        int i = 0;
        float offset = sineOffset;
        //Task[] tasks = new Task[resolution];
        for (int x = 0; x < resolution; ++x)
        {
            //var ti = i;
            //var toffset = offset;
            //tasks[x] = Task.Run(() => { setRow(x, ti, toffset, s); });
            //i += resolution;
            for (int y = 0; y < resolution; ++y)
            {
                float noise = Mathf.PerlinNoise(x + offset, y + offset) / rippleNoiseReductionFactor;
                vertices[i++] = new Vector3(position.x + (s * x),   // X
                                            (position.y + 0.01f)    // Y
                                            + (waveHeight * Mathf.Sin(offset) / waveReductionFactor)
                                            + (noise)
                                            , position.z + (s * y));  // Z
            }
            offset += 0.3f;
        }
        
        int step = resolution;
        int index = 0;
        for (int y = 0; y < (resolution - 1); ++y)
        {
            for (int x = 0; x < (resolution - 1); ++x)
            {
                triangles[index++] = (x + y * resolution);
                triangles[index++] = (x + y * resolution) + step + 1;
                triangles[index++] = (x + y * resolution) + step;
                triangles[index++] = (x + y * resolution);
                triangles[index++] = (x + y * resolution) + 1;
                triangles[index++] = (x + y * resolution) + step + 1;
            }
        }

        //Task.WaitAll(tasks);
        

        if (mesh != null)
        {
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
        }
        
    }


    private void setRow(int x, int i, float offset, float s)
    {
        //Debug.Log(" x = " + x + " i = " + i + " vertices = " + vertices.Length);
        for (int y = 0; y < resolution; ++y)
        {
            float noise = Mathf.PerlinNoise(x + offset, y + offset) / rippleNoiseReductionFactor;
            vertices[i++] = new Vector3(position.x + (s * x),   // X
                                        (position.y + 0.01f)    // Y
                                        + (waveHeight * Mathf.Sin(offset) / waveReductionFactor)
                                        + (noise)
                                        //+ (Mathf.Cos(offset)*waveHeight*0.1f/waveReductionFactor)
                                        , position.z + (s * y));  // Z
        }
    }
}
