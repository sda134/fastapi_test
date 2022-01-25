from fastapi import FastAPI
from fastapi.requests import Request            # starlette.requests でも良い
from fastapi.responses import HTMLResponse      # 
from fastapi.templating import Jinja2Templates  # starlette.templating でも可
#from starlette.requests import Request

# test
from models import User
import database

app = FastAPI(
    title='SShel イントラ',
    description='家庭内イントラサービス',
    version='0.0 beta'
)

templates = Jinja2Templates(directory="templates")
jinja_env = templates.env
 

#@app.get("/", response_class=HTMLResponse)   urls.pyよりも、このスタイルの方が絶対良い
def index(request: Request):
    return templates.TemplateResponse(
        'index.html',
        {'request': request})

#@app.get("/admin", response_class=HTMLResponse)   urls.pyよりも、このスタイルの方が絶対良い
def admin(request: Request):
    return templates.TemplateResponse(
        'admin.html',
        {'request': request, 'username': 'admin'})

@app.get("/account", response_class=HTMLResponse)
def admin(request: Request):
    return templates.TemplateResponse(
        'account/top.html',
        {'request': request})

@app.get("/account/add", response_class=HTMLResponse)
def admin(request: Request):
    return templates.TemplateResponse(
        'account/add.html',
        {'request': request})


@app.get("/test", response_class=HTMLResponse)
def test(request: Request):
    records = database.session.query(User).all() 
    for rec in records:
        print(rec.name)