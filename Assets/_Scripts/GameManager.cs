using System.Collections.Generic;
using System.Linq;
using Assets._Scripts.Controller;
using Assets._Scripts.ExtensionMethods;
using Assets._Scripts.Map;
using Assets._Scripts.Weapons;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets._Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TileMap _map;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private List<GameObject> _weaponPrefabs = new List<GameObject>();
        private readonly List<Player.Player> _players = new List<Player.Player>();


        private void Awake()
        {
            if (!this._map)
                this._map = FindObjectOfType<TileMap>() ?? this.gameObject.AddComponent<TileMap>();

            if (!this._players.Any())
                this._players.AddRange(FindObjectsOfType<Player.Player>());

            Random.InitState((int)System.DateTime.Now.Ticks);
        }

        private void Start()
        {
            this.InitGame();
        }

        public void InitGame()
        {
            DefaultController.ControllerNumber = 0;

            this._map.Create();
            this.RemovePlayers();
            this.SpawnPlayers();
            this.SetPlayerStates();
        }

        private void RemovePlayers()
        {
            if (!this._players.Any()) return;

            this._players.ForEach(p => Destroy(p.gameObject));
            this._players.Clear();
        }

        private void SpawnPlayers()
        {
            for (var i = 0; i < 2; i++)
            {
                var floorTileIndex = Random.Range(0, this._map["FloorTile"].Count);
                var spawnPoint = this._map["FloorTile"][floorTileIndex].transform.position;
                var player = Instantiate(this._playerPrefab, spawnPoint, Quaternion.identity);
                this._players.Add(player.GetComponent<Player.Player>() ?? player.AddComponent<Player.Player>());
            }
        }

        private void SetPlayerStates()
        {
            this._players.ForEach(p =>
            {
                p.Init();
                this.AttachPlayerToMap(p);
                p.Weapons.AddRange(this._weaponPrefabs.Select(w => w?.GetComponent<IWeapon>()));
                p.SetActiveWeapon(1);
            });
        }

        private void AttachPlayerToMap([NotNull] Player.Player p)
        {
            p.transform.parent = this._map.transform;
            var playerPosition = p.transform.position;
            p.transform.localPosition = new Vector3(playerPosition.x, p.transform.localScale.y / 2, playerPosition.z);
        }

        private void FixedUpdate()
        {
            this._players.ForEach(p => p.Move());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                this._players[0].SetActiveWeapon(0);

            for (var i = 0; i < this._players.Count; i++)
                this.HandlePlayer(this._players[i]);
        }

        private void HandlePlayer(Player.Player player)
        {
            if (player.Health <= 0)
                this.RemoveTerminatedPlayer(player);
            else
            {
                player.Attack();
                player.UpdateHealthBar();
            }
        }

        private void RemoveTerminatedPlayer([NotNull] Player.Player player)
        {
            player.DropWeapons();
            this._players.Remove(player);
            Destroy(player.gameObject);
        }
    }
}
