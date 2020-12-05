using Controllers.Player;
using Controllers.Player.Upgrades;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(Vitals))]
    [RequireComponent(typeof(PlayerInteract))]
    public class PlayerManager : MonoBehaviour
    {
        private PlayerInteract _playerInteract;
        private Vitals _vitals;
        private PlayerMovement _playerMovement;

        private void Start()
        {
            _playerInteract = GetComponent<PlayerInteract>();
            _vitals = GetComponent<Vitals>();
            _playerMovement = GetComponent<PlayerMovement>();
        }
    }
}