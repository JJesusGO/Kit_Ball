using System.Collections;
using UnityEngine;

namespace Ball
{
    public class Disparador : MonoBehaviour
    {
        [SerializeField]
        private float fuerza = 300.0f;

        public void AccionDisparar() {
            BallPlayer player = BallPlayer.GetInstancia();
            Vector3 direccion = transform.forward.normalized;
            player.AddFuerza(direccion * fuerza);
        }
    }
}