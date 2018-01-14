using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseTaskScript : MonoBehaviour
{
    private Rigidbody rb;
    private Queue<Transformation> taskQueue;
    private bool taskStarted = false;
    private float timeStarted;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (taskStarted)
            moveOnTask();

    }
    public void startTask(List<Transformation> taskTransforamtions)
    {
        taskQueue = new Queue<Transformation>(taskTransforamtions);
        timeStarted = Time.time;
        taskStarted = true;
        rb.position = new Vector3(0, 0, 0);
        rb.rotation = new Quaternion(0, 0.7071068f, 0, 0.7071068f);
        print("Task started");

    }

    public void stopTask()
    {
        taskStarted = false;
        timeStarted = 0;
        taskQueue = null;
    }



    //check if the nose should be moved on a pressed key
    private void moveOnTask()
    {
        if (taskQueue == null || taskQueue.Count < 1)
        {
            return;
        }

        var nextTransformation = taskQueue.Peek();
        if (nextTransformation.OccurenceTime < Time.time - timeStarted)
        {
            taskQueue.Dequeue();
            rb.velocity += new Vector3(nextTransformation.xTranslation, 0, nextTransformation.yTranslation); //z coordiante is not used 
            rb.angularVelocity += new Vector3(0, nextTransformation.rotationFactor, 0);
            print("moved: x=" + nextTransformation.xTranslation + ", y=" + nextTransformation.yTranslation + ", rot=" + nextTransformation.rotationFactor);
        }
    }
}
