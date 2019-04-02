using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : StateChange
{
    List<float[]> movementAmounts = new List<float[]>();
    int movementNumber = 1;
    float currentMovement;
    float duration = 1f;
    float baseDuration;
    float currentMovementLeft = 0;
    Vector3 currentDir;
    Vector3 baseDir;


    public Movement(string name, Vector3 dir, float dur, Entity entity)
    {
        e = entity;
        duration = dur;
        baseDuration = dur;
        baseDir = dir;
        if (name == "walk")
        {
            movementAmounts.Add(new float[2] { 1f, 0f });
        }
        else if (name == "attack")
        {
            movementAmounts.Add(new float[2] { 0.5f, 0f });
            movementAmounts.Add(new float[2] { -0.5f, 0f });
        }
        currentDir = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.up, baseDir, Vector3.forward), Vector3.forward) * new Vector3(movementAmounts[0][1], movementAmounts[0][0]);
        currentMovement = currentDir.magnitude;
        currentMovementLeft = currentMovement;
    }

    public Movement(Vector3 dir, float dur, float distance, Entity entity)
    {
        e = entity;
        duration = dur;
        currentDir = dir;
        currentMovement = distance;
        currentMovementLeft = currentMovement;

    }

    public bool Tick2(float delta)
    {

        float amountMoved = (Time.deltaTime * (currentMovement / duration));
        if (amountMoved > currentMovementLeft)
        {
            amountMoved = currentMovementLeft;
        }
        e.transform.Translate(currentDir.normalized * amountMoved);
        currentMovementLeft -= amountMoved;
        e.transform.position = new Vector3(e.transform.position.x, e.transform.position.y, e.transform.position.y);
        if (currentMovementLeft <= 0)
        {
            return true;
        }
        return false;
    }

    public override bool Tick(float delta)
    {
        float amountMoved = (Time.deltaTime * (currentMovement / duration));
        if (amountMoved > currentMovementLeft)
        {
            amountMoved = currentMovementLeft;
        }
        e.transform.Translate(currentDir.normalized * amountMoved);
        currentMovementLeft -= amountMoved;
        e.transform.position = new Vector3(e.transform.position.x, e.transform.position.y, e.transform.position.y);
        if (currentMovementLeft <= 0)
        {
            if (movementNumber >= movementAmounts.Count)
            {
                return true;
            }
            else
            {
                currentDir = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.up, baseDir, Vector3.forward), Vector3.forward) * new Vector3(movementAmounts[movementNumber][1], movementAmounts[movementNumber][0]);
                currentMovement = currentDir.magnitude;
                currentMovementLeft = currentMovement;
                duration = baseDuration;
                movementNumber += 1;
            }
        }
        return false;
    }
}
