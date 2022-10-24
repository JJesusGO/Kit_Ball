using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;

namespace Iluminacion{

    public enum ColisionTipo{
        FISICA,TRIGGER
    }
    public enum ColisionEstado {
        ENTER,EXIT
    }
    public delegate void ColisionEvento(ColisionInformacion informacion);

    public struct ColisionInformacion{

        private GameObject      objeto  ;
        private ColisionEstado estado;
        private ColisionTipo    tipo;
        private Colision        colision1,
                                colision2;  

        public ColisionInformacion(GameObject objeto,Colision colision1,Colision colision2, ColisionTipo tipo, ColisionEstado estado){
            
            this.colision1 = colision1;
            this.colision2 = colision2;
            this.objeto = objeto;
            this.estado = estado;
            this.tipo   = tipo;

        }

        public Colision       GetColision(){
            return colision1;
        }

        public GameObject     GetObjetoImpacto() {
            return objeto;
        }
        public Colision       GetColisionImpacto() {
            return colision2;
        }
       
        public ColisionEstado GetColisionEstado() {
            return estado;
        }
        public ColisionTipo   GetColisionTipo(){
            return tipo;
        }


        public bool IsColisionTipo(ColisionTipo tipo){
            return GetColisionTipo() == tipo;
        }
        public bool IsColisionEstado(ColisionEstado estado){
            return GetColisionEstado() == estado;
        }

    }
    [RequireComponent(typeof(Collider))]
    public class Colision : MonoBehaviour{

        [Header("Eventos")]
        [SerializeField]
        private UnityEvent triggerEnter = null;
        [SerializeField]
        private UnityEvent triggerExit = null;
        [SerializeField]
        private UnityEvent colisionEnter = null;
        [SerializeField]
        private UnityEvent colisionExit = null;

        private ColisionTipo         colisiontipo   = ColisionTipo.TRIGGER;
        private Collider                  colision  = null;

        private void Awake(){
            colision = GetComponent<Collider>();
            colisiontipo = (colision.isTrigger)?ColisionTipo.TRIGGER:ColisionTipo.FISICA;
        }

        private void OnTriggerEnter(Collider otro) { 
        
            Rigidbody rigidbody = otro.attachedRigidbody;

            GameObject objeto   = null;
            Colision   colision = null;                      

            if (rigidbody != null){
                objeto   = rigidbody.gameObject;
                colision = otro.GetComponent<Colision>();
            }
            else{
                objeto = otro.gameObject;
                colision = otro.gameObject.GetComponent<Colision>();
            }
            ActivarEvento(new ColisionInformacion(objeto, this,colision, ColisionTipo.TRIGGER, ColisionEstado.ENTER));

        }
        private void OnTriggerExit(Collider otro) { 
            
            Rigidbody rigidbody = otro.attachedRigidbody;

            GameObject objeto   = null;
            Colision   colision = null;                      

            if (rigidbody != null){
                objeto   = rigidbody.gameObject;
                colision = otro.GetComponent<Colision>();
            }
            else{
                objeto   = otro.gameObject;
                colision = otro.gameObject.GetComponent<Colision>();
            }
            ActivarEvento(new ColisionInformacion(objeto, this,colision, ColisionTipo.TRIGGER, ColisionEstado.EXIT));

        }
        private void OnCollisionEnter(Collision otro) { 
        
            Rigidbody rigidbody = otro.collider.attachedRigidbody;

            GameObject objeto   = null;
            Colision   colision = null;                      

            if (rigidbody != null){
                objeto   = rigidbody.gameObject;
                colision = otro.collider.GetComponent<Colision>();
            }
            else{
                objeto   = otro.gameObject;
                colision = otro.collider.GetComponent<Colision>();
            }
            ActivarEvento(new ColisionInformacion(objeto, this,colision, ColisionTipo.FISICA, ColisionEstado.ENTER));

        }
        private void OnCollisionExit(Collision otro) { 
        
            Rigidbody rigidbody = otro.collider.attachedRigidbody;

            GameObject objeto   = null;
            Colision   colision = null;                      

            if (rigidbody != null){
                objeto   = rigidbody.gameObject;
                colision = otro.collider.GetComponent<Colision>();
            }
            else{
                objeto   = otro.gameObject;
                colision = otro.collider.GetComponent<Colision>();
            }
            ActivarEvento(new ColisionInformacion(objeto, this,colision, ColisionTipo.FISICA, ColisionEstado.EXIT));

        }

        private void ActivarEvento(ColisionInformacion informacion){
            if (informacion.GetColisionTipo() == ColisionTipo.FISICA)
            {
                if (informacion.GetColisionEstado() == ColisionEstado.ENTER)
                {
                    colisionEnter.Invoke();
                }
                else {
                    colisionExit.Invoke();
                }
            }
            else {
                if (informacion.GetColisionEstado() == ColisionEstado.ENTER)
                {
                    triggerEnter.Invoke();
                }
                else
                {
                    triggerExit.Invoke();
                }
            }
        }
            
        public ColisionTipo         GetColisionTipo(){
            return colisiontipo;
        }   

    }

}