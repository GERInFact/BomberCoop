using UnityEngine;

namespace Assets._Scripts.Controller
{
    public class DefaultController : MonoBehaviour, IController
    {
        private Rigidbody _playerPhysics;
        public static int ControllerNumber;
        private int _controllerNumber;
        [SerializeField] private float _speed;


        private void Awake()
        {
            this._controllerNumber = ControllerNumber;
            ControllerNumber++;
        }

        public void Init()
        {
            this._playerPhysics = this.GetComponent<Rigidbody>() ?? this.gameObject.AddComponent<Rigidbody>();
        }

        public void Move()
        {
            this._playerPhysics.velocity = new Vector3(Input.GetAxis($"Horizontal{this._controllerNumber}"), 0, Input.GetAxis($"Vertical{this._controllerNumber}")) * this._speed * Time.fixedDeltaTime;
        }

        public bool IsInteracting()
        {
            return Input.GetButtonDown($"Fire{this._controllerNumber}");
        }
    }
}
