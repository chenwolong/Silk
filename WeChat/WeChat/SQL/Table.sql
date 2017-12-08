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
isAdd bit NULL default(0),--�Ƿ������ӵ�Ȩ��
isUpdate bit NULL default(0),--�Ƿ����޸ĵ�Ȩ��
isDelete bit NULL default(0),--�Ƿ���ɾ����Ȩ��
Addtime datetime NULL,
isdeleted bit default(0)
)


create table SYS_UserInfo
(
Id int identity(1,1) primary key,
Uname varchar(20) not null,
Upwd varchar(100) NULL,--�û�����  
RoleId int  FOREIGN KEY REFERENCES SYS_Role(Id),
EmployeeName nvarchar(20),--ְԱ����
pinyin varchar(20),--ƴ��
idCard varchar(20),--���֤��
PhotoNum varchar(50),--ͷ����
EmployeeSex nvarchar(1) default('��'),
EmployeePhone varchar(20),--��ϵ��ʽ
Age int,
Worker nvarchar(50),--ְҵ/������λ
HomeAddress nvarchar(200),--��ͥ��ַ
IP varchar(50),--ע��IP
AddTime varchar(50),--ע��ʱ��
remark1 nvarchar(500),--����Ϊ��ע�ֶ�
isdeleted bit default(0)
)

create table WeChat_Menus
(
Id int identity(1,1) primary key,
MenuName  nvarchar(20) not null,--�˵�����
MenuType varchar(50) default('view'),--�¼����� ��click view scancode_push ��
MenuKeyValue varchar(1000),--�˵�KEYֵ��Valueֵ
MenuFid int default(0),
EventType varchar(50) default('none'),--�¼����� view ������ת���¼�  clickӵ��һЩ�¼� �� text images �����ı� ����ͼƬ��
MenuOrder int default(0),--�˵�����
CreateTime datetime default(getdate()),
CreateBy int  FOREIGN KEY REFERENCES SYS_UserInfo(Id),
isdeleted bit default(0)
)


create table SYS_pic--ϵͳ��ͼ
(
Id int identity(1,1) primary key,
PicNum varchar(50) not null ,--��ͼ���001  001
PicCate varchar(20),--��ͼ����  ��Ʒ��ͼ  
shopingPic varchar(200),--��ͼ/pic/1.jpg  
AddTime datetime,--���ʱ��
remark1 nvarchar(500),--����Ϊ��ע�ֶ�
)


create table SYS_Log--ϵͳ��־��
(
Id int identity(1,1) primary key,
userNum varchar(50),--�������˻�
userId int,--������Id
userRight int,--������Ȩ��
logcate nvarchar(20),--��������  ��¼ �޸� ɾ�� ����(ע��) ת�� 
logContent nvarchar(500),--��������  �˺�Ϊ�������û���2015-5-25 14:14:14 ��IPΪ121.121.121.121�Ļ����������Ʒ���������ɾ��������
logIp varchar(50),--IP��ַ
logTime datetime,--����ʱ��
remark1 nvarchar(500),
CreateTime datetime default(getdate()),
CreateBy int  FOREIGN KEY REFERENCES SYS_UserInfo(Id),
)


create table SYS_Menus--��ܲ˵���
(
Id int identity(1,1) primary key,
menuName nvarchar(20),--��Ŀ����
menuPth varchar(200),--��Ŀ·��URL
FId int,--��Id
rightId varchar(10),--����Ȩ��
AddTime datetime,--���ʱ��
MenusSort int default(0),--����
isdeleted bit default(0),
remark1 varchar(00),--ͼ��?
)