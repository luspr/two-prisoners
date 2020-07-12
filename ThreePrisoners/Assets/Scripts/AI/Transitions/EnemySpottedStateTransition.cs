using UnityEngine;

public class EnemySpottedStateTransition : AIStateTransition
{
    private float rangeOfSight = 20f;

    private int numSightRays = 20;

    Quaternion startingAngle = Quaternion.AngleAxis(-75, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);
    private Bot bot;
    private GameObject gameObject;
    private Transform transform;

    public EnemySpottedStateTransition(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
        bot = gameObject.GetComponent<Bot>();
    }

    public bool CheckCondition()
    {
        Transform enemy = LookForEnemy();
        bot.CurrentEnemy = enemy;
        return enemy != null;
    }

    private Transform LookForEnemy()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;
        for (var i = 0; i < 30; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, rangeOfSight))
            {
                // TODO change this to checking enum. Maybe new script "Character", where team membership is coded.
                if (hit.transform.tag == "Player")
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return hit.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * rangeOfSight, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }

}