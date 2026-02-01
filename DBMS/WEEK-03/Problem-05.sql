--problem 05 :
select
    employee_id
from
    Employees
where
    salary < 30000
    AND manager_id NOT IN (
        SELECT
            employee_id
        FROM
            Employees
    )
ORDER BY
    employee_id;