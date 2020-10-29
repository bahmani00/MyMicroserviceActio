

Project Structure:
MyMicroserviceActio.Common -> Messages(RabbitMQ)
MyMicroserviceActio.Api -> MongoDB
MyMicroserviceActio.Services.Activities -> MongoDB
MyMicroserviceActio.Services.Identity -> MongoDB

requests => API Gateway(MyMicroserviceActio.Api) => push a message to service bus(add_activity msg) => subscribers recieve the message(Activity service) =>
activity_add event raised => API gateway receives and send response back.
