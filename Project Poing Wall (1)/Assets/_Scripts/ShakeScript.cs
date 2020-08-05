using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    bool shake = false;
    bool shakeLeft = false;

    [SerializeField] float shakeLimit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {
            if (!shakeLeft)
            {
                var zRotation = Mathf.MoveTowards(transform.eulerAngles.z, shakeLimit, Time.deltaTime * 50f);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);

                if (transform.eulerAngles.z >= shakeLimit)
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
                    shakeLeft = true;
                }
            }
            else
            {
                var zRotation = Mathf.MoveTowards(transform.eulerAngles.z, -shakeLimit, Time.deltaTime * 50f);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);

                if (transform.eulerAngles.z >= shakeLimit)
                {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
                    shakeLeft = false;
                    shake = false;
                }
            }
        }
    }

    public void SetGoal()
    {
        shake = true;
    }
}
