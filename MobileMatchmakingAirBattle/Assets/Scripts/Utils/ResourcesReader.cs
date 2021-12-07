using UnityEngine;

namespace Utils
{
    // TODO: refactoring обсудить
    public class ResourcesReader
    {
        private static Sprite _winsSprite = null;

        public static Sprite WinsSprite
        {
            get
            {
                if (_winsSprite == null)
                    _winsSprite = Resources.Load<Sprite>("Sprite/Icons/Wins");
                return _winsSprite;
            }
        }

        private static Sprite _failsSprite = null;

        public static Sprite FailsSprite
        {
            get
            {
                if (_failsSprite == null)
                    _failsSprite = Resources.Load<Sprite>("Sprite/Icons/Fails");
                return _failsSprite;
            }
        }

        private static Sprite _fragsSprite = null;

        public static Sprite FragsSprite
        {
            get
            {
                if (_fragsSprite == null)
                    _fragsSprite = Resources.Load<Sprite>("Sprite/Icons/Frags");
                return _fragsSprite;
            }
        }

        private static Sprite _fightsSprite = null;

        public static Sprite FightsSprite
        {
            get
            {
                if (_fightsSprite == null)
                    _fightsSprite = Resources.Load<Sprite>("Sprite/Icons/Fights");
                return _fightsSprite;
            }
        }
    }
}