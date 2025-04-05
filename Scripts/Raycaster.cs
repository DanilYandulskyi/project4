using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    
    public bool Cast(Vector3 origin, out Vector3 raycastHit)
    {
        float yOffset = 0.5f;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(origin);

        if (Physics.Raycast(ray, out hit, _layerMask))
        {
            raycastHit = new Vector3(hit.point.x, hit.point.y + yOffset, hit.point.z);
            return true;
        }

        raycastHit = Vector3.zero;

        return false;
    }
}
