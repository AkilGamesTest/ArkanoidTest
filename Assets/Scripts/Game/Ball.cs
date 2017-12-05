using UnityEngine;

public enum BallSize : sbyte
{ 
    SMALL,
    NORMAL,
    BIG,
}

public class Ball : MonoBehaviour
{
    public TrailRenderer trail;

    const float startVelocity = 7.0f;

    Vector2 lastVelocity;
    bool isPaused;

    LocationController controller;
    LocationController Controller
    {
        get
        {
            if (controller == null)
            {
                controller = LocationController.Instance;
            }
            return controller;
        }
    }

    Rigidbody2D body;
    Rigidbody2D Body
    {
        get
        {
            if (body == null)
            {
                body = GetComponent<Rigidbody2D>();
            }
            return body;
        }
    }

    BallSize size;
    public BallSize Size
    {
        get
        {
            return size;
        }
        set
        {
            size = value;
            if (value > BallSize.BIG)
            {
                size = BallSize.BIG;
            }
            if (value < BallSize.SMALL)
            {
                size = BallSize.SMALL;
            }
            switch (size)
            {
                case BallSize.SMALL:
                    transform.localScale = new Vector3(0.15f, 0.15f, 1.0f);
                    break;
                default:
                case BallSize.NORMAL:
                    transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
                    break;
                case BallSize.BIG:
                    transform.localScale = new Vector3(0.6f, 0.6f, 1.0f);
                    break;
            }
        }
    }

    // Can be changed. Get damage info from settings file
    int Damage()
    {
        switch (Size)
        {
            case BallSize.SMALL:
                return 1;
            default:
            case BallSize.NORMAL:
                return 2;
            case BallSize.BIG:
                return 4;
        }
    }
    
    void Start()
    {
        Size = BallSize.NORMAL;
    }

    void FixedUpdate()
    {
        if(Controller.Levels.IsPaused && !isPaused)
        {
            isPaused = true;
            lastVelocity = Body.velocity;
            Body.velocity = Vector2.zero;
            Body.bodyType = RigidbodyType2D.Static;
        }
        else if (!Controller.Levels.IsPaused && isPaused)
        {
            isPaused = false;
            Body.bodyType = RigidbodyType2D.Dynamic;
            Body.velocity = lastVelocity;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Block")
        {
            Block hitted = collision.transform.GetComponent<Block>();
            hitted.GetDamage(Damage());
            Controller.AddHitBallScore();
        }
        else if (collision.transform.tag == "Armor")
        {
            Controller.Sound.PlaySound("ArmorHit");
        }
        else
        {
            Controller.Sound.PlaySound("Hit");
        }
        Body.velocity = Body.velocity.normalized * startVelocity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "DeathZone")
        {
            Controller.RemoveBall(this);
        }
    }

    public void Launch()
    {
        Body.velocity = new Vector2(0.0f, startVelocity);
        trail.gameObject.SetActive(true);
        trail.startColor = Random.ColorHSV(0.0f, 1.0f);
    }

    public void Reactivate()
    {
        Body.velocity = Vector2.zero;
        Size = BallSize.NORMAL;
        gameObject.SetActive(true);
        trail.gameObject.SetActive(false);
    }
}