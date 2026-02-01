--problem 03 :
select
    format (trans_date, 'yyyy-MM') As month,
    country,
    count(*) As trans_count,
    count(
        case
            when state = 'approved' then 1
        end
    ) As approved_count,
    sum(amount) as trans_total_amount,
    sum(
        case
            when state = 'approved' then amount
            else 0
        end
    ) approved_total_amount
from
    Transactions
Group By
    format (trans_date, 'yyyy-MM'),
    country
ORDER BY
    month,
    country;