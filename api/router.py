from fastapi import APIRouter

router = APIRouter()

@router.post("/api/record/add")     # 新規作成
async def add_record():
    pass

@router.put("/api/record/edit")     # 修正
async def edit_record():
    pass
