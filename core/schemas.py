from typing import Optional
from datetime import datetime, date
from pydantic import BaseModel, Field

# まだ何を記述するのかよく分かっていない 2022-01-31
# Pydantic モデル型で宣言された場合、リクエストボディとして解釈されます。

class AccountRecord(BaseModel):
    date:date
    ammount:int
    category_id:int
    resource_id:int