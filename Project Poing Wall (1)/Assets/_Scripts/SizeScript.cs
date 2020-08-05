using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeScript : MonoBehaviour
{
    bool downSize = false;
    bool size = false;

    [SerializeField] float sizeUp;
    [SerializeField] float sizeDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (size)
        {
            if (!downSize)
            {
                Vector3 scale = transform.localScale;

                scale = Vector3.MoveTowards(scale, Vector3.one * sizeUp, Time.deltaTime * 6f);

                transform.localScale = scale;

                if (transform.localScale == Vector3.one * sizeUp)
                {
                    downSize = true;
                }
            }
            else
            {
                Vector3 scale = transform.localScale;

                scale = Vector3.MoveTowards(scale, Vector3.one * sizeDown, Time.deltaTime * 6f);

                transform.localScale = scale;

                if (transform.localScale == Vector3.one * sizeDown)
                {
                    downSize = false;
                    size = false;
                }
            }
        }
    }

    public void SetSize()
    {
        size = true;
    }
}
