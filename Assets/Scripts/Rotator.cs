using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField] GameObject target; //The object we want to rotate
    [SerializeField] GameObject pivot; //The point we want to rotate around
    [SerializeField] float speed = 10; //Speed of the rotation


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = pivot.transform.right; //rotation direction

        target.transform.RotateAround(
            pivot.transform.position,
            direction,
            speed * Time.deltaTime
        );
    }
}
