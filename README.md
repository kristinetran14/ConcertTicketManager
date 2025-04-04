# 🎟️ Concert Ticket Management System

## Overview
The **Concert Ticket Management System** is an ASP.NET MVC (.NET 9) and Web API application designed to manage concert events, ticket reservations, and venue capacity. It enables event organizers to create and manage events while allowing customers to reserve and purchase tickets seamlessly.

## 🚀 Features

### 🎫 Event Management
- Create and update concert events
- Define ticket types and pricing (e.g., VIP, General Admission)
- Manage venue capacity
- Store and display event details (date, venue, description)

### 🛒 Ticket Reservations and Sales
- Reserve tickets for a limited time window
- Complete ticket purchases via integrated payment system
- Cancel ticket reservations
- View real-time ticket availability and remaining capacity

---

## 👤 User Stories

### 1. Create a Concert Event
**As an event organizer**  
I want to create a new concert event with details like name, date, venue, and ticket types  
**So that** customers can purchase tickets for the event.

**Acceptance Criteria:**
- Event name must be unique and linked to a venue
- Start date must be in the future
- Ticket types and pricing must be defined
- Capacity cannot exceed venue limit

---

### 2. Update a Concert Event
**As an event organizer**  
I want to modify an existing event’s details like date, venue, and ticket types  
**So that** I can make adjustments without canceling the event.

**Acceptance Criteria:**
- Updates are only allowed **before ticket sales begin**
- The new venue must have **equal or greater capacity**
- All changes must be **logged for auditing**

---

### 3. Set Ticket Types and Pricing
**As an event organizer**  
I want to define ticket types with different prices  
**So that** customers can choose based on preference.

**Acceptance Criteria:**
- Multiple ticket types per event
- Pricing must be positive
- Total ticket count must respect venue capacity

---

### 4. Manage Available Capacity
**As an event organizer**  
I want real-time tracking of ticket availability  
**So that** I can manage ticket sales efficiently.

**Acceptance Criteria:**
- Ticket counts auto-update on reservation and purchase
- No ticket sales beyond venue capacity
- Organizers can view live availability stats

---

### 5. Reserve Tickets for a Time Window
**As a customer**  
I want to temporarily reserve tickets  
**So that** I have time to complete the purchase.

**Acceptance Criteria:**
- Reserved tickets are removed from availability
- Expired reservations return tickets to inventory
- Confirmation message includes reservation details

---

### 6. Purchase Tickets
**As a customer**  
I want to securely purchase tickets  
**So that** I can attend the event.

**Acceptance Criteria:**
- Only available tickets can be purchased
- Integrated with payment provider (e.g., Stripe/PayPal)
- Email confirmation includes digital ticket(s)

---

### 7. Cancel a Reservation
**As a customer**  
I want to cancel a reservation before purchasing  
**So that** others can book those tickets.

**Acceptance Criteria:**
- Only unpurchased reservations can be canceled
- Canceled tickets return to the availability pool
- Cancellation confirmation is sent to the user

---

### 8. View Ticket Availability
**As a customer**  
I want to view available tickets by type  
**So that** I can decide whether to purchase.

**Acceptance Criteria:**
- Real-time availability shown per ticket type
- Sold-out status clearly indicated
- Filter available tickets by type

---

## ☁️ Azure Integration

This project is designed to run on Microsoft Azure with the following components:

- **Azure App Services** – for hosting the MVC frontend and Web API
- **Azure SQL Database** – for storing event, venue, and ticket data

### 🔗 Live Demo
Access the live application here:  
👉 [Concert Ticket Management System (Live on Azure)]([https://your-azure-app-url.azurewebsites.net](https://concertticketwebapi-cjhtewe8cpfyfqe3.westus2-01.azurewebsites.net/))

---

## 🛠 Tech Stack

- **.NET 9**, **ASP.NET MVC**, **Web API**
- **Entity Framework Core**
- **SQL Server / Azure SQL**
- **Azure App Services**
- Optional: **Stripe** or **PayPal** integration for payments
