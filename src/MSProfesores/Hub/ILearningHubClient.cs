using System;
namespace MSProfesores.Hub
{
	public interface ILearningHubClient
	{
        Task ReceiveMessage(string message);
    }
}

