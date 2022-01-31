from datetime import datetime, date
from typing import Optional
from fastapi import APIRouter, Query, Form
from fastapi.requests import Request                # starlette.requests でも良い
from fastapi.responses import HTMLResponse, RedirectResponse 
from fastapi.templating import Jinja2Templates      # starlette.templating でも可

from core.models import User, Category, Resource, AccountRecord
import core.database as database

router = APIRouter()

templates = Jinja2Templates(directory="app/templates")
jinja_env = templates.env

@router.get("/", response_class=HTMLResponse)
def index(request: Request):
    return templates.TemplateResponse(
        'index.html',
        {'request': request})

@router.get("/admin", response_class=HTMLResponse)
def admin(request: Request):
    return templates.TemplateResponse(
        'admin.html',
        {'request': request, 'username': 'admin'})

@router.get("/account", response_class=HTMLResponse)
def account_list(request: Request):
    today=str(date.today())
    return RedirectResponse(('/account/%s' % (today)))

@router.get("/account/{date}", response_class=HTMLResponse)
def account_list(request: Request, date:date, query_date:Optional[str] = Query(None)):
    if query_date:       
        #date = datetime.strptime(query_date, '%Y-%m-%d').date()
        # 'account/'にすると相対になっって account/account になるので注意
        return RedirectResponse('/account/%s' % (query_date))   
    else:    
        records = database.session.query(AccountRecord).filter(AccountRecord.date==date).all()
        database.session.close()
        return templates.TemplateResponse(
            'account/list.html',
            {
                'request': request,
                'records': records,
                'date':date,
            })


@router.get("/account/adding/{date}", response_class=HTMLResponse)
def account_add(request: Request, date:date):
    categories = database.session.query(Category).all()
    resources = database.session.query(Resource).all()
    return templates.TemplateResponse(
        'account/adding.html',
        {
            'request': request,
            'categories': categories,
            'resources': resources,
            'date': date,
        })

# requestはうまく言ったが… 2022-01-31
@router.post("/account/adding/{date}", response_class=HTMLResponse)
async def request_add(request: Request):    # Request はstarlette らしい
    form = await request.form()
    ammount = form['ammount']
    print(amount)

#@router.post("/account/request/add")
#def request_add(amount: int=Form(...)):
#    print(amount)

