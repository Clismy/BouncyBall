using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BallAgainstWall : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 velo;
    [SerializeField] float maxMagnitude;
    [SerializeField] TextMeshProUGUI text;
    int count = 0;
    [SerializeField] bool updateVelocity;
    Vector2 newVelocity;
    [SerializeField] bool hitOnlyOnce = true;

    [SerializeField] TrailRenderer trail;

    [SerializeField] bool moveToPos;
    [SerializeField] Transform originPos;
    [SerializeField] float moveToSpeed;

    [SerializeField] SizeScript size;

    bool goal = false;
    bool sizeDown = false;

    public bool canAcceptVelocity = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //rb.velocity = new Vector3(1, 1, 0);
        rb.AddForce(new Vector3(0, 5, 0), ForceMode2D.Impulse);
    }

    void Update()
    {
        if(!moveToPos)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxMagnitude);
            if (rb.velocity != Vector2.zero)
            {
                velo = rb.velocity;
            }

            //trail.time = rb.velocity.magnitude * 0.02f;
            if (trail.time >= 1)
            {
                //trail.time = 1;
            }


            text.text = "" + count;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originPos.position, Time.deltaTime * moveToSpeed);

            if(transform.position == originPos.position)
            {
                moveToPos = false;
                rb.isKinematic = false;
            }
        }


        if (goal)
        {
            GoalYeah();
           
        }

        
        //var yRotation = Mathf.MoveTowards(text.transform.eulerAngles.y, 360f, Time.deltaTime * 50f);
        //text.transform.eulerAngles = new Vector3(text.transform.eulerAngles.x, yRotation, text.transform.eulerAngles.z);
    }

    void GoalYeah()
    {
        if(!sizeDown)
        {
            Vector3 scale = text.transform.localScale;

            scale = Vector3.MoveTowards(scale, Vector3.one * 2f, Time.deltaTime * 6f);

            text.transform.localScale = scale;

            if (text.transform.localScale == Vector3.one * 2f)
            {
                sizeDown = true;
            }
        }
        else
        {
            Vector3 scale = text.transform.localScale;

            scale = Vector3.MoveTowards(scale, Vector3.one, Time.deltaTime * 6f);

            text.transform.localScale = scale;

            if (text.transform.localScale == Vector3.one)
            {
                sizeDown = false;
                goal = false;
            }
        }
       
    }

    public void SetVelocity(Vector2 newV)
    {
        if (!moveToPos)
        {
            rb.velocity = newV;
            if(rb.velocity != Vector2.zero)
            {
                velo = rb.velocity;
            }
            //GetComponent<SizeScript>().SetSize();
            size.SetSize();
        }
    }

    public float GetVelocityMagnitute()
    {
        return rb.velocity.magnitude;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Wall")
        {
            rb.velocity = Vector2.Reflect(velo, collision.contacts[0].normal);
            hitOnlyOnce = true;
            size.SetSize();

            //if(rb.velocity.x <= 0.001f && rb.velocity.y <= 0.001f)
            //{
            //    ResetBall();
            //}
        }
        else if(collision.transform.tag == "BonusWall")
        {

        }
    }

    void ResetBall()
    {
        count = 0;
        hitOnlyOnce = true;
        moveToPos = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Death Zone")
        {
            //ResetBall();
            SceneManager.LoadScene("2dBall");
        }
        else if (collision.transform.tag == "Goal")
        {
            count += 1;
            goal = true;
            hitOnlyOnce = true;
            collision.transform.parent.GetComponent<ShakeScript>().SetGoal();
        }
    }
}