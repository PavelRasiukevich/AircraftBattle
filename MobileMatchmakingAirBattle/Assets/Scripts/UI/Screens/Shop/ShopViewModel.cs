using UnityEngine;

namespace UI.Screens.Shop
{
    public class ShopViewModel : MonoBehaviour
    {
        public void Load(GameObject modelPrefab)
        {
            if (transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);

            GameObject model = Instantiate(modelPrefab, transform);
            model.transform.position = transform.position;
            model.transform.rotation = transform.rotation;
        }
    }
}