create procedure findProducts
     @min_price decimal(10,2),
     @max_price decimal(10,2),
     @count int output

as
begin
    select @count = count(*)
    from Products
    where Price between @min_price and @max_price
end;



declare @productsCount int;

exec findProducts 10, 100, @productsCount output;

select @productsCount as 'Products Count';

