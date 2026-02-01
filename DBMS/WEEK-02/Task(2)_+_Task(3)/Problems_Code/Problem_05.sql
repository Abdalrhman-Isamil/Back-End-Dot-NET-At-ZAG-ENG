SELECT e.emp_name ,
    COALESCE(d.dept_name, 'Unassigned') AS dept_name
    FROM Employees e
    LEFT JOIN Departments d ON e.dept_id = d.dept_id;