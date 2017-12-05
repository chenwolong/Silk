create database WeChatDB
go
use WeChatDB
go
create table WeChat_Token
(
Id int identity(1,1) primary key,
TokenValue varchar(200),
CreateTime datetime,
)

create table WeChat_Menus
(
Id int identity(1,1) primary key,
MenuName  nvarchar(20) not null,--菜单名称
MenuType varchar(50) default('view'),--事件类型 有click view scancode_push 等
MenuKeyValue varchar(1000),--菜单KEY值或Value值
MenuFid int default(0),
EventType varchar(50) default('none'),--事件类型 view 链接跳转无事件  click拥有一些事件 如 text images 发送文本 发送图片等
MenuOrder int default(0),--菜单排序
CreateTime datetime default(getdate())
)