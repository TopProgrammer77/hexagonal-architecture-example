### .NET projects

## Installing
* Install Visual Studio 2022
* Install PostgreSQL
* Install Apache Kafka
* Install Postman

1. PosgreSQL
  Execute the ./public.sql file to create the tables which are used in the application.
 - DB name: orders
 - Username: orders_admin
 - Password: orders_pass
  Url: localhost:5432
2. Apache Kafka
* Install the kafka, Go to kafka folder then run the Kafka server
```
bin/kafka-storage.sh format -t abcdefghijklmn123456789 -c config/kraft/server.properties
bin/kafka-server-start.sh config/kraft/server.properties
```

* Create Topic
```
bin/kafka-topics.sh --create --topic charge-order --bootstrap-server localhost:9092
bin/kafka-topics.sh --create --topic create-order --bootstrap-server localhost:9092
bin/kafka-topics.sh --create --topic order-charged --bootstrap-server localhost:9092
bin/kafka-topics.sh --create --topic order-not-charged --bootstrap-server localhost:9092
```

3. Start the Project
- Run `./dotnet/Multi-Language-Services.sln
- Go to Menu -> Debug -> BillingService then press Ctrl+F5. Billing Service will run on localhost:5001
- Go to Menu -> Debug -> OrderService then presse Ctrl+F5. Order Service will run on localhost:5000


4. Start the test

 Go to https://web.postman.co/ and click 'New Request' button
* first case
  
  * url: localhost:5000/order/1/1
  * Method: GET
* second case
  * url: localhost:5000/order
  * Method: POST
  * Body
```
{
    "CustomerId": "7",
    "OrderId": "8",
    "OrderItems": [
        {
            "OrderItemId": "6",
            "ProductName": "product1",
            "ProductPrice": "6.6"
        }
    ]
}
```

5. Watch the Console windows opened by Visual Studio(by pressing Ctrl+F5)
    
