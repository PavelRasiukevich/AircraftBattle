using Assets.Scripts.AirCrafts;
using Assets.Scripts.Utils;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public AirCraft AirCraft { get; set; }

    private void OnCollisionEnter(Collision other)
    {
        if (UtilityMethods.ValidateCollision(other))
            AirCraft.Die();
    }
}
