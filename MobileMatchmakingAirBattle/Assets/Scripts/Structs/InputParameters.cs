using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Structs
{
    public struct InputParameters
    {
        public Vector2 Input;
        public bool IsStickPressed;
        public float GasCoefficient;
        public float Delta;
    }
}
