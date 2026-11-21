_Questions for the self-check:_

1. Explain the difference between terms: REST and RESTful. What are the six constraints?
2. HTTP Request Methods (the difference) and HTTP Response codes. What is idempotency? Is HTTP the only protocol supported by the REST?
3. What are the advantages of statelessness in RESTful services?
4. How can caching be organized in RESTful services?
5. How can versioning be organized in RESTful services?
6. What are the best practices of resource naming?
7. What are OpenAPI and Swagger? What implementations/libraries for .NET exist? When would you prefer to generate API docs automatically and when manually?
8. What is OData? When will you choose to follow it and when not?
9. What is Richardson Maturity Model? Is it always a good idea to reach the 3rd level of maturity?
10. What does pros and cons REST have in comparison with other web API types?

ANSWERS


1. REST vs RESTful and the six constraints

REST is an architectural style; RESTful describes a system that follows it. Its constraints are:

Client–server: separate client and server responsibilities.
Stateless: each request contains all necessary context.
Cacheable: define whether responses can be reused.
Uniform interface: resource identification, representations, self-descriptive messages, and hypermedia.
Layered system: clients need not know about intermediate layers.
Code on demand, optional: servers may provide executable client code.

2. HTTP methods, response codes, idempotency, and protocols
GET: retrieve; HEAD: retrieve headers only.
POST: submit data for processing, often creating a resource.
PUT: create or fully replace a resource at a known URI.
PATCH: partially update; DELETE: remove; OPTIONS: describe communication options.

Response classes: 1xx informational, 2xx success, 3xx redirection/cache validation, 4xx client errors, 5xx server errors. Common examples: 200 OK, 201 Created, 204 No Content, 304 Not Modified, 400 Bad Request, 401 Unauthorized, 403 Forbidden, 404 Not Found, 409 Conflict, 500 Internal Server Error.

Idempotency: repeating an identical request has the same intended server-side effect as performing it once, although responses may differ. GET, HEAD, PUT, DELETE, and OPTIONS are idempotent; POST and PATCH are not guaranteed to be.

HTTP is not required: REST is protocol-independent, although HTTP is its usual implementation.

3. Advantages of statelessness

Easier horizontal scaling, load balancing, failure recovery, and monitoring because requests do not depend on server-held client sessions. Resource data can still be stored in databases; stateless does not mean “no stored data.”

4. Caching

Use client caches, proxies, or CDNs with:

Cache-Control / Expires: freshness and cache permissions.
ETag / Last-Modified: validation through conditional requests, returning 304 when unchanged.
Vary: distinguish representations by request headers.

Use private for user-specific caches and no-store when responses must not be stored. Server-side application caching can complement HTTP caching.

5. Versioning

Common options: path (/v1/orders), query parameter (?api-version=1), custom header, or media type. Choose consistently, introduce versions mainly for breaking changes, and communicate deprecation and migration periods.

6. Resource naming

Use plural nouns, lowercase paths, and consistent hyphenation: /purchase-orders/42. Avoid CRUD verbs such as /getOrders; use HTTP methods instead. Express relationships with /customers/7/orders, keep nesting shallow, and use query parameters for filtering, sorting, and pagination.

7. OpenAPI, Swagger, and .NET

OpenAPI is a machine-readable HTTP API description standard. Swagger is a tooling ecosystem, including Swagger UI. .NET options include Microsoft.AspNetCore.OpenApi, Swashbuckle.AspNetCore, and NSwag.

Prefer automatic generation for implementation-first APIs to reduce documentation drift. Prefer a manually designed OpenAPI contract for contract-first development and cross-team agreement. In either case, manually add meaningful examples, business rules, and explanations.

8. OData

OData standardizes queryable APIs, including filtering, sorting, selection, expansion, and metadata, for example $filter=Price gt 100&$select=Name,Price.

Choose it for flexible, data-heavy querying and interoperable clients. Avoid it for simple endpoints or tightly controlled business workflows where its query complexity adds little value. Restrict query depth and cost when enabling it.

9. Richardson Maturity Model
Level 0: one endpoint, RPC-style messages.
Level 1: separate resources.
Level 2: meaningful HTTP methods and status codes.
Level 3: hypermedia controls, or HATEOAS, guide available next actions.

Level 3 is not always worth the complexity. Level 2 often meets practical needs, although strict REST includes hypermedia as part of its uniform interface.

10. REST compared with other API styles

Pros: widespread support, straightforward resource-oriented design, HTTP caching, interoperability, and easy debugging.

Cons: multiple round trips, over-fetching/under-fetching, and awkward modeling of some business operations.

GraphQL: more flexible client-selected data, but more complex query governance and caching.
gRPC: efficient typed calls and streaming, but less convenient for direct browser/public consumption.
SOAP: formal contracts and WS-* standards, but heavier messages and tooling.