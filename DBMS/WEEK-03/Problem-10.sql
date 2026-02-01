--(((((I used AI for a lot of parts to help me solve this question)))))
--problem 10 :
--Part of Security & Reporting (Problem 10)
--Problem 10: The "Marketing VIP Dashboard" (Views)
CREATE VIEW
    VIPCUSTOMERS_V AS
SELECT
    C.NAME,
    C.EMAIL,
    SUM(O.AMOUNT_TOTAL) AS TOTAL_SPENT
FROM
    CUSTOMERS C
    JOIN ORDERS O ON C.CUSTOMER_ID = O.CUSTOMER_ID
GROUP BY
    C.NAME,
    C.EMAIL
HAVING
    SUM(O.AMOUNT_TOTAL) > 5000
ORDER BY
    SUM(O.AMOUNT_TOTAL) DESC;