import hashlib
from datetime import datetime
from sqlalchemy.schema import Column, ForeignKey
from sqlalchemy.types import Integer, String, Date
from database import Base


class User(Base):
    __tablename__ = "user"  				# テーブル名を指定
    user_id = Column('id', Integer, primary_key=True)
    name = Column(String)
    password = Column(String(255))

    def __init__(self, username, password):
        self.username = username
        self.password = hashlib.md5(password.encode()).hexdigest()

class Category(Base):
    __tablename__ = "category"              # テーブル名を指定
    category_id = Column('id', Integer, primary_key=True)
    name = Column(String)

    def __init__(self, name):
        self.name = name

class Resource(Base):
    __tablename__ = "resource"              # テーブル名を指定
    resource_id = Column('id', Integer, primary_key=True)
    name = Column(String)

    def __init__(self, name):
        self.name = name
        

class AccountRecord(Base):
    __tablename__ = "account_records"       # テーブル名を指定
    account_id = Column('id', Integer, primary_key=True)
    #date = models.DateField(default=timezone.now)  Djangoの場合
    date = Column(Date, default=None, nullable=False )
    ammount = Column(Integer, default=0)
    name = Column(String)
    category_id = Column(ForeignKey('category.id'))
    resource_id = Column(ForeignKey('resource.id'))
