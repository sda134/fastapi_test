from fastapi import FastAPI

from api.router import router as api_router
from app.router import router as app_router

app = FastAPI(                                      # FastAPIクラス: Starletteを継承している
    title='SShel イントラ',
    description='家庭内イントラサービス',
    version='0.0 beta'
)
app.include_router(api_router)
app.include_router(app_router)
