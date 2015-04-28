using UnityEngine;
using System.Collections;

public class ArrowBehavior : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        switch (collider.tag)
        {
            case "Wall":
                {
                    UserStatus.Instance.DamageCastle(1);
                    Destroy(gameObject);
                }
                break;
            case "DeathCollider":
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
