CREATE TABLE T_Customer(
Customer_ID int IDENTITY(1,1) PRIMARY KEY,
Customer_Account nvarchar(50) NOT NULL,
Customer_Pwd nvarchar(50) NOT NULL,
Customer_Age int NOT NULL,
Customer_Phone nvarchar(50) NULL,
Customer_Email nvarchar (50) NULL
)


CREATE TABLE T_CustomerAddress(
CustomerAddress_ID int IDENTITY(1,1) PRIMARY KEY,
Customer_ID int NOT NULL,
Customer_Address nvarchar (50) NOT NULL
)


CREATE TABLE T_DetailsType(
   DTID int IDENTITY(1,1)PRIMARY KEY,
   DTName nvarchar(50)NOT NULL,
   DTTableName nvarchar(50)NOT NULL,
   DTBeizu nvarchar(50)NOT NULL
)


CREATE TABLE T_Level1(
   Level1_ID int IDENTITY(1,1)PRIMARY KEY,
   Level1_Name nvarchar(50)NOT NULL,
   Level1_Desc nvarchar(500)NULL
)


CREATE TABLE T_Level2(
Level2_ID int IDENTITY (1,1)PRIMARY KEY,
Level2_Name nvarchar(50)NOT NULL,
Level2_Desc nvarchar(500)NULL,
Level1_ID int NULL
)


CREATE TABLE T_Level3(
Level3_ID int IDENTITY (1,1)PRIMARY KEY,
Level3_Name nvarchar(50)NOT NULL,
Level3_Desc nvarchar(500)NULL,
Level2_ID int NULL,
Level1_ID int NULL)


create table T_Order(
Order_ID int identity(1,1)  primary key,
Order_number nchar(10) not null,
CustomerAddress_ID int  not null,
Pay_from nchar(10)  not null,
Is_send  bit  null,
Is_pay  bit null,
Pay_time datetime not  null)


create table  T_ShoppingCart(
ShoppingItem_ID int  identity(1,1)  primary key,
Customer_ID int not null,
Ware_ID int  not null,
Ware_Qty int  not null,
Ware_Sum float null,
Order_ID  int null)
 

create table T_Ware(
Ware_id int identity(1,1)primary key,
Ware_number nchar(10)not null,
Ware_name nvarchar(200)not null,
Ware_weight nchar(10)not null,
Ware_stock int null,
Ware_level3 int not null,
Ware_price float null,
Extent_ID int null,
Ware_image nvarchar(50)null,
DetailsType_ID int not null
)
alter table T_Level2 add foreign key(Level2_ID) references T_Level1(Level1_ID)
alter table T_Level3 add foreign key(Level2_ID) references T_Level2(Level2_ID)
alter table T_Level3 add foreign key(Level1_ID) references T_Level1(Level1_ID)
alter table T_Order add foreign key(CustomerAddress_ID) references T_CustomerAddress(CustomerAddress_ID)
alter table T_ShoppingCart add foreign key(Customer_ID) references T_Customer(Customer_ID)
alter table T_ShoppingCart add foreign key(Order_ID) references T_Order(Order_ID)
alter table T_Ware add foreign key(DetailsType_ID) references T_DetailsType(DTID)



alter table  T_Customer  add  unique  (Customer_Phone) 
alter table  T_Customer  add  unique  (Customer_Email)
alter table  T_Level1    add  unique  (Level1_Name)
alter table  T_Level2    add  unique   (Level2_Name)
alter table  T_Level3    add  unique   (Level3_Name)
alter table  T_Order add  unique (Order_number)
alter table  T_Ware  add  unique  (Ware_number)
 

