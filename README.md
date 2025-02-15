# Launching the Application

## Prerequisites

- **Docker Desktop** (for local Redis and SQL Server)
- **.NET SDK** (for the API)
- **Node.js & Angular CLI** (for the client)
- **Stripe CLI** (for webhook testing)

## Running the Application

### 1. Start Docker Containers

- Ensure that **Docker Desktop** is running in local mode.
- Start the containers for **Redis** and **SQL Server**.

### 2. Start the .NET API

```sh
cd API
dotnet watch run
```

### 3. Start the Angular Client

```sh
cd CLIENT
ng serve
```

### 4. Start the Stripe Webhook Listener

```sh
stripe listen --forward-to https://localhost:5001/api/payments/webhook -e payment_intent.succeeded
```

## Useful Commands

### Generate Components, Services, Guards, and Interceptors

From the `CLIENT` folder, use the following command:

```sh
ng g c/s/g/i <name>
```

where:

- `c` : **Component**
- `s` : **Service**
- `g` : **Guard**
- `i` : **Interceptor**

## Performance Optimization

> "There are two hard things in Computer Science: cache invalidation and naming things."
>
> — _Phil Karlton_

> "The three main performance optimization techniques are caching, caching and... caching."
>
> — _Unknown_

Performance optimization primarily involves caching. Avoiding unnecessary database queries is often the best improvement you can make.

### API Optimizations

- **Caching** data and queries. : Done (cd Cached Attribute and InvalidateCache Attribute in API)

### Client Optimizations

- **Lazy Loading** to load Angular modules on demand : Done (cf app.routes in Client)

## E-Commerce Roadmap

### Management System

- **Product** management 🛍️
- **Stock** management 📦
- **Inventory** management 📊

### Customer Service

- Enhancing customer experience
- Managing returns and complaints

### And More!

🚀 Many improvements to come...
