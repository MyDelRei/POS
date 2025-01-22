use POS;
go
create table product
(   N int IDENTITY(1,1),
    Product_Id as 'P' + RIGHT('000' + CAST(N as varchar(3)),3) PERSISTED not null PRIMARY KEY,
    product_name NVARCHAR(50) not null,
    price varchar(20) not null,
    size CHAR(5)not null,
    catagories nvarchar(30) not null
)
create TABLE customer 
(   N int identity(1,1),
    Customer_Id as 'C' + RIGHT('000' + CAST(N as varchar(3)),3) PERSISTED not NULL primary key,
    Full_Name nvarchar(50) not null,
    Email nvarchar(100) not null unique,
    Membership_Id char(8) not null,
    Membership_status char(5) not NULL

)
create table Orderlist
(   N int IDENTITY(1,1),
    Order_ID AS 'OD' + RIGHT('000' + CAST(N AS VARCHAR(3)), 3) PERSISTED not null primary KEY,
    Customer_Id varchar(4),
    order_date smalldatetime not null,
    Total_amount varchar(20) not null,
    tax varchar(20) not NULL,
    discount char(4) ,
    CONSTRAINT fk_customer_order FOREIGN KEY (Customer_Id) REFERENCES customer(Customer_Id) on UPDATE cascade on delete cascade 
)
create table order_item
(   N int IDENTITY(1,1),
    orderItem_id AS 'ODI' + RIGHT('000' + CAST(N AS VARCHAR(3)), 3) PERSISTED not null primary KEY,
    Order_ID varchar(5),
    Product_Id varchar(4),
    Quantity int not null , 
    price_active varchar(20),
    size char(4),
    constraint fk_ord_ordItem FOREIGN key (Order_ID) REFERENCES OrderList(Order_ID) on update cascade on delete cascade ,
    constraint fk_product_order_item FOREIGN key (Product_Id) REFERENCES product(Product_Id) on update cascade on delete cascade 
)
create table payment
(   N int identity(1,1), 
    Pay_id AS 'PI' + RIGHT('000' + CAST(N AS VARCHAR(3)), 3) PERSISTED not null primary key,
    Order_Id varchar(5), 
    Payment_Method NVARCHAR(20),
    Pay_date smallDateTime not null,
    amount_pay char(7),
    FOREIGN key (Order_ID) REFERENCES OrderList(Order_ID) on update cascade on delete cascade 
)
