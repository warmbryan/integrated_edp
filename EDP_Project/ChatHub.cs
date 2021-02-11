
/* Unmerged change from project '10_App_Code'
Before:
using System;
After:
using Microsoft.AspNet.SignalR;
using System;
*/
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace EDP_Project
{
    public class ChatHub : Hub
    {
        public void Send(string tempId, string message)
        {
            Clients.All.broadcastMessage(tempId, message);
        }

        public void Join(string name)
        {

        }

        public Task JoinChat(string roomId, string userId)
        {
            return Groups.Add(Context.ConnectionId, roomId);
        }

        public Task EndChat(string roomId, string userId)
        {
            return Groups.Remove(Context.ConnectionId, roomId);
        }
    }
}