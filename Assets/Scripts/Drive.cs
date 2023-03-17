using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    private Camera cam;
    public float speed = 10f;
    public float rotationSpeed = 20f;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponentInChildren<Camera>();
        cam.gameObject.transform.LookAt(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        float vertTranslation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float horizTranslation = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        this.transform.Translate(0,0,vertTranslation);
        this.transform.Translate(horizTranslation, 0, 0);

        if (Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }

        float camHeight = cam.gameObject.transform.position.y;
        if (Input.GetKey(KeyCode.R) && (camHeight > 5f))
        {
            cam.gameObject.transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.F) && (camHeight < 45f))
        {
            cam.gameObject.transform.Translate(0, 0, -speed * Time.deltaTime);
        }

        float angle = Vector3.Angle(cam.gameObject.transform.forward, Vector3.up);
        if ((angle <= 175f) && Input.GetKey(KeyCode.T))
        {
            cam.gameObject.transform.Translate(Vector3.up * (speed * Time.deltaTime));
            cam.gameObject.transform.LookAt(this.transform.position);
        }
        else if ((angle > 95) &&Input.GetKey(KeyCode.G))
        {
            cam.gameObject.transform.Translate(-Vector3.up * (speed * Time.deltaTime));
            cam.gameObject.transform.LookAt(this.transform.position);
        }
    }
}
