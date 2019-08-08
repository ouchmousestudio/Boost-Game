using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Cycler : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] float cycleLength = 2f;

    [Range(0, 1)]
    [SerializeField]
    float movementAmount;

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //todo protect against divide by 0
        if (cycleLength <= Mathf.Epsilon) { return; }
        float cycles = Time.time / cycleLength; //grows from 0

        const float tau = Mathf.PI * 2; //around 2.68

        float rawSine = Mathf.Sin(cycles * tau);

        movementAmount = (rawSine + 1f) / 2;

        Vector3 offset = movementAmount * movementVector;
        transform.position = startingPos + offset;
    }
}
