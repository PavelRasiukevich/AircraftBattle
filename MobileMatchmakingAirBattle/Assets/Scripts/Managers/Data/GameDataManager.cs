using Core;
using ScriptableObjects;
using TO;
using UnityEngine;

namespace Data
{
    public class GameDataManager : BaseInstance<GameDataManager>
    {
        [SerializeField] private PlanesDataScriptableObject _planes;
        public PlanesDataScriptableObject PlanesData => _planes;

        public PlaneInfo CurrentPlane { get; set; }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
        
    }
}