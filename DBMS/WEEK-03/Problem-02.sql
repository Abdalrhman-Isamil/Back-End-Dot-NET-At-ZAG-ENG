--problem 02 :
select
    p.project_id,
    ROUND(AVG(E.experience_years * 1.0), 2) as average_years
from
    Project P
    inner join Employee E On p.employee_id = E.employee_id
Group By
    p.project_id;