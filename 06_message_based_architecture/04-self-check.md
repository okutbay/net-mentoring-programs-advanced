_Questions for the self-check:_

1. What is Message Based Architecture? What is the difference between Message Based Architecture and Event Based Architecture?’
2. What is Message Broker? How do message brokers work?
3. When should you use message brokers?
4. Name and describe any distribution pattern.
5. What are the advantages and disadvantages of using message broker?
6. What is the difference between Queue and Topic?
7. What are the typical failures in MBA? How can you address them? What is Saga pattern?

ANSWERS

1. Message-Based Architecture is an architecture where components communicate by sending messages, usually asynchronously through queues or brokers.
Message-based: A message is commonly intended for a particular receiver to process, e.g., CreateOrder.
Event-based: An event announces that something already happened, e.g., OrderCreated. Multiple services may react independently.

2. A Message Broker is middleware that receives messages from producers and delivers them to consumers.

3. Use them when we need:
Asynchronous communication
Loose coupling between services
Reliable message delivery
Load balancing / buffering
Communication between microservices
Systems that must handle temporary service failures

4. Publish/Subscribe (Pub/Sub):
A producer publishes a message/event to a topic.
Multiple consumers subscribe to that topic.
Each subscriber can independently receive and process the message.
Example: OrderCreated may be processed by Payment, Inventory, and Notification services.

5. Advantages:
Loose coupling
Asynchronous processing
Better scalability
Reliable message delivery
Improved fault tolerance

Disadvantages:
Additional infrastructure and complexity
Broker can become a bottleneck or failure point
Harder debugging and monitoring
Message ordering and duplication can be difficult to manage

6. Difference between Queue and Topic
Queue: Usually one message → one consumer. If several consumers exist, they compete for messages. Good for distributing work.
Topic: One message → potentially many subscribers. Each subscriber can receive its own copy. Good for Pub/Sub and event distribution.


7. Typical failures include:
Lost messages → persistent/durable messages and acknowledgements
Duplicate messages → idempotent consumers
Consumer failure → retries and Dead Letter Queues (DLQ)
Broker failure → clustering/replication
Message-processing errors → retries, DLQ, monitoring
Distributed transaction failures → Saga pattern

Saga Pattern: A Saga manages a business transaction across several services as a sequence of local transactions. If one step fails, compensating transactions undo or compensate for previously completed steps.
Example: Create Order → Reserve Stock → Process Payment → Arrange Shipping
If payment fails, a compensating action can release the reserved stock.