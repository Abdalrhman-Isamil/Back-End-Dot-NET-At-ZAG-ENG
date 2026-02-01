--problem 01 :
select
    *
from
    cinema
where
    description Is not null
    AND description != 'boring'
    AND id % 2 != 0
order by
    rating desc;