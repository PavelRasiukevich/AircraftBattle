using System;
using System.Linq;
using Assets.Scripts.Utils;
using UnityEngine;

namespace GameObjectComponents
{
    public class BodySettings : MonoBehaviour
    {
        [SerializeField] private GameObject _body;

        public void Config(Color color)
        {
            try
            {
                _body.GetComponent<Renderer>().materials.Last(
                    mat => mat.name.Contains(Const.AircraftMaterialName)).color = color;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}