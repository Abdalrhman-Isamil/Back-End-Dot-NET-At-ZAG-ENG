--problem 04 :
Select
    teacher_id,
    COUNT(Distinct subject_id) AS cnt
from
    Teacher
Group By
    teacher_id;