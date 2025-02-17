# 📌 What is this application made of?

> **Skinet** is an application built by following Neil Cummings' Udemy course. It consists of a **.NET 9 API** and an **Angular v18 client**.

## 🛠 API Features & Concepts

- Using the **.NET CLI**
- **Entity Framework** (EF) migrations
- **Stripe** integration
- **SQL Server** database
- **Redis** for caching
- **Caching strategies** with Redis
- **Generic Repository Pattern**
- **Specification Pattern**
- **Unit of Work** pattern
- **Role-based authentication**
- **Entity modeling**

## 🎨 Client Features & Concepts

- **Authentication**
- **Stripe payment integration**
- **Routing and navigation**
- **Lazy loading** for performance
- **Tailwind CSS** for styling
- **RxJS Observables**
- **Service-based architecture**
- **Error handling**
- **Signals**
- **Angular Material** components
- **TypeScript best practices**

## 🔍 Other Notable Features

- The solution is divided into **three projects**: `API`, `Core`, and `Infrastructure`.
- **Upstash** is used for a free **Redis database** in production.
- **Docker containers** manage local databases (configured in `docker-compose.yml`).
- **CI/CD workflow** is set up with **GitHub Actions** (`main_skinet-course.yml`).

---

# 🚀 What's Next?

## ✅ Building a New App on This Foundation

- Inspired by **Flowbite** E-Commerce UI components.

## 📚 Exploring More Courses by Neil Cummings

- **Microservices** with .NET & Next.js
- **Clean Architecture** with .NET & React

---

# 🏁 Launching the Application

## Prerequisites

Ensure you have the following installed:

- **Docker Desktop** (for local Redis and SQL Server)
- **.NET SDK** (for the API)
- **Node.js & Angular CLI** (for the client)
- **Stripe CLI** (for webhook testing)

## Running the Application

### 1️⃣ Start Docker Containers

```sh
# Ensure Docker is running
# Start Redis and SQL Server
cd project-root
docker-compose up -d
```

### 2️⃣ Start the .NET API

```sh
cd API
dotnet watch run
```

### 3️⃣ Start the Angular Client

```sh
cd CLIENT
ng serve
```

### 4️⃣ Start the Stripe Webhook Listener

```sh
stripe listen --forward-to https://localhost:5001/api/payments/webhook -e payment_intent.succeeded
```

---

# 🔧 Useful Commands

### 🎨 Generate Components, Services, Guards, and Interceptors

From the `CLIENT` folder, run:

```sh
ng g c/s/g/i <name>
```

where:

- `c` : **Component**
- `s` : **Service**
- `g` : **Guard**
- `i` : **Interceptor**

---

# ⚡ Optimization and Roadmap

> Below are key areas chosen for optimization and fixes planned for upcoming development sessions.

## 📈 Performance Optimization

> "There are two hard things in Computer Science: cache invalidation and naming things."
>
> — _Phil Karlton_

> "The three main performance optimization techniques are caching, caching and... caching."
>
> — _Unknown_

### 🔹 API Optimizations

✅ **Caching** data and queries (Implemented via `CachedAttribute` and `InvalidateCacheAttribute` in API).

### 🔹 Client Optimizations

✅ **Lazy Loading** (Implemented via `app.routes.ts` in CLIENT).

## 🛒 E-Commerce Roadmap

### 🔹 **Management System**

- ✅ **Product** management 🛍️
- 🏗️ **Product Review** system _(In progress)_
- ✅ **Email Service** (Resend integration)
- ✅ **Stock** management 📦
- 🔜 **Inventory** management 📊 _(Planned for next phase)_

### 🔹 **Customer Service**

- Enhancing **customer experience**
- Managing **returns and complaints**

## 🛠️ Next Fixes Scheduled

- 🔄 LAST NAME should always be uppercase.
- 🔡 First Name should start with an uppercase letter.
- 📏 Add more spacing around **Menu Button**.
- 🖱️ Implement **OnMouseHover** interactions.
- 🌍 **Internationalization (i18n) support**.

---

🎯 **Stay tuned for upcoming improvements!** 🚀
