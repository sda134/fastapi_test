from core.urls import app        # urls.py で add_api_routeした後の appが取得できる（らしい）
import uvicorn
#import core.database

if __name__ == '__main__':
    uvicorn.run(app=app, port=8000)
