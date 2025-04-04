# Concert Ticket Management System

## Overview
The Concert Ticket Management System is a .NET Web API designed to manage concert events, ticket reservations, and venue capacity. It enables event organizers to create and manage events, while customers can reserve and purchase tickets.

---

## Features
### 1. Event Management
- Create and update concert events
- Set ticket types and pricing
- Manage available venue capacity
- Store event details (date, venue, description)

### 2. Ticket Reservations and Sales
- Reserve tickets for a limited time
- Purchase tickets through an existing payment system
- Cancel reservations
- View real-time ticket availability

---

## User Stories

### Event Management
#### 1. Create a Concert Event
- As an event organizer  
- I want to create a new concert event with details like name, date, venue, and ticket types  
- So that customers can purchase tickets for the event  
- Acceptance Criteria:  
  - The event must have a unique name and be associated with a venue  
  - The event must have a start date in the future  
  - Ticket types and pricing should be defined  
  - If capacity exceeds the venue’s limit, the system should prevent creation  

#### 2. Update a Concert Event
- As an event organizer  
- I want to modify an existing event’s details like date, venue, and ticket types  
- So that I can make adjustments without canceling the event  
- Acceptance Criteria:  
  - Updates are allowed only before ticket sales begin  
  - The new venue must have equal or greater capacity  
  - All changes should be logged for auditing  

#### 3. Set Ticket Types and Pricing
- As an event organizer  
- I want to define different ticket types (e.g., VIP, General Admission) with prices  
- So that customers can choose their preferred ticket type  
- Acceptance Criteria:  
  - Each event can have multiple ticket types  
  - Prices must be greater than zero  
  - The system must enforce venue capacity limits  

#### 4. Manage Available Capacity
- As an event organizer  
- I want to track ticket availability in real-time  
- So that I can manage sales efficiently  
- Acceptance Criteria:  
  - Ticket counts update automatically on reservation/purchase  
  - System prevents sales when venue capacity is reached  
  - Organizers can view real-time ticket availability  

---

### Ticket Reservations and Sales
#### 5. Reserve Tickets for a Time Window
- As a customer  
- I want to reserve tickets for a concert event for a short time (e.g., 10 minutes)  
- So that I can complete my purchase before they are released back  
- Acceptance Criteria:  
  - Reserved tickets are temporarily removed from availability  
  - Expired reservations return tickets to the available pool  
  - Customers receive a confirmation message with reservation details  

#### 6. Purchase Tickets
- As a customer  
- I want to purchase tickets using an integrated payment system  
- So that I can secure my spot at the event  
- Acceptance Criteria:  
  - Only available tickets can be purchased  
  - The system integrates with a payment processor  
  - Customers receive digital tickets via email upon purchase  

#### 7. Cancel a Reservation
- As a customer  
- I want to cancel my ticket reservation before purchase  
- So that my reserved tickets are released for others  
- Acceptance Criteria:  
  - Reservations can be canceled only before purchase  
  - Canceled tickets return to the availability pool  
  - Customers receive a confirmation of cancellation  

#### 8. View Ticket Availability
- As a customer  
- I want to check available tickets for an event  
- So that I can decide whether to buy before they sell out  
- Acceptance Criteria:  
  - The system displays real-time ticket availability  
  - Availability can be filtered by ticket type  
  - Sold-out events are clearly indicated
