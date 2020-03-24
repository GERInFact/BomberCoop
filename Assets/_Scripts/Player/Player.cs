using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Controller;
using Assets._Scripts.Weapons;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Player
{
    public class Player : MonoBehaviour
    {
        #region controller

        private IController _controller;

        #endregion


        #region health

        [SerializeField]
        private int _health;
        public int Health
        {
            get => this._health;
            set => this._health = value;
        }
        [SerializeField] private Slider _healthBar;

        #endregion

        #region weaponsystem

        private IWeapon _activeWeapon;
        public List<IWeapon> Weapons { get; } = new List<IWeapon>();

        #endregion

        public void Init()
        {
            this._controller = this.GetComponent<DefaultController>() ?? this.gameObject.AddComponent<DefaultController>();
            this._controller.Init();
            this._healthBar.maxValue = this.Health;
        }

        public void Move()
        {
            this._controller?.Move();
        }

        public void Attack()
        {
            if (this._controller.IsInteracting()) this._activeWeapon.Draw(this.transform.position + Vector3.back * 6);
        }

        public void SetActiveWeapon(int weaponSlot)
        {
            if (this.IsWeaponAvailable(weaponSlot))
                this._activeWeapon = this.Weapons[weaponSlot];
        }

        private bool IsWeaponAvailable(int weaponSlot)
        {
            return weaponSlot < this.Weapons.Count && weaponSlot > -1;
        }

        public void DropWeapons()
        {
            this.Weapons.Clear();
        }

        public void CollectWeapon([NotNull] IWeapon weapon)
        {
            this.Weapons.Add(weapon);
        }

        public void UpdateHealthBar()
        {
            this._healthBar.value = this._health;
        }
    }
}
