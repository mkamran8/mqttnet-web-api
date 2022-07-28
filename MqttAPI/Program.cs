using MqttAPI;
using MQTTnet.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedMqttServer(
           optionsBuilder =>
           {
               optionsBuilder.WithDefaultEndpoint();
           });

builder.Services.AddMqttConnectionHandler();
builder.Services.AddConnections();

builder.Services.AddSingleton<MqttController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseEndpoints(
    endpoints =>
    {
        endpoints.MapConnectionHandler<MqttConnectionHandler>(
            "/mqtt",
            httpConnectionDispatcherOptions => httpConnectionDispatcherOptions.WebSockets.SubProtocolSelector =
                protocolList => protocolList.FirstOrDefault() ?? string.Empty);
    });
app.UseMqttServer(
    server =>
    {
        /*
        * Attach event handlers etc. if required.
        */

        var _controller = new MqttController();

        server.ValidatingConnectionAsync += _controller.ValidateConnection;

        server.ClientConnectedAsync += _controller.OnClientConnected;

    });


app.MapGet("/", () => {
    return "use /mqtt for MQTT endpoints";
});
app.Run();

