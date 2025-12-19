SELECT
    D1.id
FROM
    Weather D1
    JOIN Weather D2 ON DATEDIFF (day, D2.recordDate, D1.recordDate) = 1
WHERE
    D1.temperature > D2.temperature;