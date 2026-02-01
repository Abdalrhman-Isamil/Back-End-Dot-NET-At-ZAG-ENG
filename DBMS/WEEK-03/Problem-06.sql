--problem 06 :
select
    case
        when id % 2 = 0 then id -1
        when id % 2 != 0
        AND id <> (
            select
                Max(id)
            from
                Seat
        ) then id + 1
        else id
    end as id,
    student
from
    Seat
order by
    id;