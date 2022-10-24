using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Ball{

    [System.Serializable]
    public struct Entrada{
        #pragma warning disable CS0649
        [SerializeField]
        private string nombre;
        [SerializeField]
        private Tecla tecla;
        [SerializeField]
        private UnityEvent evento;
        #pragma warning restore CS0649

        public string GetNombre(){
            return nombre;
        }
        public Tecla GetTecla(){
            return tecla;
        }
        public void ActivarEvento(){

            if (evento == null)
                return;
            evento.Invoke();

        }

    }

    public class ManagerEntrada : MonoBehaviour{

        [SerializeField]
        private Entrada []entrada = null;

        private void Update(){

            if (entrada == null)
                return;
            for (int i = 0; i < entrada.Length; i++)
                if (entrada[i].GetTecla().IsClickDown())
                    entrada[i].ActivarEvento();

        }
            
    }

}