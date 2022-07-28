using MQTTnet.Server;

namespace MqttAPI
{
    public sealed class MqttController
    {
        public MqttController()
        {
            // Inject other services via constructor.
        }

        public Task OnClientConnected(ClientConnectedEventArgs eventArgs)
        {
            return Task.CompletedTask;
        }


        public Task ValidateConnection(ValidatingConnectionEventArgs eventArgs)
        {
            
            return Task.CompletedTask;
        }
    }
}
