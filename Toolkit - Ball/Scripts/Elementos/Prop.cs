using System.Collections;
using UnityEngine;

namespace Ball
{
    public class Prop : MonoBehaviour
    {
        public void AccionDestruir() {
            GameObject.Destroy(gameObject);
        }
        public void AccionEnable(bool enable)
        {
            gameObject.SetActive(enable);
        }

    }
}