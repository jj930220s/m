using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private float offsetX;
    private float offsetY;

    private void Start()
    {
        target = GameManager.Instance.player.transform;

        offsetX = transform.position.x-target.position.x;
        offsetY = transform.position.y-target.position.y;
    }


    private void Update()
    {
        if (target != null)
        {
            Vector3 pos = transform.position;

            pos.x = target.position.x + offsetX;
            pos.y = target.position.y + offsetY;

            transform.position = pos;
        }
    }


}
