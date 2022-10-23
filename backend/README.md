# backend

##Методы:

### Авторизация
POST 127.0.0.1:80/api/auth

Content-Type: application/json

{
"dictionary":"ru_en",
"email":"vasia"
}

### Проверка авторизации
GET 127.0.0.1:80/api/auth

### Получение слов
GET 127.0.0.1:80/api/words?count=10


### отправка слова
POST 127.0.0.1:80/api/words

Content-Type: application/json

{
"word_id":2,
"count":10,
"queue_words": [2,3]
}



