using UnityEngine;

namespace Assets._Scripts.Controller
{
    public interface IController
    {
        void Init();
        void Move();
        bool IsInteracting();
    }
}
