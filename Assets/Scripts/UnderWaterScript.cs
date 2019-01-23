using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
//using UnityEngine.PostProcessing;

public class UnderWaterScript : MonoBehaviour
{
    
    //public PostProcessingBehaviour post;
    //private MeshCreator water;
    //private Vector3[] vertices;

    //// Use this for initialization
    //void Start()
    //{
    //    post = GetComponent<PostProcessingBehaviour>();
    //    water = GameObject.FindGameObjectWithTag("Water").GetComponent<MeshCreator>();
    //    vertices = water.getVertices();
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    vertices = water.getVertices();
    //    Vector3 closestPoint = getClosest(transform.position);
    //    if (closestPoint.y > transform.position.y)
    //    {
    //        RenderSettings.fogDensity = 0.15f;
    //        post.profile.colorGrading.settings.channelMixer.blue.Set(0f,0f,2f);
    //        post.profile.colorGrading.settings.channelMixer.red.Set(0f,0f,0f);
    //        post.profile.colorGrading.settings.channelMixer.green.Set(0f,1f,1f);
    //    }
    //    else
    //    {
    //        RenderSettings.fogDensity = 0.04f;
    //        post.profile.colorGrading.settings.channelMixer.blue.Set(0f, 0f, 1.14f);
    //        post.profile.colorGrading.settings.channelMixer.red.Set(0f, 0f, 0f);
    //        post.profile.colorGrading.settings.channelMixer.green.Set(0f, 1f, 0f);
    //    }
    //}

    //Vector3 getClosest(Vector3 point)
    //{
    //    Vector3 closest1 = vertices[0];
    //    Vector3 closest2 = vertices[0];
    //    float distance1 = Vector3.Distance(closest1, point);
    //    float distance2 = distance1;
    //    Task[] tasks = new Task[2];
    //    tasks[0] = Task.Run(() =>
    //    {
    //        for (int i = 0; i < vertices.Length / 2; i++)
    //        {
    //            if (Vector3.Distance(vertices[i], point) < distance1)
    //            {
    //                closest1 = vertices[i];
    //                distance1 = Vector3.Distance(vertices[i], point);
    //            }
    //        }
    //    });

    //    tasks[1] = Task.Run(() =>
    //    {
    //        for (int i = vertices.Length / 2; i < vertices.Length / 2; i++)
    //        {
    //            if (Vector3.Distance(vertices[i], point) < distance2)
    //            {
    //                closest2 = vertices[i];
    //                distance2 = Vector3.Distance(vertices[i], point);
    //            }
    //        }
    //    });

    //    Task.WaitAll(tasks);

    //    if (distance1 < distance2)
    //        return closest1;
    //    //foreach(Vector3 vertex in vertices)
    //    //{
    //    //    if (Vector3.Distance(vertex, point) < distance) {
    //    //        closest = vertex;
    //    //        distance = Vector3.Distance(vertex, point);
    //    //    }
    //    //}
    //    return closest2;
    //}
}
