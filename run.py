from main import app
import uvicorn

if __name__ == '__main__':
    uvicorn.run(app=app, port=8000)
    