from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
 
Base = declarative_base()
 
engine = create_engine(
  "postgresql+psycopg2://usi:kaikusai134@192.168.11.97/account",
)
 
Session = sessionmaker(bind=engine)
session = Session()
