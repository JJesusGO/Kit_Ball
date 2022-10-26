using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Ball {
    [RequireComponent(typeof(Rigidbody))]
    public class BallPlayer : MonoBehaviour {

        [SerializeField]
        private float fuerzaMovimiento = 200.0f;
        [SerializeField]
        private Vector2 fuerzaDireccion = Vector2.one;
        [SerializeField]
        private float velocidadMaxima = 10.0f;

        private static BallPlayer instancia = null;
        private ManagerAplicacion manager;
        private ManagerBall mb;
        private Rigidbody rb;

        private Vector3 checkpoint = Vector3.zero;

        private void Start() {
            manager = ManagerAplicacion.GetInstancia();
            mb = ManagerBall.GetInstancia();
            rb = GetComponent<Rigidbody>();

            checkpoint = transform.position;
        }

        public void Update() {
            UpdateVelocidadMaxima();
        }

        private void UpdateVelocidadMaxima() {

            if (rb.velocity.magnitude > velocidadMaxima) { 
                rb.velocity = rb.velocity.normalized * velocidadMaxima;
            }
        }

        public void AddFuerza(Vector3 fuerza)
        {
            rb.AddForce(fuerza, ForceMode.Force);
        }
        public void AddFuerzaEmpuje(Vector3 fuerza, float multiplicador)
        {
            AddFuerza(fuerzaMovimiento * fuerza * multiplicador);
        }
        public void AddFuerzaEmpuje(Vector3 fuerza)
        {
            AddFuerzaEmpuje(fuerza, 1);
        }

        public void SetCheckpoint(Vector3 position) {
            checkpoint = position;
        }
        public void AccionAdelante() {
            if (!mb.IsJugando())
                return;
            Camera camera = manager.GetCamera();
            Vector3 direccion = manager.GetCamera().transform.forward;
            direccion = (new Vector3(direccion.x, 0, direccion.z)).normalized;
            AddFuerzaEmpuje(direccion, fuerzaDireccion.y);
        }
        public void AccionAtras()
        {
            if (!mb.IsJugando())
                return;
            Camera camera = manager.GetCamera();
            Vector3 direccion = -manager.GetCamera().transform.forward;
            direccion = (new Vector3(direccion.x, 0, direccion.z)).normalized;
            AddFuerzaEmpuje(direccion, fuerzaDireccion.y);
        }

        public void AccionDerecha()
        {
            if (!mb.IsJugando())
                return;
            Camera camera = manager.GetCamera();
            Vector3 direccion = manager.GetCamera().transform.right;
            direccion = (new Vector3(direccion.x, 0, direccion.z)).normalized;
            AddFuerzaEmpuje(direccion, fuerzaDireccion.x);
        }

        public void AccionIzquierda()
        {
            if (!mb.IsJugando())
                return;
            Camera camera = manager.GetCamera();
            Vector3 direccion = -manager.GetCamera().transform.right;
            direccion = (new Vector3(direccion.x, 0, direccion.z)).normalized;
            AddFuerzaEmpuje(direccion, fuerzaDireccion.x);
        }

        public void AccionTPCheckpoint() {
            transform.position = checkpoint;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public static BallPlayer GetInstancia() {
            if (instancia == null)
                instancia = GameObject.FindObjectOfType<BallPlayer>();
            return instancia;
        }

    }

}

