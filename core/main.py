from datetime import datetime, date

from fastapi import FastAPI
from fastapi.requests import Request            # starlette.requests でも良い
from fastapi.responses import HTMLResponse, RedirectResponse 
from fastapi.templating import Jinja2Templates  # starlette.templating でも可

from core.models import User, Category, Resource, AccountRecord
import core.database as database

app = FastAPI(
    title='SShel イントラ',
    description='家庭内イントラサービス',
    version='0.0 beta'
)

templates = Jinja2Templates(directory="core/templates")
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
def account_list(request: Request):
    today=str(date.today())
    return RedirectResponse(('account/%s' % (today)))

@app.get("/account/{date}", response_class=HTMLResponse)
def account_list(request: Request, date:date):
    print(date)
    records = database.session.query(AccountRecord).filter(AccountRecord.date==date).all()
    database.session.close()
    return templates.TemplateResponse(
        'account/list.html',
        {'request': request,
        'records': records})


@app.get("/account/add", response_class=HTMLResponse)
def account_add(request: Request):
    return templates.TemplateResponse(
        'account/add.html',
        {'request': request})
   