using UnityEngine;

public class InputUtils
{
    public static bool RayCastToScreenCenter(float range, out RaycastHit hit)
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 4f);
        return Physics.Raycast(ray, out hit, range);
    }
}