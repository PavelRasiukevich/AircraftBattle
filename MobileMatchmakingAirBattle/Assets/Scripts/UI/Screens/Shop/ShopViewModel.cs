using GameObjectComponents;
using TO;
using UnityEngine;

namespace UI.Screens.Shop
{
    public class ShopViewModel : MonoBehaviour
    {
        public void Load(PlaneInfo plane)
        {
            if (transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);

            GameObject model = Instantiate(plane.PlaneShopModel, transform);
            model.transform.position = transform.position;
            model.transform.rotation = transform.rotation;
            model.GetComponent<BodySettings>().Config(plane.Settings.Color);
        }
    }
}