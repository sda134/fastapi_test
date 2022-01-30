from fastapi import APIRouter

router = APIRouter()

@router.get("/records/{date}")
async def list_records():
    pass
