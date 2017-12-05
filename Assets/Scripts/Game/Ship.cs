using UnityEngine;
using UnityEngine.EventSystems;

public class Ship : MonoBehaviour, IPointerClickHandler
{
    public float speed = 0.01f;

    // Game field half width
    float bounds;

    float touchPosX;
    Vector3 pos;
    Camera cam;
    Ball CariedBall;

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

    void Start()
    {
        cam = Camera.main;
        bounds = 2.5f;
    }

    void FixedUpdate()
    {
        if(Controller.Levels.IsPaused)
        {
            return;
        }
        touchPosX = cam.ScreenToWorldPoint(Input.mousePosition).x;
        pos = transform.position;
        if (Mathf.Abs(pos.x - touchPosX) > 0.1f)
        {
            pos.x = Mathf.Clamp(pos.x + speed * (touchPosX - pos.x > 0.0f ? 1.0f : -1.0f), -bounds, bounds);
            transform.position = pos;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CariedBall != null)
        {
            Controller.Sound.PlaySound("Click");
            Controller.ReleaseBall(CariedBall);
            CariedBall = null;
        }
    }

    public void SetBall(Ball ball)
    {
        ball.transform.SetParent(transform);
        ball.transform.localPosition = new Vector3(0.0f, 0.53f, 0.0f);
        CariedBall = ball;
        ball.Reactivate();
    }
}