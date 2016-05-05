using UnityEngine;
using System.Collections;

public interface Enemy {

    void GetHit(Vector2 bulletVelocity, float bulletMass, int damage);
}
