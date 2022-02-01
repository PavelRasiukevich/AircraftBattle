using System;
using System.Linq;
using Assets.Scripts.Utils;
using TO;
using UnityEngine;

namespace AirCrafts
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private GameObject _body; // body с основным материалом

        public void Config(PlaneSettings settings)
        {
            try
            {
                _body.GetComponent<Renderer>().materials.Last(
                    mat => mat.name.Contains(Const.AircraftMaterialName)).color = settings.Color;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}