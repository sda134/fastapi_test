from models import User, Category, Resource
import database

# 開発用にデータテーブルをリセットする
# 運用開始したら、このモジュールは削除する事

if __name__ == "__main__":
    # テーブルを作成する
    database.Base.metadata.drop_all(bind=database.engine)
    database.Base.metadata.create_all(bind=database.engine, checkfirst=False)

    food = Category(name='食費')
    miscellaneous = Category(name='雑費')
    database.session.add(food)
    database.session.add(miscellaneous)

    card_docomo = Resource(name='Dカード')
    wallet_sada = Resource(name='サダ財布')
    database.session.add(card_docomo)
    database.session.add(wallet_sada)
    database.session.commit()
    
