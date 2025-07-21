using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 /*
HealLink is a medical collaboration system designed for patients and doctors. 
The application allows patients to subscribe to doctors to receive ongoing care, share medical history, and track progress. 
Below is the domain model context that should be considered while reviewing the design and behavior of the domain entities.

🧑‍⚕️ USER ROLES:
- There are two user types: Doctor and Patient.
- All users start as a generic User with basic info, then become either a Doctor or Patient via registration.

👥 PATIENT-GUARDIAN RELATIONSHIP:
- A Patient can have one Guardian (another registered patient).
- A Guardian can view some of the patient's medical information and medication schedule.
- This is modeled through the PatientGuardian entity, which includes metadata like RelationshipType.

📄 MEDICAL HISTORY:
- Patients can record different types of medical history (e.g., old scans, chronic conditions, allergies).
- All history types share most properties, but some (like scans) may include additional file links.
- The design supports inheritance or unified type with internal differentiation by category.

📨 DOCTOR REQUESTS:
- Patients must send a request to subscribe to a Doctor.
- There is only one DoctorRequest entity, with an enum `RequestType` to distinguish between:
    - `New`: initiating a subscription.
    - `Renewal`: extending an existing subscription.
- The request includes:
    - PatientId and DoctorId.
    - Requested subscription duration.
    - (For renewals) a reference to the existing subscription.

💰 PAYMENT FLOW:
- Payment is made **before** the doctor accepts the request.
- If the doctor rejects the request, payment should be refunded externally.
- This system assumes Stripe or a similar provider handles payments.
- In development mode, payments may be mocked.

🩺 PATIENT-DOCTOR SUBSCRIPTIONS:
- A subscription (PatientDoctorSubscription) links a Patient and Doctor.
- Each subscription has:
    - Start and end dates.
    - A reference to the DoctorRequest that created or renewed it.
- All subscriptions follow a centralized set of durations (e.g., 1 day, 1 week, 1 month).
- Doctors cannot define custom durations or prices.

📆 RENEWALS:
- Renewals are just another type of DoctorRequest.
- The system should track renewal history and the originating request.

🧾 OTHER NOTES:
- Stripe keys are stored in appsettings and backend handles payment logic.
- No frontend interaction with Stripe directly in this version.
- In development, test payments can be initiated through the backend using mock APIs.

GOAL:
This domain design should support extensibility, simple logic flow, and clear separation of concerns.
Please help validate that the classes and properties in the domain models align with this system description.
*/

