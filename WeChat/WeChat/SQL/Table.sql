create database WeChat
go
use WeChat
go
create table WeChat_Token
(
Id int identity(1,1) primary key,
TokenValue varchar(200),
CreateTime datetime,
)


CREATE TABLE SYS_Role(
Id int IdENTITY(1,1) primary key,
RightName nvarchar(50) NULL,
RightVle int NULL,
isAdd bit NULL default(0),--是否有增加的权限
isUpdate bit NULL default(0),--是否有修改的权限
isDelete bit NULL default(0),--是否有删除的权限
Addtime datetime NULL,
isdeleted bit default(0)
)


create table SYS_UserInfo
(
Id int identity(1,1) primary key,
Uname varchar(20) not null,
Upwd varchar(100) NULL,--用户密码  
RoleId int  FOREIGN KEY REFERENCES SYS_Role(Id),
EmployeeName nvarchar(20),--职员姓名
pinyin varchar(20),--拼音
idCard varchar(20),--身份证号
PhotoNum varchar(50),--头像编号
EmployeeSex nvarchar(1) default('男'),
EmployeePhone varchar(20),--联系方式
Age int,
Worker nvarchar(50),--职业/工作岗位
HomeAddress nvarchar(200),--家庭地址
IP varchar(50),--注册IP
AddTime varchar(50),--注册时间
remark1 nvarchar(500),--以下为备注字段
isdeleted bit default(0)
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
CreateTime datetime default(getdate()),
CreateBy int  FOREIGN KEY REFERENCES SYS_UserInfo(Id),
isdeleted bit default(0)
)


create table SYS_pic--系统配图
(
Id int identity(1,1) primary key,
PicNum varchar(50) not null ,--配图编号001  001
PicCate varchar(20),--配图种类  商品配图  
shopingPic varchar(200),--配图/pic/1.jpg  
AddTime datetime,--添加时间
remark1 nvarchar(500),--以下为备注字段
)


create table SYS_Log--系统日志表
(
Id int identity(1,1) primary key,
userNum varchar(50),--操作人账户
userId int,--操作人Id
userRight int,--操作人权限
logcate nvarchar(20),--操作类型  登录 修改 删除 增加(注册) 转账 
logContent nvarchar(500),--操作内容  账号为张三的用户于2015-5-25 14:14:14 在IP为121.121.121.121的机器上针对商品种类进行了删除操作！
logIp varchar(50),--IP地址
logTime datetime,--操作时间
remark1 nvarchar(500),
CreateTime datetime default(getdate()),
CreateBy int  FOREIGN KEY REFERENCES SYS_UserInfo(Id),
)


create table SYS_Menus--框架菜单表
(
Id int identity(1,1) primary key,
menuName nvarchar(20),--栏目名称
menuPth varchar(200),--栏目路径URL
FId int,--父Id
rightId varchar(10),--访问权限
AddTime datetime,--添加时间
MenusSort int default(0),--排序
isdeleted bit default(0),
remark1 varchar(00),--图标?
)