using Assets.Scripts.Interfaces;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class Dome : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (UtilityMethods.ValidateTrigger(other, out IReturnToBattle result))
                result.Return();
        } 
    }
}