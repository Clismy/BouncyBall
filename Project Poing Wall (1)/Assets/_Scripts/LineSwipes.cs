using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSwipes : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    LineRenderer line;
    Transform childCircle;

    [SerializeField] float toEndSpeed = 40f;
    [SerializeField] float circleExplodeSpeed = 5f;
    [SerializeField] float circleAlphaSpeed = 2f;

    bool circleExplode = false;

    void OnEnable()
    {
        line = GetComponent<LineRenderer>();
        childCircle = transform.GetChild(0);
    }

    public void SetPositions(Vector3 sPos, Vector3 ePos)
    {
        startPos = Camera.main.ScreenToWorldPoint(new Vector3(sPos.x, sPos.y, 10));
        endPos = Camera.main.ScreenToWorldPoint(new Vector3(ePos.x, ePos.y, 10));

        childCircle.position = startPos;
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }
    
    void Update()
    {
        startPos = Vector3.MoveTowards(startPos, endPos, Time.deltaTime * toEndSpeed);

        line.SetPosition(0, startPos);
        childCircle.position = startPos;

        if(startPos == endPos)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            circleExplode = true;
        }


        if (circleExplode)
        {
            Vector3 scale = childCircle.localScale;
            scale = Vector3.MoveTowards(scale, new Vector3(2, 2), Time.deltaTime * circleExplodeSpeed);
            childCircle.localScale = scale;

            Color cC = childCircle.GetComponent<SpriteRenderer>().color;
            cC.a = Mathf.MoveTowards(cC.a, 0, Time.deltaTime * circleAlphaSpeed);
            childCircle.GetComponent<SpriteRenderer>().color = cC;


            if(cC.a == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
