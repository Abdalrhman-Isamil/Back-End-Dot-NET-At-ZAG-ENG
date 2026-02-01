SELECT CONCAT(c.first_name, ' ', c.last_name) AS full_name ,
    o.order_id ,
    o.amount
    FROM Customers c FULL OUTER JOIN Orders o ON c.customer_id = o.customer_id;
