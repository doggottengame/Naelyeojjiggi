using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] bool isGround;

    private void OnTriggerStay(Collider other)
    {
        isGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGround = false;
    }

    public void Jump()
    {
        isGround = false;
    }

    public bool IsGround()
    {
        return isGround;
    }
}
