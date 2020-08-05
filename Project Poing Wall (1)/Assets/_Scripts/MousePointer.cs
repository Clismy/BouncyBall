using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MousePointer : MonoBehaviour
{

    [SerializeField, Header("Debug Mode")] bool debugMode = false;

    [SerializeField] LineRenderer line;
    [SerializeField] LineRenderer predictionLine;
    [SerializeField] float movementSpeed;
    [SerializeField] GameObject swipeRenderes;

    [SerializeField] SizeScript sizeS;

    Vector3 startPos;

    Rigidbody2D rb;
    Vector2 velocity;
    [SerializeField] SpriteRenderer circleSprite;
    bool lineGoBack = false;

    public delegate void OnPlayer();
    public static OnPlayer OnPlayerDead;

    [SerializeField] GameObject deathParticle;
    [SerializeField] GameObject impactParticle;

    bool activate = false;
    bool retrieveDash = false;
    bool skipFirstInput = true;

    void Awake()
    {
        PressToStart.OnStartGame += ActivatePlayer;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        activate = false;
        skipFirstInput = true;
        //rb.AddForce(Vector3.up * movementSpeed, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (!activate || skipFirstInput) return;

        if (!debugMode)
        {
           TouchInput();
        }
        else
        {
            MouseInput();
        }

        if (rb.velocity != Vector2.zero)
        {
            velocity = rb.velocity;
        }

    }

    void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            line.SetPosition(1, screenToWorld);

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                Vector3 worldStartPos = Camera.main.ScreenToWorldPoint(new Vector3(startPos.x, startPos.y, 10));
                line.SetPosition(0, worldStartPos);

                line.enabled = true;
                circleSprite.gameObject.SetActive(true);
                predictionLine.enabled = true;

                circleSprite.transform.position = worldStartPos;
                retrieveDash = false;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (Swipes.instance.CurrentSwipes > 0)
                {
                    var endPos = (Vector3)touch.position;

                    Vector3 dir = endPos - startPos;
                    dir.Normalize();

                    if (dir.sqrMagnitude != 0)
                    {
                        SetVelocity(dir * movementSpeed);

                        Swipes.instance.DecreaseSwipe();
                    }

                    predictionLine.enabled = false;

                    GameObject swipeRe = Instantiate(swipeRenderes, Vector3.zero, Quaternion.identity) as GameObject;
                    swipeRe.GetComponent<LineSwipes>().SetPositions(startPos, endPos);
                }

                line.enabled = false;
                circleSprite.gameObject.SetActive(false);
                retrieveDash = true;
            }

            if (predictionLine.enabled)
            {
                Vector3 dir = (Vector3)touch.position - startPos;
                dir.Normalize();

                Vector3 realDir = (transform.position + dir);
                predictionLine.SetPosition(1, realDir);

                predictionLine.SetPosition(0, transform.position);
            }
        }

        void TouchBegan()
        { 

        }
    }
    void MouseInput()
    {
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        line.SetPosition(1, screenToWorld);

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            Vector3 worldStartPos = Camera.main.ScreenToWorldPoint(new Vector3(startPos.x, startPos.y, 10));

            line.SetPosition(0, worldStartPos);

            line.enabled = true;
            circleSprite.gameObject.SetActive(true);
            predictionLine.enabled = true;

            circleSprite.transform.position = worldStartPos;
            retrieveDash = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Swipes.instance.CurrentSwipes > 0)
            {
                var endPos = Input.mousePosition;

                Vector3 dir = endPos - startPos;
                dir.Normalize();

                if (dir.sqrMagnitude != 0)
                {
                    SetVelocity(dir * movementSpeed);

                    Swipes.instance.DecreaseSwipe();
                }

                predictionLine.enabled = false;

                GameObject swipeRe = Instantiate(swipeRenderes, Vector3.zero, Quaternion.identity) as GameObject;
                swipeRe.GetComponent<LineSwipes>().SetPositions(startPos, endPos);
            }

            line.enabled = false;
            circleSprite.gameObject.SetActive(false);
            retrieveDash = true;
        }

        if (predictionLine.enabled)
        {
            Vector3 dir = Input.mousePosition - startPos;
            dir.Normalize();

            Vector3 realDir = (transform.position + dir);
            predictionLine.SetPosition(1, realDir);

            predictionLine.SetPosition(0, transform.position);
        }
    }

    public void SetVelocity(Vector2 newV)
    {
        rb.velocity = newV;
    }
    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(0.2f);
        skipFirstInput = false;
    }

    void Death()
    {
        GameObject explosion = Instantiate(deathParticle, transform.position, transform.rotation);
        gameObject.SetActive(false);
        OnPlayerDead.Invoke();
    }

    void ActivatePlayer()
    {
       // rb.AddForce(Vector3.up * movementSpeed, ForceMode2D.Impulse);
        activate = true;
        skipFirstInput = true;
        StartCoroutine(WaitForSeconds());
    }

    //void OnApplicationFocus(bool pauseStatus)
    //{
    //    if (pauseStatus)
    //    {
    //        Time.timeScale = 0;
    //        //your app is in the background, yay!!!!
    //    }
    //    else
    //    {
    //        Time.timeScale = 1;
    //    }
    //}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 reflect = Vector2.Reflect(velocity, collision.contacts[0].normal).normalized;
        if (reflect.sqrMagnitude == 0)
        {
            reflect = Vector2.down;
        }
        rb.velocity = reflect * movementSpeed;
        velocity = rb.velocity;

        if (collision.transform.tag == "Wall")
        {
            collision.transform.GetComponent<BlinkingWall>()?.Blink();

            Vector3 dir = transform.position - (Vector3)collision.contacts[0].point;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject impact = Instantiate(impactParticle, collision.contacts[0].point, Quaternion.AngleAxis(angle, Vector3.forward));

            if (retrieveDash)
            {
                retrieveDash = false;
            }
        }
        if (collision.transform.tag == "Goal")
        {
            Score.instance.TotalScore += 15;
            if (retrieveDash)
            {
                Swipes.instance.IncreaseSwipe();
                retrieveDash = false;
            }
        }
        if (collision.transform.tag == "Enemy")
        {
            Death();
        }

        //GetComponent<SizeScript>()?.SetSize();
        sizeS.SetSize();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Death Zone")
        {
            Death();
        }
        else if (collision.transform.tag == "BonusWall")
        {
            Swipes.instance.IncreaseSwipe();
            Score.instance.TotalScore += 5;
        }
        if (collision.transform.tag == "Goal")
        {
            Score.instance.TotalScore += 10;
        }
        //GetComponent<SizeScript>()?.SetSize();
        sizeS.SetSize();
    }

    void OnDestroy()
    {
        PressToStart.OnStartGame -= ActivatePlayer;    
    }
}