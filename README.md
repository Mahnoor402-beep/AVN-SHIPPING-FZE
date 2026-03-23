# 🚢 AVN Shipping FZE — Freight Forwarding Management System

![AVN Shipping FZE](screenshot.png)

> A full-featured desktop-based Freight Forwarding Management System 
> built from scratch for AVN Shipping FZE using C# and Microsoft Visual Studio.

---

## 📌 Overview

This is a complete **Windows Desktop Application** developed to digitize 
and streamline all freight forwarding operations for AVN Shipping FZE. 
The system replaces manual processes with a centralized, efficient, 
and user-friendly platform.

---

## ✨ Features & Modules

| Module | Description |
|--------|-------------|
| 📊 **Dashboard** | Real-time overview of all operations and key metrics |
| 👥 **Customers** | Full client profile and relationship management |
| 📋 **Inquiries** | Log, track and assign freight inquiries |
| 🗂️ **Job Cards** | End-to-end shipment tracking and job management |
| 💰 **Billing** | Automated invoicing, payments and balance tracking |
| 📈 **Profitability** | Margin and revenue tracking per shipment |
| 🏦 **Accounts** | Complete financial account management |
| 👨‍💼 **Employees** | Staff profiles, roles and access control |

---

## 🛠️ Tech Stack

| Technology | Details |
|------------|---------|
| **Language** | C# (C-Sharp) |
| **IDE** | Microsoft Visual Studio |
| **Database** | SQL Server |
| **Framework** | .NET Framework / Windows Forms |
| **Platform** | Windows Desktop Application |
| **Architecture** | Object-Oriented Programming (OOP) |

---

## 🖥️ Screenshots

### Dashboard
![Dashboard](screenshots/dashboard.png)

### Customers Module
![Customers](screenshots/customers.png)

### Job Cards Module
![Job Cards](screenshots/jobcards.png)

### Billing Module
![Billing](screenshots/billing.png)

### Profitability Module
![Profitability](screenshots/profitability.png)

---

## 🚀 Getting Started

### Prerequisites
- Windows OS (Windows 10 or later)
- Microsoft Visual Studio 2019 or later
- SQL Server (2019 or later)
- .NET Framework 4.8 or later

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/AVN-Shipping-FZE-Freight-Management-System.git
```

2. **Open the project**
```bash
Open Visual Studio → File → Open → Project/Solution
Select the .sln file
```

3. **Set up the Database**
```bash
Open SQL Server Management Studio
Run the script in /database/setup.sql
Update connection string in App.config
```

4. **Build and Run**
```bash
Press F5 in Visual Studio to build and run
OR
Build → Start Without Debugging (Ctrl + F5)
```

---

## ⚙️ Configuration

Open `App.config` and update your SQL Server connection string:
```xml
<connectionStrings>
  <add name="AVNShippingDB"
       connectionString="Server=YOUR_SERVER_NAME;
                        Database=AVNShippingFZE;
                        Integrated Security=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

---

## 📂 Project Structure
```
AVN-Shipping-FZE/
│
├── 📁 Forms/
│   ├── Dashboard.cs
│   ├── Customers.cs
│   ├── Inquiries.cs
│   ├── JobCards.cs
│   ├── Billing.cs
│   ├── Profitability.cs
│   ├── Accounts.cs
│   └── Employees.cs
│
├── 📁 Models/
│   ├── Customer.cs
│   ├── JobCard.cs
│   ├── Invoice.cs
│   └── Employee.cs
│
├── 📁 Database/
│   ├── setup.sql
│   └── DatabaseHelper.cs
│
├── 📁 Screenshots/
│   ├── dashboard.png
│   ├── customers.png
│   ├── jobcards.png
│   └── billing.png
│
├── App.config
├── Program.cs
└── README.md
```

---

## 💡 Key Highlights

- ✅ Built entirely from scratch — no third-party frameworks
- ✅ Clean Object-Oriented architecture
- ✅ Fully integrated modules — data flows seamlessly across the system
- ✅ Real-world deployment at AVN Shipping FZE
- ✅ Scalable and maintainable codebase

---


## 📄 License

This project is licensed under the MIT License.
See the [LICENSE](LICENSE) file for details.

