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
MenuName  nvarchar(20) not null,--�˵�����
MenuType varchar(50) default('view'),--�¼����� ��click view scancode_push ��
MenuKeyValue varchar(1000),--�˵�KEYֵ��Valueֵ
MenuFid int default(0),
EventType varchar(50) default('none'),--�¼����� view ������ת���¼�  clickӵ��һЩ�¼� �� text images �����ı� ����ͼƬ��
MenuOrder int default(0),--�˵�����
CreateTime datetime default(getdate())
)