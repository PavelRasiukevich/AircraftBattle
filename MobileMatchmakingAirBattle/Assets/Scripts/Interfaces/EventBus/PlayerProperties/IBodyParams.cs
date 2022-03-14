using UnityEngine;

namespace Interfaces.EventBus.PlayerProperties
{
    public interface IBodyParams : ISubscriber
    {
        void OnColorChanged(int actorNumber, Vector3 clr);
    }
}