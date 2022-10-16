# Билдим статику
# npm run build --prefix ~/thousandwords/frontend # TEST
# Билдим backend
# docker build -t "backend-api" ~/thousandwords/backend # TODO
# Перекладываем статику #TEST

# Запускаем сервис
docker-compose stop
docker-compose rm -f
docker-compose up


# c ci-cd статика, compose file , nginx.conf