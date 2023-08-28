using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundcontroller : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject[] layer = new GameObject[4];
    
    [SerializeField] float speedMove;
    float move;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        backGroundMove();
        move = target.gameObject.GetComponent<Rigidbody2D>().velocity.x;
    }
    void backGroundMove()
    {
        for(int i=0;i<layer.Length; i++)
        {
            float temp = (i+1)*2;
            layer[i].transform.Translate(Vector3.left * move * Time.deltaTime * (speedMove/temp));
        }
    }
}
