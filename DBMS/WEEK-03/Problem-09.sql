--problem 09 :
create procedure sp_applycategorydiscount@catid int,
@discountpercent decimal(5, 2) as
begin
update products
set
    price = case
        when price * (1 -@discountpercent / 100) > min_price then price * (1 -@discountpercent / 100)
        else min_price
    end
where
    category_id =@catid;

end;