# Wine Country Festival Control System

## Overview

The Wine Country Festival Control System is a console-based C# application designed to simulate the operations of a wine festival. The system manages attendees, wine booths, and wine stock while supporting real-time background operations, event notifications, exception handling, and multithreading.

The project demonstrates Object-Oriented Programming principles, interfaces, threading, events, custom exceptions, logging, file handling, and testing.



## Domain

Event & Festival Management

## System Name

Wine Country Festival Control System

---

## Team Members

### Person 1 – Foundation & Core Logic
- OOP Design
- Abstract Base Class
- Inheritance
- Interfaces
- Business Logic
- Data Store

### Person 2 – Multithreading & Concurrency
- Background Monitoring Loop
- Processing Queue
- Thread Safety
- Random Failure Generator
## Multithreading and Concurrency

The system uses asynchronous Tasks to simulate real-time festival operations while keeping the application responsive.

### Background Monitoring Loop

A monitoring task runs independently of user input and performs periodic system checks every few seconds.

Examples:

- Monitor low wine stock
- Monitor VIP attendee check-ins
- Monitor booth activity

### Processing Queue

The system uses a queue structure to manage tasks in a First-In-First-Out (FIFO) order.

Example queue operations:

- Generate Sales Report
- Check Inventory
- Update Attendance Records

### Random Failure Generator

A background task periodically selects a random wine booth and simulates operational failures. This creates realistic festival scenarios and allows the system to react to changing conditions.

### Thread Safety

Shared resources are protected using lock statements.


lock(_lockObject)
{
    // Safe access to shared data
}


This prevents race conditions when multiple tasks access festival data at the same time.

### Concurrency Model

The system runs three background tasks:

1. Monitoring Loop
2. Processing Queue
3. Random Failure Generator

These tasks run concurrently using:
Task.Run()
//and periodically pause using:
await Task.Delay(...)
//to simulate real-time operations without freezing the console application.


### Person 3 – Events, UI & Exception Handling
- Menu System
- Events & Delegates
- Custom Exceptions
- User Input Validation

### Person 4 – Bonus Features & Quality Assurance
- File Management
- Logging System
- Unit Testing
- Documentation


### Project Architecture
- Program.cs = Entry point and primary menu execution loop
- Festival.cs = Core model representing the state of the festival
- FestivalService.cs =Logic layer for organising workflow and services
- Winebooth.cs= Class representing the vendor booths and details
- WineStock.cs= Class representing the inventory of the wine and its qualities
- attendees.cs= Class for attendees and managment
- Events.cs = Event management
- FileManager.cs = File input/output handler and used for loading app data
- Logger.cs = Logging utility for error tracking
- Display.cs = UI helper for rendering the text and console output
- IConsumables.cs = Interface defining standard properties of consumable items
- IReport.cs= Interface for making reports
- Exception.cs = Exception classes for specific error handling
- SimpleTests.cs= Test suite for verifying logic functionality 
