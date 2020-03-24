using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets._Scripts.Weapons
{
    public class Vortex : MonoBehaviour, IWeapon
    {

        public void Draw(Vector3 postition)
        {
            Instantiate(this, postition + Vector3.back * 10, this.transform.rotation);
        }

        public void ApplyDamage(Player.Player player)
        {
            player.Health = 0;
        }

        public void Upgrade()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            var player = other?.GetComponent<Player.Player>();
            if (player == null) return;

            this.ApplyDamage(player);
            Destroy(this.gameObject, .1f);
        }
    }
}
