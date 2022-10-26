using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

namespace Ball{

    public enum GameStatus { 
        JUGANDO, PERDIDO, GANADO
    }

    public class ManagerBall : MonoBehaviour {

        [Header("Gameplay")]
        [SerializeField]
        private int puntuacionTarget = 30;
        [Tooltip("El valor esta en segundos")]
        [SerializeField]
        private float tiempoMaximo = 30;
        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI []uiTiempo = null;
        [SerializeField]
        private TextMeshProUGUI []uiPuntuacion = null;
        [Header("Eventos")]
        [SerializeField]
        private UnityEvent inicio = null;
        [SerializeField]
        private UnityEvent perdido = null;
        [SerializeField]
        private UnityEvent ganado = null;


        private static ManagerBall instancia = null;

        private int puntuacion = 0;
        private Temporizador temporizador = null;
        private GameStatus status = GameStatus.JUGANDO;

        private void Start(){
            temporizador = new Temporizador(tiempoMaximo);
            temporizador.Start();
            status = GameStatus.JUGANDO;
            Cursor.lockState = CursorLockMode.Locked;
            if (inicio != null)
                inicio.Invoke();
            UpdatePuntuacionUI();
        }

        private void Update(){
            temporizador.Update();
            if (status == GameStatus.JUGANDO && temporizador.IsActivo()) {

                if (puntuacion >= puntuacionTarget)
                {
                    status = GameStatus.GANADO;
                    ganado.Invoke();
                }
                else {
                    status = GameStatus.PERDIDO;
                    perdido.Invoke();
                }
            }
            if (status == GameStatus.JUGANDO && puntuacion >= puntuacionTarget) {
                status = GameStatus.GANADO;
                ganado.Invoke();
            }

            UpdateTiempoUI();            
        }

        private void UpdateTiempoUI()
        {
            if (uiTiempo == null)
            {
                return;
            }

            float tiempo = tiempoMaximo - temporizador.GetTiempo();

            int minutos = (int)(tiempo / 60.0f);
            float segundos = tiempo - (minutos * 60.0f);


            string textoMinutos;
            if (minutos < 10)
            {
                textoMinutos = "0" + minutos;
            }
            else
            {
                textoMinutos = minutos.ToString();
            }

            string textoSegundos;
            if (segundos < 10)
            {
                textoSegundos = "0" + ((int)segundos);
            }
            else
            {
                textoSegundos = ((int)segundos).ToString();
            }

            foreach(TextMeshProUGUI tx in uiTiempo)
                tx.SetText(textoMinutos + " : " + textoSegundos);
        }
        private void UpdatePuntuacionUI() {
            if (uiPuntuacion == null)
                return;

            string textoPuntuacion;
            if (puntuacion < 10)
            {
                textoPuntuacion = "0" + puntuacion;
            }
            else
            {
                textoPuntuacion = puntuacion.ToString();
            }

            foreach (TextMeshProUGUI tx in uiPuntuacion)
                tx.SetText(textoPuntuacion);
        }

        public void AccionAddPuntaje(int puntaje) {
            puntuacion += puntaje;
            if(puntuacion < 0)
                puntuacion = 0;
            UpdatePuntuacionUI();
        }

        public void AccionReiniciarNivel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void AccionActivarCursor(bool enable) {
            if (enable)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }

        public bool IsJugando() {
            return status == GameStatus.JUGANDO;
        }

        public static ManagerBall GetInstancia(){
            if (instancia == null)
                instancia = GameObject.FindObjectOfType<ManagerBall>();
            return instancia;
        }

        

    }

}