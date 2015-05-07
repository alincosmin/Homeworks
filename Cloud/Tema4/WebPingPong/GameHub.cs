using Microsoft.AspNet.SignalR;

namespace WebPingPong
{
    public class GameHub : Hub
    {
        private static int[][] _positions = {new int[]{0,0}, new[]{0,0}}; 

        public void SendMove(int playerNumber, int xMove, int yMove)
        {
            _positions[playerNumber][0] = xMove;
            _positions[playerNumber][1] = yMove;

            Clients.All.broadcastPositions(_positions[0], _positions[1]);
        }

        public void SendScores(int scoreA, int scoreB)
        {
            Clients.All.updateScores(scoreA, scoreB);
        }

        public void Connect()
        {
            var clientId = Context.ConnectionId;
            Clients.Caller.assignId(clientId);
        }
    }
}