﻿using Microsoft.AspNetCore.SignalR;

namespace MSProfesores.Hub
{
	public class LearningHub : Microsoft.AspNetCore.SignalR.Hub<ILearningHubClient>
    {
		public async Task BroadcastMessage(string message)
		{
			await Clients.All.ReceiveMessage(message);
		}

		public override async Task OnConnectedAsync()
		{
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception? exception)
		{
			await base.OnDisconnectedAsync(exception);
		}
	}
}

