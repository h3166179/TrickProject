using UnityEngine;

public class CameraParallax : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField]private Camera camera;

    private Vector2 originalPoint;//��ʼx��y
    private float startZ;//��ʼz

    private Vector2 cameraMoveDir;
    private float deapthZ;
    private float clipPlane;//�ü��棨ȷ���������������Ǹ�������
    private float parallaxFactor;//�Ӳ�ϵ��


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
