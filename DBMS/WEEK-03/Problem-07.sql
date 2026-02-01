--problem 07 :
select
    employee_id,
    department_id
from
    Employee
where
    primary_flag = 'Y'
union
select
    employee_id,
    department_id
from
    Employee
where
    employee_id IN (
        select
            employee_id
        from
            Employee
        Group By
            employee_id
        Having
            COUNT(*) = 1
    );

-- Part of Index problems (Problem 7)
-- The Observability Bottleneck (Composite Indexes)
-- Composite Index أفضل ->
Create Index service On AppLogs (service_name, created_at Desc);