using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 targetOffset;
    [SerializeField] protected LayerMask layerMask;

    [SerializeField] private float xSpeed = 40f;
    [SerializeField] private float ySpeed = 100f;

    [SerializeField] protected float distanceBase = 15f;
    [SerializeField] protected float distanceMin = 5f;
    [SerializeField] protected float distanceMax = 25f;
    [SerializeField] protected float scrollSence = 15f;

    protected float currentDistance;

    protected float x = 0.0f;
    protected float y = 0.0f;

    void FixedUpdate()
    {
        if (target == null)
            return;
    }

    protected Vector3 CheckBackwardCollision(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 newPosition = endPosition;
        RaycastHit rayHit = new RaycastHit();
        if (Physics.Linecast(startPosition, endPosition, out rayHit, layerMask))
        {
            //newPosition = new Vector3(rayHit.point.x + rayHit.normal.x * 0.5f, endPosition.y, rayHit.point.z + rayHit.normal.z * 0.5f);
            newPosition = rayHit.point * 0.5f;// new Vector3(rayHit.point.x + rayHit.normal.x * 0.5f, rayHit.point.y + rayHit.normal.y * 0.5f, rayHit.point.z + rayHit.normal.z * 0.5f);
        }

        return newPosition;

    }

    protected void GetInput()
    {
        if (!Cursor.visible)
        {
            distanceBase -= Input.GetAxis("Mouse ScrollWheel") * scrollSence * Time.deltaTime;
            distanceBase = Mathf.Clamp(distanceBase, distanceMin, distanceMax);

            x += Input.GetAxis("Mouse X") * xSpeed * currentDistance * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

        }
    }

}
