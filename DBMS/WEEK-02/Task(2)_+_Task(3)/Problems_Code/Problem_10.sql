-- ? ================= 1NF-Steps-Tables ===================== -- ?

--! 1NF Steps :
--! We separate patient_phones → PatientPhones table
--! We separate appointment_dates → Appointments table

-- * << Small Note >> * --> : 
--* [ I'm generate this forms tables by AI ] After created them manually in my paper .
-- * ( Thank you for all the efforts Team Leaders ) * -- 

Patients Table :

| patient_id | patient_name |
| 1          | John Smith   |
| 2          | Mary Johnson |



PatientPhones Table :

| patient_id | phone_number |
| 1          | 555-0101     |
| 1          | 555-0102     |
| 2          | 555-0201     |




Doctors Table :

| doctor_id | doctor_name | specialization | clinic_name        |
| 201       | Dr. Adams   | Cardiology     | Heart Care Center  |
| 202       | Dr. Brown   | Dermatology    | Skin Health Clinic |




Appointments Table :

| appointment_id | patient_id | doctor_id | appointment_date |
| 1              | 1          | 201       | 2024-01-15       |
| 1              | 1          | 201       | 2024-01-22       |
| 2              | 2          | 202       | 2024-01-18       |
| 3              | 1          | 202       | 2024-02-10       |




-- ? ==================== 2NF-Steps-Tables ===================== -- ?

--! 2NF Steps : 
--! Principle: Everything depends entirely on the primary key.

--! Appointments Table → Primary Key = (appointment_id, appointment_date)

--! Each column depends entirely on the primary key 

--! Doctors Table → Each column depends only on doctor_id 

--! Patients Table → Each column depends only on Patient_id

Patients Table :

| Column Name  | Type    | Key |
| patient_id   | int     | PK  |
| patient_name | varchar |     |




PatientPhones Table :

| Column Name  | Type    | Key    |
| patient_id   | int     | PK, FK |
| phone_number | varchar | PK     |




Doctors Table :

| Column Name    | Type    | Key |
| doctor_id      | int     | PK  |
| doctor_name    | varchar |     |
| specialization | varchar |     |
| clinic_name    | varchar |     |


Appointments Table : 

| Column Name      | Type | Key |
| appointment_id   | int  | PK  |
| patient_id       | int  | FK  |
| doctor_id        | int  | FK  |
| appointment_date | date | PK  |


-- ? ==================== 3NF-Steps-Tables ===================== -- ?

--! 2NF Steps : 
--! There are no transitive dependencies

Patients Table : 

| patient_id | patient_name |

PatientPhones Table : 

| patient_id | phone_number |

Doctors Table : 

| doctor_id | doctor_name | specialization | clinic_name |

Appointments Table : 

| appointment_id | patient_id | doctor_id | appointment_date |