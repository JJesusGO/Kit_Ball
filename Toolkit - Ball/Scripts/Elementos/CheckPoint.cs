using System.Collections;
using UnityEngine;

namespace Ball
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField]
        private Transform position;
        public void AccionSetCheckpoint() {
            if (position == null)
                return;
            BallPlayer player = BallPlayer.GetInstancia();
            player.SetCheckpoint(position.position);
        }
    }
}