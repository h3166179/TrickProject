using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    [SerializeField][Tooltip("Camera Move Factor")]private float delayMoveFactor = 3f;//延迟因子
    private Transform player;
    private float offsetY=1.13f;//相机与Player之间的偏移量
    [SerializeField] private float leftBound;
    [SerializeField] private float rightBound;
    [SerializeField] private float topBound;
    [SerializeField] private float bottomBound;

    private void Awake()
    {
        //GameManager.Instance.RegisterCamera(this.gameObject);
        camera = GetComponent<Camera>();
    }

    private void Start()
	{
        player = GameManager.Instance.GetPlayer().transform;
        //offsetY = transform.position.y - player.position.y;
        float pos_x = player.position.x;
        if (pos_x <= leftBound)
            pos_x = leftBound;
        else if (pos_x >= rightBound)
            pos_x = rightBound;
        transform.position = new Vector3(pos_x, player.position.y + offsetY, transform.position.z);
    }

	private void FixedUpdate()
    {
        Vector3 new_point = new Vector3(player.position.x, player.position.y + offsetY, transform.position.z);
        if (new_point.x <= leftBound)
            new_point.x = leftBound;
        else if (new_point.x >= rightBound)
            new_point.x = rightBound;
        if (new_point.y <= topBound)
            new_point.y = topBound;
        else if (new_point.y >= bottomBound)
            new_point.y = bottomBound;
        if (Vector3.Distance(new_point, transform.position) <= 0.005f)//相机精度修正
            transform.position = new_point;
        else
            transform.position = Vector3.Lerp(transform.position,new_point, delayMoveFactor*Time.fixedDeltaTime);
    }

    public void SetOffsetY(float y)
	{
        offsetY = y;
	}
}
