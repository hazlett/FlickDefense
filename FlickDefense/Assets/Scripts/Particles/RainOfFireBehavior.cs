using UnityEngine;
using System.Collections;

public class RainOfFireBehavior : MonoBehaviour
{
    private ParticleSystem.CollisionEvent[] collisionEvents = new ParticleSystem.CollisionEvent[16];
    void OnParticleCollision(GameObject other)
    {
        int safeLength = particleSystem.safeCollisionEventSize;
        if (collisionEvents.Length < safeLength)
            collisionEvents = new ParticleSystem.CollisionEvent[safeLength];

        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);
        int i = 0;
        while (i < numCollisionEvents)
        {
            GameObject explosion = (GameObject)GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Particles/BomberExplosion"));
            explosion.transform.position = collisionEvents[i].intersection;
            i++;
        }
    }
}