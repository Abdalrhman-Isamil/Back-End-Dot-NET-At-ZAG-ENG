
--|==================== Solutions ======================|

--problem 01 :

select * 
from cinema 
where description Is not null
AND description !='boring' 
AND id %2 !=0
order by rating desc;

--|=====================================================|


--problem 02 :

select p.project_id , ROUND(AVG(E.experience_years * 1.0), 2) as average_years
from Project P inner join Employee E 
On p.employee_id = E.employee_id
Group By p.project_id;

--|=====================================================|


--problem 03 :

select format(trans_date,'yyyy-MM') As month , country ,
count(*) As trans_count,
count(
    case
    when state='approved'then 1
    end
) As approved_count ,
sum(amount) as trans_total_amount ,
sum(
    case 
    when state='approved'then amount else 0
    end
) approved_total_amount
from Transactions
Group By format(trans_date,'yyyy-MM') , country
ORDER BY month, country;

--|=====================================================|


--problem 04 :


Select teacher_id, COUNT(Distinct subject_id) AS cnt
from Teacher 
Group By teacher_id;


--|=====================================================|


--problem 05 :


select employee_id
from Employees 
where salary < 30000 AND manager_id NOT IN (SELECT employee_id FROM Employees)
ORDER BY employee_id;


--|=====================================================|



--problem 06 :

select  case when id%2 =0  then id-1
when id % 2 !=0 AND id<>(select Max(id) from Seat) then id + 1
else id
end as id,student
from Seat
order by id;

--|=====================================================|



--problem 07 :

select employee_id, department_id
from Employee
where primary_flag = 'Y'

union

select employee_id, department_id
from Employee
where employee_id IN (
    select employee_id
    from Employee
    Group By employee_id
    Having COUNT(*) = 1
);

-- Part of Index problems (Problem 7)
-- The Observability Bottleneck (Composite Indexes)

-- Composite Index أفضل ->
{
Create Index service
On AppLogs (service_name, created_at Desc);
}

--|=====================================================|


--problem 08 :

-- The "Key Lookup" Mystery (Covering Indexes)
الاندكس الحالي مش كفاية لانه موجود على الاوردر ديت بس يعني مش شامل على كل حاجه 
واحنا لما بنيجي نستعلم عن حاجه بيكون فالاغلب لاكتر من اتريبيوت والاندكس الحالي بيحدد صفوف بس 
فده بيبطيء البيرفورمانس خاصة لو عدد الصفوف كبير فيجيب تايم ليميت
بمعنى ان الاندكس موجود بس مش كافي لأن محتاج كل الأعمدة اللي الاستعلام عايزها موجودة في الاندكس نفسه

وهنحلها بالكافرينج اندكس لانه بيشمل كل حاجه والاستعلامات اللي بستخدمها بالاتريبيوتس
 : الكويري بتاعته 
 {

Create Index order
On Orders (order_date)
INCLUDE (customer_id, total_amount);

 }
وبكده يكون الاستعلام هيبقى سريع جدًا والداتا بيز هيمشي كله على الاندكس 

--|=====================================================|


--problem 09 :

create procedure sp_applycategorydiscount
    @catid int,
    @discountpercent decimal(5,2)
as
begin
    update products
    set price = 
        case 
            when price * (1 - @discountpercent / 100) > min_price 
            then price * (1 - @discountpercent / 100)
            else min_price
        end
    where category_id = @catid;
end;



--|=====================================================|

--(((((I used AI for a lot of parts to help me solve this question)))))

--problem 10 :

--Part of Security & Reporting (Problem 10)
--Problem 10: The "Marketing VIP Dashboard" (Views)

CREATE VIEW VIPCUSTOMERS_V AS
SELECT 
    C.NAME,
    C.EMAIL,
    SUM(O.AMOUNT_TOTAL) AS TOTAL_SPENT
FROM CUSTOMERS C
JOIN ORDERS O
    ON C.CUSTOMER_ID = O.CUSTOMER_ID
GROUP BY C.NAME, C.EMAIL
HAVING SUM(O.AMOUNT_TOTAL) > 5000
ORDER BY SUM(O.AMOUNT_TOTAL) DESC;


--|=====================================================|
