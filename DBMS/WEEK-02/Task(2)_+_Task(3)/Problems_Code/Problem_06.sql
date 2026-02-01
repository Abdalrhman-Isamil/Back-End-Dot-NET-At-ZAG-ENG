    SELECT p.product_name , s.supplier_name
    FROM Products p
    LEFT JOIN Suppliers s ON p.supplier_id = s.supplier_id
    WHERE p.product_name LIKE '%Phone%';