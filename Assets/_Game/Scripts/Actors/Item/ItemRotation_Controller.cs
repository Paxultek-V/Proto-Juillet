using UnityEngine;

public class ItemRotation_Controller : MonoBehaviour
{
    private void Update()
    {
        FixRotation();
    }


    private void FixRotation()
    {
        if (transform.rotation.eulerAngles.x > 0.2f || transform.rotation.eulerAngles.y > 0.2f)
        {
            Vector3 fixedRotation = transform.rotation.eulerAngles;
            fixedRotation.x = 0f;
            fixedRotation.y = 0f;

            transform.rotation = Quaternion.Euler(fixedRotation);
        }
    }
}
