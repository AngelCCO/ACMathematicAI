using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public GameObject fuel;

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

        Debug.Log("Angle:" + angle * Mathf.Rad2Deg);
        Debug.Log("unity Angle:" + Vector3.Angle(tF, fD));

        Debug.DrawRay(this.transform.position, tF, Color.green, 2);
        Debug.DrawRay(this.transform.position, fD, Color.red, 2);

        this.transform.Rotate(0, 0, angle * Mathf.Rad2Deg);
    }
    void CalculateDistance()
    {
        Vector3 tP = this.transform.position;
        Vector3 fP = fuel.transform.position;

        float distance = Mathf.Sqrt(Mathf.Pow(tP.x - tP.x, 2) + Mathf.Pow(tP.y - fP.y, 2));

        float unityDistance = Vector3.Distance(tP, fP);
            Debug.Log("Distance:" + distance);
        Debug.Log("Unity Distance" + unityDistance);
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
    }
}
