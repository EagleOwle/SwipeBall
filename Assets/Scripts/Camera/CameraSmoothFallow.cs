using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothFallow : GameCamera
{
    public float smoothFollow = 4;
    public float smoothRotate = 60;

    Vector3 wantedPosition;
    float smooth;

    private void FixedUpdate()
    {
        if (target == null)
            return;

        SmoothFollow();
    }

    private void SmoothFollow()
    {
        currentDistance = Mathf.MoveTowards(currentDistance, distanceBase, scrollSence);

        wantedPosition = (target.transform.position - target.transform.forward * currentDistance);
        wantedPosition.y = target.transform.position.y + targetOffset.y;

        Vector3 newPosition = CheckBackwardCollision(target.transform.position, wantedPosition);

        if(wantedPosition != newPosition)
        {
            smooth *= 10;
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smooth);

        Vector3 point = target.transform.position + target.transform.forward;
        var direction = (point - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, smoothRotate * Time.deltaTime);

        smooth = smoothFollow;
    }
}
