
# Microservice & DDD implementation of Activity App

## Overview


## Project Structure:
1. MyMicroserviceActio.Common -> Messages(RabbitMQ)
2. MyMicroserviceActio.Api -> MongoDB
3. MyMicroserviceActio.Services.Activities -> MongoDB
4. MyMicroserviceActio.Services.Identity -> MongoDB

requests => API Gateway(MyMicroserviceActio.Api) => push a message to service bus(add_activity msg) => subscribers recieve the message(Activity service) =>
activity_add event raised => API gateway receives and send response back.
