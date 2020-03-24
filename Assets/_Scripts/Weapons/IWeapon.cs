using JetBrains.Annotations;
using UnityEngine;

namespace Assets._Scripts.Weapons
{
    public interface IWeapon
    {
        void Draw(Vector3 postition);
        void ApplyDamage([NotNull] Player.Player player);
        void Upgrade();
    }
}
