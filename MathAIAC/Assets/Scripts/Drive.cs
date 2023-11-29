using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public GameObject fuel;
    bool autoPilot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void CalculateAngle()
    {
        Vector3 tF = this.transform.up;
        Vector3 fD = fuel.transform.position - this.transform.position;

        float dot = tF.x * fD.x + tF.y * fD.y;
        float angle = Mathf.Acos(dot / (tF.magnitude * fD.magnitude));

        Debug.DrawRay(this.transform.position, tF * 10, Color.green, 2);
        Debug.DrawRay(this.transform.position, fD, Color.red, 2);

        int clockwise = 1;
        if (Cross(tF, fD).z < 0)
            clockwise = -1;

        this.transform.Rotate(0, 0, (angle * clockwise * Mathf.Deg2Rad) * 0.2f);
    }

    Vector3 Cross(Vector3 v, Vector3 w)
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.z * w.x - v.x * w.z;
        float zMult = v.x * w.y - v.y * w.x;

        Vector3 crossPod = new Vector3(xMult, yMult, zMult);
        return crossPod;
    }
    void CalculateDistance()
    {
        Vector3 tP = this.transform.position;
        Vector3 fP = fuel.transform.position;

        float distance = Mathf.Sqrt(Mathf.Pow(tP.x - tP.x, 2) + Mathf.Pow(tP.y - fP.y, 2) + Mathf.Pow(tP.z - fP.z, 2));

        float unityDistance = Vector3.Distance(tP, fP);
            Debug.Log("Distance:" + distance);
        Debug.Log("Unity Distance" + unityDistance);
    }

    float autoSpeed = 0.1f;
    void AutoPilot()
    {
        CalculateAngle();
        this.transform.Translate(this.transform.up * autoSpeed, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, translation, 0);
        transform.Rotate(0, 0, -rotation);

        if(Input.GetKeyDown(KeyCode.Space))
            {
            CalculateDistance();
            CalculateAngle();
        }

        if (Input .GetKeyUp(KeyCode.T))
        {
            autoPilot = !autoPilot;
        }
        if(autoPilot)
            AutoPilot();
    }
}
