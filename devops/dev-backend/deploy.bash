# Билдим backend
docker build -t "backend-api" ~/thousandwords/backend 

# Запускаем сервис
docker-compose stop
docker-compose rm -f
docker-compose up -d
