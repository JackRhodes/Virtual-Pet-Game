# Virtual Pet Game

## Introduction

An API for a simplistic game that will adding animals against users. Animals will slowly increase in hunger over time and decrease in happiness, the user must then call the API in order to keep the animals status as happy.

This API hopes to fulfill the following requirements:

* Users have animals
* Petting animals makes them happy 
* Feeding animals makes them less hungry
* Animals start “neutral” on both metrics
* Happiness decreases over time / hunger increases over time
* Users can own multiple animals of different types
* Different animal types have metrics which increase/decrease at different rates

The API uses Swagger to allow calling of the API, however any other HTTP tool can call it (Postman was used before swagger was integrated). Swagger can be accessed via http://localhost:PORT/swagger

The API uses a singleton service to store its data, therefore all data is reset upon closure of the application. The application was designed to be easily ported to EF Core, and operates numerous work-arounds to get round features that EF Core automatically provides. For example, each repository has a GetNextId method while in reality EF cores sequential database fields will generate this. Another example is that EF core allows updating of data, while System.Collection.Generic only allows adding and removing, therefore work-arounds have been implemented.

## Pre-requisites

* .NET Core 2.1
* An API caller (swagger integrated)