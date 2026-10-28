_Questions for the self-check:_

1. What are the cons and pros of the Monolith architectural style?
2. What are the cons and pros of the Microservices architectural style?
3. What is the difference between SOA and Microservices?
4. [Open question] What does hybrid architectural style mean? Think of your current and previous projects and try to describe which architectural styles they most likely followed.
5. Name several examples of the distributed architectures. What do ACID and BASE terms mean.
6. Name several use cases where Serverless architecture would be beneficial.

ANSWERS
1. Monolith

Pros: Simple development, easy deployment, easier testing, good performance for small/medium systems.
Cons: Hard to scale components independently, difficult maintenance as code grows, technology lock-in, slower releases.

2. Microservices

Pros: Independent deployment, better scalability, fault isolation, technology flexibility, faster team autonomy.
Cons: Higher complexity, network overhead, distributed data challenges, more monitoring and DevOps effort.

3. SOA vs Microservices

SOA: Large, enterprise-level services often integrated through a central ESB (Enterprise Service Bus).
Microservices: Smaller, independently deployable services with lightweight communication (REST, messaging) and decentralized governance.

4. Hybrid Architecture

A combination of multiple architectural styles (e.g., Monolith + Microservices, Client-Server + Event-Driven).
Example: Many legacy enterprise systems start as a monolith while new modules are developed as microservices, resulting in a hybrid architecture.
All of my previous projects were following 3 layered architectural style. 

5. Distributed Architectures & ACID/BASE

Examples: Microservices, SOA, Event-Driven Architecture, Client-Server, Peer-to-Peer (P2P), Serverless.
ACID: Atomicity, Consistency, Isolation, Durability (strong transactional consistency).
BASE: Basically Available, Soft State, Eventual Consistency (high availability and scalability).

6. Serverless Use Cases

Event processing (file uploads, notifications).
API backends with variable traffic.
Scheduled jobs and automation.
IoT data processing.
Real-time stream processing.
Chatbots and lightweight web applications.