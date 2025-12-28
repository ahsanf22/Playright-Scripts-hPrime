# HPrime Automation Test Suite 🧪

Enterprise-grade regression testing framework for HPrime, built with **Playwright for .NET (C#)**.
Designed to validate complex clinical workflows across **multiple hospital instances**, handling unique form logic and flows for each client deployment while maintaining a unified core architecture.

## 🚀 Tech Stack
* **Framework:** Playwright for .NET
* **Language:** C#
* **Design Pattern:** Page Object Model (POM)
* **Runner:** NUnit / MSTest
* **CI/CD:** GitHub Actions (Ready)

## 📂 Project Structure
This solution employs a **multi-tenant architecture** where different hospital instances are tested within a single repository:

* `HPrimeTestProject/` - Core framework, shared utilities, and base classes used across all instances.
* `GC_PGY/` - **Gold Coast Hospital:** Specific workflows for patient registration and clinical forms.
* `HBH_WBA_Beta/` - **HBH Hospital:** Validation scripts for beta environment deployments.
* `HNE_NMW_Beta/` - **HNE Hospital:** pecialized testing for the Interview Module.
* `IMU/` - **International Medical University:** Specialized testing for the semester based forms across instances.

## 🛠️ Key Features
* **Scalable Architecture:** Supports multiple hospital configurations (Tenants) with distinct business rules in one solution.
* **Page Object Model (POM):** Strict separation of locators and logic for long-term maintainability.
* **Parallel Execution:** Optimized for speed using NUnit parallelization.
* **Auto-Wait & Retries:** Leveraging Playwright's built-in stability features to handle network latency.

## 🏃‍♂️ How to Run
1. **Clone the repository:**
   ```bash
   git clone [https://github.com/ahsanf22/Playright-Scripts-hPrime.git](https://github.com/ahsanf22/Playright-Scripts-hPrime.git)
