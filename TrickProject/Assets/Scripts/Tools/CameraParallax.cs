using UnityEngine;

public class CameraParallax : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField]private Camera camera;

    private Vector2 originalPoint;//初始x、y
    private float startZ;//初始z

    private Vector2 cameraMoveDir;
    private float deapthZ;
    private float clipPlane;//裁剪面（确定是在正数方向还是负数方向）
    private float parallaxFactor;//视差系数


    private void Start()
    {
        player = GameManager.Instance.GetPlayer().transform;
        camera = GameManager.Instance.GetCamera();

        //Init
        originalPoint = new Vector2(transform.position.x,transform.position.y);
        startZ = transform.position.z;
        //cameraMoveDir = (Vector2)(camera.transform.position - transform.position);
        cameraMoveDir = new Vector2(camera.transform.position.x - transform.position.x, 0);
        deapthZ = startZ - player.transform.position.z;
        clipPlane = -player.transform.position.z+(deapthZ > 0 ? camera.farClipPlane : camera.nearClipPlane);//camera.nearClipPlane
        parallaxFactor = Mathf.Abs(startZ / clipPlane);
    }

    private void Update()
    {
        Vector2 new_point = originalPoint + (cameraMoveDir * parallaxFactor);
        transform.position = new Vector3(new_point.x, new_point.y, startZ);
    }
}
