using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PongNetworkManager : NetworkManager
{
    public Transform leftRacketSpawn;
    public Transform rightRacketSpawn;
    GameObject ball;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        //  base.OnServerAddPlayer(conn);
        Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == 2)
        {
            ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
            NetworkServer.Spawn(ball);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        //base.OnServerDisconnect(conn);
        if (ball != null)
        {
            NetworkServer.Destroy(ball);
        }

        base.OnServerDisconnect(conn);
    }

}

