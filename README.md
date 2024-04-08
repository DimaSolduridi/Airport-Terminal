# Airport Terminal Project

Welcome to the **Airport Terminal Project**! This project is an immersive simulation that showcases real-time flight movements and details within an airport terminal. It's built across three core components: a **Web API** developed with ASP.NET Core, a **SQL Database** for storing flight data, and a **React-based client application** that uses SignalR for real-time updates.

## 🚀 Components

### 1. **Web API (ASP.NET Core)**
The backbone of our project, this Web API handles all CRUD operations related to flight data. It's built on ASP.NET Core and interacts with the SQL database through Entity Framework. Designed with multithreading in mind, it ensures high performance and scalability.

### 2. **SQL Database**
Our data powerhouse, structured to efficiently store and manage flight details. It supports quick data retrieval and updates, enabling real-time processing and responsiveness.

### 3. **Client-Side Application (React with SignalR)**
The face of our project, this application provides a dynamic and interactive user experience. It displays flight updates and movements through the airport terminal in real-time, thanks to SignalR.

## ✨ Features

- **Real-Time Flight Updates**: Stay informed with live updates on flight movements and details, powered by SignalR.
- **CRUD Operations**: Full control over flight data with comprehensive create, read, update, and delete operations.
- **Multithreading Support**: Our backend is built to handle multiple requests efficiently, ensuring smooth operation.
- **Dynamic Data Visualization**: An engaging and intuitive display of flight information, enhancing the user experience.

## 🛠 Getting Started

To embark on your journey with the Airport Terminal Project, follow these steps:

1. **Clone the Repository**: Secure a local copy of our project by cloning this repository.
2. **Set Up the Database**: Initialize your SQL database using our schema and ensure it's operational.
3. **Configure the Web API**: Adjust the Web API's connection strings to connect to your SQL database.
4. **Launch the Web API**: Start serving flight data by running the ASP.NET Core Web API.
5. **Run the Client Application**: Dive into the React application, install dependencies, and launch it.
6. **Simulate Flight Data**: Utilize the standalone console application to create flight objects and send them to the Web API via HTTP requests.
