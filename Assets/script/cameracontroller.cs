using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameracontroller : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float smooth;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        folow();
    }
    void folow()
    {
        Vector3 s1 = transform.position;
        Vector3 s2 = target.transform.position;
        s2 = new Vector3(s2.x, s2.y+3, s1.z);
        transform.position = Vector3.SmoothDamp(s1,s2,ref velocity,smooth);
    }
}
