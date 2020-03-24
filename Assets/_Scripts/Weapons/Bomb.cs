using System.Xml.Serialization;
using Assets._Scripts.ExtensionMethods;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets._Scripts.Weapons
{
    public class Bomb : MonoBehaviour, IWeapon
    {
        #region member/props
        [SerializeField] private int _baseDamage;
        public int BaseDamage => this._baseDamage;
        #endregion

        #region interface methods
        public void Draw(Vector3 position)
        {
            Instantiate(this, position, Quaternion.identity);
        }

        public void ApplyDamage(Player.Player player)
        {
            if (player.Health > 0)
                player.Health -= this._baseDamage;
            else
                player.Health = 0;
        }

        public void Upgrade()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            var player = other?.GetComponent<Player.Player>();
            if (player == null) return;

            this.ApplyDamage(player);
            Destroy(this.gameObject, .1f);
        }
    }
}
